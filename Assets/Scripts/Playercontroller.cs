using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private const float LANE_DISTANCE = 2.5f;
    private CharacterController controller;
    public bool isRunning;
    public bool isFlying;
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
        controller = this.GetComponent<CharacterController>();
        anim = this.GetComponent<Animator>();
        isRunning = false;
        isFlying = false;
        //transform.rotation = Quaternion.Euler(60f, 0f, 0f);
    }
    void Update()
    {
        if (!isRunning)
            return;
        if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;
            GameManager.instance.UpdateModifier(speed - originalSpeed);
        }
        bool isGrounded = IsGrounded();
        anim.SetBool("Grounded", isGrounded);
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
        if(isFlying)
        {
            // flying
            anim.SetTrigger("Idle");
            // verticalVelocity = Mathf.Lerp(verticalVelocity, jumpForce / 2, Time.deltaTime * speed);
            verticalVelocity = jumpForce / 2;
            transform.rotation = Quaternion.Euler(60f, 0f, 0f);
            StartCoroutine(StopFlying(5f));
        }
        else
        {
            if (isGrounded)
            {
                anim.SetTrigger("StartRunning");
                verticalVelocity = 0f;
                if (Input.GetKeyDown(KeyCode.UpArrow) || MobileInput.instance.SwipeUp)
                {
                    // Jump
                    anim.SetTrigger("Jump");
                    verticalVelocity = jumpForce;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) || MobileInput.instance.SwipeDown)
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
        }
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        Vector3 newtargetPosition = new Vector3(desiredLane * GameSettings.LANE_DISTANCE, 0, 0);
        targetPosition = Vector3.Lerp(newtargetPosition, new Vector3(desiredLane * GameSettings.LANE_DISTANCE, 0, 0), Time.deltaTime * speed);
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).x * speed;
        moveVector.y = verticalVelocity;
        moveVector.z = speed;
        controller.Move(moveVector * Time.deltaTime);
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0f;
            transform.forward = Vector3.Lerp(transform.forward, dir, 0.05f);
        }


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
    IEnumerator StopFlying(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isFlying = false;
        verticalVelocity -= gravity * Time.deltaTime;
    }
    void Crash()
    {
        anim.SetTrigger("Death");
        isRunning = false;
        GameManager.instance.OnDeath();
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        switch (hit.gameObject.tag)
        {
            case Tag.OBSTACLE:
                {
                    Crash();
                    break;
                }
            
            case Tag.COIN:
                {
                    Debug.Log("coin");
                    GameManager.instance.GetCoin();
                    hit.transform.gameObject.SetActive(false);
                    break;
                }
            case Tag.ITEM_FLY:
                {
                    hit.gameObject.SetActive(false);
                    Debug.Log("isFlying" + isFlying);
                    isFlying = true;
                    break;
                }
            case Tag.ITEM_MAGNET:
                {
                    hit.gameObject.SetActive(false);
                    GameManager.instance.IsMagnet = true;
                    Debug.Log(GameManager.instance.IsMagnet);
                    
                    break;
                }
            // to be continued...
            default:
                {
                    break;
                }
        }
    }
   
}
