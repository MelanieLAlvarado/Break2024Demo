using UnityEngine;

public abstract class Widget : MonoBehaviour
{
    private GameObject _owner;

    public virtual void SetOwner(GameObject newOwner) 
    {
        _owner = newOwner;
    }
}
