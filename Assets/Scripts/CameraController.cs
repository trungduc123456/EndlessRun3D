using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform lookAt;
    public Vector3 offSet = new Vector3(0f, 5f, -10f);
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
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (!IsMoving)
            return;
        Vector3 desiredPosition = lookAt.position + offSet;
        desiredPosition.x = 0;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
	}
}
