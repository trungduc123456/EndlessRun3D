using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform lookAt;
    private Vector3 offSet;
    private Vector3 moveVector;

    public float transition;
    public float animationDuration;
    public Vector3 animationOffset;
    // Use this for initialization


    void Start()
    {
        transition = 0f;
        animationDuration = 3f;
        animationOffset = new Vector3(0, 5, 5);
        lookAt = GameObject.Find("MAX").transform;
        offSet = transform.position - lookAt.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        moveVector = lookAt.position + offSet;
        moveVector.x = 0;
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5);

        transform.position = moveVector;

    }


}

