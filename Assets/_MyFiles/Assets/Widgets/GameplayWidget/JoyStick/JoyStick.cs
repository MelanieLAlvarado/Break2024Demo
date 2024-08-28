using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public delegate void InputUpdatedDelegate(Vector2 inputVal);

    public event InputUpdatedDelegate OnInputUpdated;
    
    [SerializeField] private RectTransform centerTransform;
    [SerializeField] private RectTransform rangeTransform;
    [SerializeField] private RectTransform thumbStickTransform;

    private float _range;

    private void Awake()
    {
        _range = rangeTransform.sizeDelta.x / 2f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rangeTransform.position = eventData.pressPosition;
        thumbStickTransform.position = eventData.pressPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rangeTransform.localPosition= Vector2.zero;
        thumbStickTransform.localPosition= Vector2.zero;
        OnInputUpdated?.Invoke(Vector2.zero);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset = eventData.position - eventData.pressPosition;
        offset = Vector2.ClampMagnitude(offset, _range);
        thumbStickTransform.position = eventData.pressPosition + offset;
        OnInputUpdated?.Invoke(offset/_range);
    }
}
