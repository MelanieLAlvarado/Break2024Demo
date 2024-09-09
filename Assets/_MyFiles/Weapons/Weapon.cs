using UnityEngine;

public abstract class Weapon : MonoBehaviour, ISocketInterface //is monobehaviour still because the children will be monobehaviour
{
    //abstract means that this class cannot be instanced.
    [SerializeField] string attachSocketName;
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
