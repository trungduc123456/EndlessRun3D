using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform lookAt;
    public Vector3 offSet = new Vector3(0f, 5.5f, -6f);
    private bool isMoving;

    public bool IsMoving
    {
        get
        {
            return isMoving;
        }

        set
        {
            isMoving = value;
        }
    }



    // Use this for initialization
    void Start ()
    {
        IsMoving = false;
		
	}
	
	
    void LateUpdate ()
    {
        if (!IsMoving)
            return;
        Vector3 desiredPosition = lookAt.position + offSet;
       
        desiredPosition.x = 0;
        //if (lookAt.GetComponent<PlayerController>().isFlying)
        //{
            
        //    desiredPosition.y = Mathf.Lerp(desiredPosition.y, 4f, 7f * Time.deltaTime);
        //}

        transform.position = Vector3.Lerp(transform.position, desiredPosition, 7f * Time.deltaTime);

    }
}
