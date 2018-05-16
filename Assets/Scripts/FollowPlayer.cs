using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
	
	void LateUpdate ()
    {
        transform.position = Vector3.forward * playerTransform.position.z;
		
	}
}
