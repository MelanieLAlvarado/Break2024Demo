using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityDock : Widget, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] AbilityWidget abilityWidgetPrefab;
    [SerializeField] RectTransform rootPanel;
    [SerializeField] float scaleRange = 150f;
    List<AbilityWidget> _abilityWidgets = new List<AbilityWidget>();

    [SerializeField] float scaledSize = 0.5f;
    [SerializeField] float scaledOffset = 50f;
    [SerializeField] float scaleRate = 20f;

    Vector3 _goalScale = Vector3.one;
    Vector3 _goalLocalOffset = Vector3.zero;

    AbilitySystemComponent _abilitySystemComponent;

    PointerEventData _touchData;

    private void Update()
    {
        if (_touchData != null)
        {
            ArrayScale();
        }
        else
        {
            ResetScale();
        }
        ScaleDock();
    }

    public void ScaleDock() 
    {
        rootPanel.transform.localScale = Vector3.Lerp(rootPanel.transform.localScale, _goalScale, Time.deltaTime * scaleRate);
        rootPanel.transform.localPosition = Vector3.Lerp(rootPanel.transform.localPosition, _goalLocalOffset, Time.deltaTime * scaleRate);
    }

    private void ResetScale()
    {
        foreach (AbilityWidget abilityWidget in _abilityWidgets)
        { 
            abilityWidget.SetScaleAmount(0);
        }
        _goalScale = Vector3.one;
        _goalLocalOffset = Vector3.zero;
    }

    private void ArrayScale()
    {
        float touchYPos = _touchData.position.y;
        float scaleAmount = 0;

        foreach (AbilityWidget abilityWidget in _abilityWidgets)
        { 
            float widgetYPos = abilityWidget.transform.position.y;
            float distanceToTouch = Mathf.Abs(widgetYPos - touchYPos);
            if (distanceToTouch > scaleRange)
            {
                abilityWidget.SetScaleAmount(0);
                continue;
            }
            scaleAmount = (scaleRange - distanceToTouch)/scaleRange;

            abilityWidget.SetScaleAmount(scaleAmount);
        }
        _goalScale = Vector3.one * (1 + (scaledSize - 1) * scaleAmount);
        _goalLocalOffset = Vector3.left * scaleAmount * scaledOffset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _touchData = eventData;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ActivateAbilityUnderTouch();
        _touchData = null;
    }

    private void ActivateAbilityUnderTouch()
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_touchData, raycastResults);
        foreach (RaycastResult raycastResult in raycastResults)
        { 
            AbilityWidget abilityWidget = raycastResult.gameObject.GetComponent<AbilityWidget>();
            if (abilityWidget != null)
            {
                abilityWidget.CastAbility();
                return;
            }
        }
    }

    public override void SetOwner(GameObject newOwner)
    { 
        base.SetOwner(newOwner);
        _abilitySystemComponent = newOwner.GetComponent<AbilitySystemComponent>();

        if (_abilitySystemComponent)
        {
            _abilitySystemComponent.onAbilityGiven += AbilityGiven;
        }
        
    }

    private void AbilityGiven(Ability newAbility)
    {
        AbilityWidget newAbilityWidget = Instantiate(abilityWidgetPrefab, rootPanel);
        newAbilityWidget.Init(newAbility);
        _abilityWidgets.Add(newAbilityWidget);
    }
}
