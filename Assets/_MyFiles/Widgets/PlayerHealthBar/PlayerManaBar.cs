using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaBar : Widget
{
    [SerializeField] private Image manaBarImage;
    [SerializeField] private TextMeshProUGUI valueText;
    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        AbilitySystemComponent ownerASC = newOwner.GetComponent<AbilitySystemComponent>();

        if (ownerASC)
        {
            ownerASC.onManaUpdated += UpdateMana;
            UpdateMana(ownerASC.Mana, 0, ownerASC.MaxMana);
        }
    }

    private void UpdateMana(float newMana, float delta, float maxMana) 
    {
        manaBarImage.fillAmount = newMana / maxMana;
        valueText.text = $"{newMana:f0}/{maxMana:f0}";
    }
}
