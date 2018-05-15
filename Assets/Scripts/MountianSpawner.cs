using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountianSpawner : MonoBehaviour
{
    private const float DISTANCE_TO_RESPAWN = 5f;
    public float scrollSpeed;
    public float totalLength;
    public bool IsScrolling { set; get; }
    private float scrollLocation;
    private Transform playerTransfrom;

    void Start()
    {
        scrollSpeed = -3.5f;
        playerTransfrom = GameObject.FindGameObjectWithTag("Player").transform;
        IsScrolling = false;
    }
    void Update()
    {
        if (!IsScrolling)
            return;
        scrollLocation += scrollSpeed * Time.deltaTime;
        Vector3 newLocation = (playerTransfrom.transform.position.z + scrollLocation) * Vector3.forward;
        transform.position = newLocation;
        if(transform.GetChild(0).position.z < playerTransfrom.transform.position.z - DISTANCE_TO_RESPAWN)
        {
            transform.GetChild(0).localPosition += Vector3.forward * totalLength;
            transform.GetChild(0).SetSiblingIndex(transform.childCount);

            //transform.GetChild(0).localPosition += Vector3.forward * totalLength;
            //transform.GetChild(0).SetSiblingIndex(transform.childCount);
        }
    }
}
