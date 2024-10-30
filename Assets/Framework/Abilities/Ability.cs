using System.Collections;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public delegate void OnAbilityCooldownStartedDelegate(float cooldownDuration);
    public delegate void OnAbilityCanCastChangedDelegate(bool bCanCast);
    public event OnAbilityCanCastChangedDelegate OnAbilityCanCastChanged; 
    public event OnAbilityCooldownStartedDelegate CooldownStarted;
    [SerializeField] float cooldownDuration = 3f;
    [SerializeField] float manaCost = 10f;
    [SerializeField] Sprite abilityIcon;

    bool _bIsOnCooldown;
    protected AbilitiySystemComponent OwnerASC{
        get; private set;
    }
    protected void BroadcastCanCast()
    {
        OnAbilityCanCastChanged?.Invoke(CanCast());
    }

    public Sprite GetAbilityIcon()
    {
        return abilityIcon;
    }
    public bool TryActivateAbility()
    {
        if (!CanCast())
        {
            return false;
        }
        ActivateAbility();
        return true;
    }
    protected abstract void ActivateAbility();
    public virtual bool CanCast()
    {
         return !_bIsOnCooldown && OwnerASC.Mana >= manaCost;
    }

    public virtual void Init(AbilitiySystemComponent abilitiySystemComponent)
    {
        OwnerASC = abilitiySystemComponent;
        OwnerASC.onManaUpdated += (mana, delta, maxMana) => BroadcastCanCast();
    }

    private void StartCooldown()
    {
        CooldownStarted?.Invoke(cooldownDuration);
        OwnerASC.StartCoroutine(CooldownCoroutine());
        BroadcastCanCast();
    }

    IEnumerator CooldownCoroutine()
    {
        _bIsOnCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        _bIsOnCooldown = false;
        BroadcastCanCast();
    }

    protected bool CommitAbility()
    {
        Debug.Log($"owner for {name} is: {OwnerASC}");
        if (!OwnerASC)
            return false; 

        if(!OwnerASC.TryConsumeMana(manaCost))
            return false;
        
        if(_bIsOnCooldown)
            return false;

        StartCooldown();
        return true;

    }
}
