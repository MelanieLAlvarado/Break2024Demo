using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SocketManager : MonoBehaviour
{
    List<AttachSocket> _attachSockets;
    //returns false if can't find the socket
    public void FindAndAttachToSocket(ISocketInterface socketInterface) 
    {
        InitSockets();
        foreach (AttachSocket socket in _attachSockets) 
        {
            if (socket.IsForSocket(socketInterface)) 
            {
                socket.Attach(socketInterface);
            }
        }
    }
    void InitSockets() 
    {
        if (_attachSockets != null)
        {
            return;
        }
        _attachSockets = new List<AttachSocket>();
        AttachSocket[] attachSockets = GetComponentsInChildren<AttachSocket>();
        _attachSockets.AddRange(attachSockets);


    }
}
