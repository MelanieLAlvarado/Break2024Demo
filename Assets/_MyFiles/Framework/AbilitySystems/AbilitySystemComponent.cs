using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystemComponent : MonoBehaviour
{
    public delegate void OnAbilityGivenDelegate(Ability newAbility);
    public event OnAbilityGivenDelegate onAbilityGiven;


    [SerializeField] float maxMana = 100f;
    [SerializeField] Ability[] initialAbilities;


    List<Ability> _abilities = new List<Ability>();
    private float _mana;

    private void Start()
    {
        _mana = maxMana;

        foreach (Ability ability in initialAbilities) 
        {
            GiveAbility(ability);
        }
    }

    public void GiveAbility(Ability newAbility)
    { 
        Ability ability = Instantiate(newAbility);
        ability.Init(this);

        _abilities.Add(ability);
        onAbilityGiven?.Invoke(ability);
    }

    internal bool TryConsumeMana(float manaCost)
    {
        if (_mana < manaCost)
        {
            return false;
        }
        _mana -= manaCost;
        return true;
    }
}
