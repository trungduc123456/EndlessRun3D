using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public Transform playerTransform;
	// Use this for initialization
	void Start ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		
	}
	// B4E9E5
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = Vector3.forward * playerTransform.position.z;
		
	}
}
