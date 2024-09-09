using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SocketManager : MonoBehaviour
{
    List<AttachSocket> _attachSocket;

    public bool FindAndAttachToSocket(ISocketInterface socketInterface)
    {
        InitSockets();
        foreach (AttachSocket socket in _attachSocket) 
        {
            if (socket.IsForSocket(socketInterface))
            {
                socket.Attch(socketInterface);
                return true;
            }
        }
        return false;
        //returns false if it can not find a socket
    }

    void InitSockets()
    {
        if(_attachSocket != null)
        {
            return;
        }

        _attachSocket = new List<AttachSocket>();
        AttachSocket[] attachSockets = GetComponentsInChildren<AttachSocket>();
        _attachSocket.AddRange(attachSockets);
    }
}
