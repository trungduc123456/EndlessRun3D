using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform lookAt;
    public Vector3 offSet = new Vector3(0f, 5f, -10f);
	// Use this for initialization
	void Start ()
    {
       // transform.position = lookAt.position + offSet;
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        Vector3 desiredPosition = lookAt.position + offSet;
        desiredPosition.x = 0;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
	}
}
