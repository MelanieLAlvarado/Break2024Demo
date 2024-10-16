using UnityEngine;

public abstract class Weapon : MonoBehaviour, ISocketInterface //is monobehaviour still because the children will be monobehaviour
{
    //abstract means that this class cannot be instanced.
    [SerializeField] string attachSocketName;
    [SerializeField] AnimatorOverrideController overrideController;
    [SerializeField] private float attackAnimSpeedMult = 1f;
    private readonly static int _attackAnimSpeedId = Animator.StringToHash("attackAnimSpeedMult");
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
        if (ownerAnimator && overrideController) 
        {
            ownerAnimator.runtimeAnimatorController = overrideController;
            
            ownerAnimator.SetFloat(_attackAnimSpeedId, attackAnimSpeedMult);
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
    public abstract void Attack(); //able to have child class override this

}
