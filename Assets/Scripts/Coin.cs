using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed;
    void Start()
    {
        speed = 10f;
    }
    void Update()
    {
        if (GameManager.instance.IsMagnet)
        {
            Collider[] hit = Physics.OverlapSphere(transform.position, 8f);
            if (hit.Length != 0)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].gameObject.tag == "Player")
                    {
                        Debug.Log("tag" + hit[i].gameObject.tag);
                        if(transform.parent.position.z < hit[i].gameObject.transform.position.z)
                        {
                            speed = 20f;
                        }
                        else
                        {
                            speed = 10f;
                        }
                        transform.parent.position = Vector3.MoveTowards(transform.parent.position, hit[i].gameObject.transform.position, Time.deltaTime * speed);
                        break;
                    }

                }
            }
        }
    }
  
	
}
