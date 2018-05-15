using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float LANE_DISTANCE = 2.5f;
    private CharacterController controller;
    public bool isRunning;
    private Animator anim;
    private float jumpForce = 5f;
    private float gravity = 12f;
    private float verticalVelocity;
    private float speed;
    private int desiredLane;

    //speed modifier
    private float originalSpeed = 12f;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;
    void Start()
    {
        desiredLane = 0;
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
        if (Input.GetKeyDown(KeyCode.LeftArrow) || MobileInput.instance.SwipeLeft)
        {
            MoveLane(false);
            Debug.Log(desiredLane);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || MobileInput.instance.SwipeRight)
        {
            MoveLane(true);
            Debug.Log(desiredLane);
        }
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        //if (desiredLane == 0)
        //{
        //    //targetPosition = Vector3.Lerp()
        //    //targetPosition = Vector3.Lerp()
        //     targetPosition += Vector3.left * LANE_DISTANCE;
        //   // targetPosition = new Vector3(desiredLane * LANE_DISTANCE, 0, 0);
        //}
        //else if (desiredLane == 2)
        //{
        //    targetPosition += Vector3.right * LANE_DISTANCE;
        //}
        targetPosition = new Vector3(desiredLane * LANE_DISTANCE, 0, 0);
        //Vector3 moveVector = targetPosition;
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).x * speed;
        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);
        if (isGrounded)
        {

            verticalVelocity = 0f;
            if (Input.GetKeyDown(KeyCode.Space) || MobileInput.instance.SwipeUp)
            {
                // Jump
                anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow) || MobileInput.instance.SwipeDown)
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
       // controller.transform.position = Vector3.MoveTowards(controller.transform.position, targetPosition, LANE_DISTANCE * Time.deltaTime);
        controller.Move(moveVector * Time.deltaTime);
       // transform.position = moveVector * Time.deltaTime;
        //Vector3 dir = controller.velocity;
        //dir.y = 0;
        //transform.forward = Vector3.Lerp(transform.forward, dir, 0.05f);

    }
    void MoveLane(bool goingRight)
    {
        
        desiredLane += (goingRight) ? 1 : -1;
       
        desiredLane = Mathf.Clamp(desiredLane, -1, 1);
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
