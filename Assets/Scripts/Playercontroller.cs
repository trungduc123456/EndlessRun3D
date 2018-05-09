using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    protected const int k_StartingLane = 0;
    private float animationDuration;
    protected bool m_Sliding;
    protected bool m_Jumping;
    protected const float laneOffset = 1f;
    protected float m_CurrentLane;
    private float speed;
    private Vector3 moveVeector;
    private float verticalVlocity;
    private const float gravity = 12f;
    public CharacterController characterController;
    protected Vector3 m_TargetPosition;
    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public float VerticalVlocity
    {
        get
        {
            return verticalVlocity;
        }

        set
        {
            verticalVlocity = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        animationDuration = 3f;
        m_Sliding = false;
        m_Jumping = false;
        m_CurrentLane = k_StartingLane;
        m_TargetPosition = Vector3.zero;
        verticalVlocity = 0f;
        speed = 5f;
        characterController = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        moveVeector = Vector3.zero;
        if (characterController.isGrounded)
        {
            verticalVlocity = 0f;
        }
        else
        {
            verticalVlocity -= gravity * Time.deltaTime;
        }
        Debug.Log(Input.GetAxisRaw("Horizontal"));

        moveVeector.x = (Input.GetAxisRaw("Horizontal") * 1.5f) * speed;

        moveVeector.y = verticalVlocity;
        moveVeector.z = speed;
        characterController.Move(moveVeector * Time.deltaTime);

    }
    public void ChangeLane(float indexLane)
    {
        float targetLane = m_CurrentLane + indexLane;
        if (targetLane < -1.5 || targetLane > 1.5)
            return;
        m_CurrentLane = targetLane;
        
    }
    public void Jump()
    {

    }
    public void Slide()
    {

    }
}
