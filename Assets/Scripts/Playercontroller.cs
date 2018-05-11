using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float LANE_DISTANCE = 2f;
    private CharacterController controller;
    public bool isRunning;
    private Animator anim;
    private float jumpForce = 5f;
    private float gravity = 12f;
    private float verticalVelocity;
    private float speed;
    private int desiredLane = 1;

    //speed modifier
    private float originalSpeed = 7f;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;
    void Start()
    {
        speed = originalSpeed;
       // controller = this.transform.GetChild(0).GetComponent<CharacterController>();
        controller = this.GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
        isRunning = false;
    }
    void Update()
    {
        if (!isRunning)
            return;

        if(Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;
            GameManager.instance.UpdateModifier(speed - originalSpeed);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(false);
            Debug.Log(desiredLane);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(true);
            Debug.Log(desiredLane);
        }
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }

        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;
        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);
        if (isGrounded)
        {

            verticalVelocity = 0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Jump
                anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                // Slide
                StartSliding();
                Invoke("StopSliding", 0.5f);
            }
        }
        else
        {
           
            verticalVelocity -= (gravity * Time.deltaTime);
          
        }
        


        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
        //Vector3 dir = controller.velocity;
        //dir.y = 0;
        //transform.forward = Vector3.Lerp(transform.forward, dir, 0.05f);

    }
    void MoveLane(bool goingRight)
    {
        
        desiredLane += (goingRight) ? 1 : -1;
        if(desiredLane < 0)
        {
            desiredLane = 0;
        }else if(desiredLane > 2)
        {
            desiredLane = 2;
        }
        //desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }
    private bool IsGrounded()
    {
        Ray groundRay = new Ray(new Vector3(controller.bounds.center.x,
            (controller.bounds.center.y - controller.bounds.extents.y) + 0.2f, controller.bounds.center.z), Vector3.down);
        return Physics.Raycast(groundRay, 0.3f);
    }
    public void StartRunning()
    {
        isRunning = true;
        anim.SetTrigger("StartRunning");
    }
    public void StartSliding()
    {
        anim.SetBool("Sliding", true);
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);
    }
    public void StopSliding()
    {
        anim.SetBool("Sliding", false);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
    }
    void Crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;
        GameManager.instance.OnDeath();
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch(hit.gameObject.tag)
        {
            case "Obstacle":
                {
                    Crash();
                    break;
                }
        }
    }
}
