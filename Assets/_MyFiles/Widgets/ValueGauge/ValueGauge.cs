using UnityEngine;
using UnityEngine.UI;

public abstract class ValueGauge : Widget
{
    [SerializeField] private Slider slider;

    public virtual void UpdateValue(float newValue, float newMaxValue) 
    {
        if (newMaxValue == 0)
        {
            return;
        }
        slider.value = newValue / newMaxValue;
    }
}
