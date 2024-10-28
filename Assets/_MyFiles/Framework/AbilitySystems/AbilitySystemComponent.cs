using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystemComponent : MonoBehaviour
{
    public delegate void OnAbilityGivenDelegate(Ability newAbility);
    public event OnAbilityGivenDelegate onAbilityGiven;

    public delegate void OnManaUpdatedDelegate(float newMana, float delta, float maxMana);
    public event OnManaUpdatedDelegate onManaUpdated;


    [SerializeField] float maxMana = 100f;
    [SerializeField] Ability[] initialAbilities;


    List<Ability> _abilities = new List<Ability>();
    private float _mana;

    public float Mana 
    {
        get => _mana;
        private set => _mana = value;
    }
    public float MaxMana
    {
        get => maxMana;
        private set => maxMana = value;
    }
    private void Awake()
    {
        _mana = maxMana;
    }
    private void Start()
    {
        foreach (Ability ability in initialAbilities) 
        {
            GiveAbility(ability);
        }
        onManaUpdated?.Invoke(_mana, 0,maxMana);
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
        onManaUpdated?.Invoke(_mana, -manaCost, maxMana);
        return true;
    }
}
