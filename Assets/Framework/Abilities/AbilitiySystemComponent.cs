using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiySystemComponent : MonoBehaviour
{

    public delegate void OnAbilityGivenDelegate(Ability newAbility);
    public event OnAbilityGivenDelegate OnAbilityGiven;

    public delegate void OnManaUpdatedDelegate(float newMana, float delta, float maxMana);
    public event OnManaUpdatedDelegate onManaUpdated;

    public float maxMana = 100f;
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
        foreach(Ability ability in initialAbilities)
        {
            GiveAbility(ability);
        }

        onManaUpdated?.Invoke(_mana, Mana, MaxMana);
    }

    public void GiveAbility(Ability newAbility)
    {
        Ability ability = Instantiate(newAbility);
        ability.Init(this);

        _abilities.Add(ability);
        Debug.Log($"ability is {ability}");
        OnAbilityGiven?.Invoke(ability);
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
