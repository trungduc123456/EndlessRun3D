using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Contacts2D : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D aCol)
    {
        Debug.Log("va cham");
        ContactPoint2D[] contacts = aCol.contacts;
        Debug.Log(contacts.Length);
        Debug.Log(contacts[0].point);
        Debug.Log(contacts[1].point);
        if(contacts[0].point.y != contacts[1].point.y)
        {
            Debug.Log("va vao thanh tuong");
        }
        else
        {
            Debug.Log("khong sao");
        }
        
    }
}
