using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public delegate void InputUpdatedDelegate(Vector2 inputVal);

    public event InputUpdatedDelegate OnInputUpdated;
    
    public event Action OnInputClicked;

    [SerializeField] private RectTransform centerTransform;
    [SerializeField] private RectTransform rangeTransform;
    [SerializeField] private RectTransform thumbStickTransform;
    [SerializeField] private float deadZone = 0.2f;

    private float _range;
    private bool _bWasDragging;
    private void Awake()
    {
        _range = rangeTransform.sizeDelta.x / 2f;
        deadZone = deadZone * deadZone;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rangeTransform.position = eventData.pressPosition;
        thumbStickTransform.position = eventData.pressPosition;
        _bWasDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rangeTransform.localPosition= Vector2.zero;
        thumbStickTransform.localPosition= Vector2.zero;
        OnInputUpdated?.Invoke(Vector2.zero);
        if (!_bWasDragging) 
        {
            OnInputClicked?.Invoke();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _bWasDragging = true;

        Vector2 offset = eventData.position - eventData.pressPosition;
        offset = Vector2.ClampMagnitude(offset, _range);
        Vector2 input = offset / _range;
        if (input.sqrMagnitude< deadZone) 
        {
            return;
        }

        thumbStickTransform.position = eventData.pressPosition + offset;
        OnInputUpdated?.Invoke(offset/_range);
    }
}
