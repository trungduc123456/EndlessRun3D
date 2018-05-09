using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRailWay : MonoBehaviour
{
    public bool isMove;
	void OnEnable()
    {
        isMove = true;
    }
    void OnDisable()
    {
        isMove = false;
    }
    void Update()
    {
        if(isMove)
        {
            transform.position = Vector3.Lerp(transform.position, -transform.forward, 5f);
        }
        
    }
}
