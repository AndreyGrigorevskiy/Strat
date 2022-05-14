using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectContauner : MonoBehaviour
{

    public GameObject currentGameObject;

    public GameObject GetGameObject()
    {
        return currentGameObject;
    }

    public void SetGameObject(GameObject innerGameobject)
    {
        currentGameObject = innerGameobject;
        Debug.Log("currentGameObject: " + currentGameObject);
    }

    public void SendMesageToGameObject(string message)
    {
        Debug.Log("send: " + message);
        Debug.Log("currentGameObject: " + currentGameObject);
        currentGameObject.SendMessage(message);
        
    }
}
