using UnityEngine;

public interface ISocketInterface 
{
    string GetSocketName();
    GameObject GetGameObject();
}

public class AttachSocket : MonoBehaviour
{
    [SerializeField] string socketName;

    public bool IsForSocket(ISocketInterface socketInterface)
    { 
        return socketName == socketInterface.GetSocketName();
    }
    public void Attach(ISocketInterface socketInterface) 
    {
        socketInterface.GetGameObject().transform.parent = transform;
        socketInterface.GetGameObject().transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
}
