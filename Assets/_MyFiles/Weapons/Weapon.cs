using UnityEngine;

public abstract class Weapon : MonoBehaviour //is monobehaviour still because the children will be monobehaviour
{
    //abstract means that this class cannot be instanced.

    public GameObject Owner 
    {
        get;
        private set;
    }

    public void Init(GameObject owner) 
    {
        Owner = owner;
    }
    public void Equip() 
    {
        gameObject.SetActive(true);
    }

    public void UnEquip() 
    {
        gameObject.SetActive(false);
    }
}
