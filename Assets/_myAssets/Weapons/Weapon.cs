using UnityEngine;
using UnityEngine.UIElements.Experimental;

public abstract class Weapon : MonoBehaviour, ISocketInterface
//public abstract class means it cannot be inherited by another object
{
    [SerializeField] string attachSocketName;
    [SerializeField] AnimatorOverrideController overrideController;
    public GameObject Owner
    {
        get;
        private set;
    }
    
    public void Init(GameObject owner)
    {
        Owner = owner;
        SocketManager socketManager = owner.GetComponent<SocketManager>();
        if (socketManager)
        {
            socketManager.FindAndAttachToSocket(this);
        }
        UnEquip();
    }

    public void Equip()
    {
        gameObject.SetActive(true);
        Animator ownerAnimator = Owner.GetComponent<Animator>();
        if (ownerAnimator)
        {
            ownerAnimator.runtimeAnimatorController = overrideController;
        }
    }

    public void UnEquip()
    {
        gameObject.SetActive(false);
    }

    public string GetSocketName()
    {
        return attachSocketName;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
