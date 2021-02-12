using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{

    //    [SerializeField]
    //    private AnimatorController playerMovingLeft;
    //    [SerializeField]
    //    private AnimatorController playerMovingRight;
    //    [SerializeField]
    //    private AnimatorController playerLeftIdle; // Plug in idle animation if you want!
    //    [SerializeField]
    //    private AnimatorController playerRightIdle;
    //    [SerializeField]
    //    private AnimatorController playerMovingUp;
    //    [SerializeField]
    //    private AnimatorController playerMovingDown;
    //    [SerializeField]
    //    private AnimatorController playerUpIdle;
    //    [SerializeField]
    //    private AnimatorController playerDownIdle;


    private float horizontalInput;
    private float verticalInput;
    [SerializeField]
    private float speed = 15;
    //private float xRange = 30;
    //private float yRange = 30;
    [SerializeField]
    private float sprint = 5;
    private float sprintSpeed;
    [SerializeField]
    private float maxStamina = 5;
    public float MaxStamina
    {
        get {return maxStamina;}
    }
    [SerializeField]
    private float staminaDrain = 0.1f;
    [SerializeField]
    private float staminaGain = 0.2f;
    private float stamina;
    public float Stamina
    {
        get {return stamina;}
    }
    private bool exhausted = false;
    private Vector2 velocity;
    private Rigidbody2D rBD2D;
    private float xSpeed;
    private float ySpeed;
    private float prevX;
    private float prevY;
    private bool hidden = false;
    public bool Hidden
    {
        get {return hidden;}
        set { hidden = value; }
    }

    private Animator playerAnimator;

    // Start is called before the first frame update

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        rBD2D = GetComponent<Rigidbody2D>();
        stamina = maxStamina;
        prevX = transform.position.x;
        prevY = transform.position.y;
        //SetNoMovingAnimBool();
    }

    //void Start()
    //{
    //    rBD2D = GetComponent<Rigidbody2D>();
    //}

    
    void FixedUpdate()
    {
        if (!exhausted)
        {
            if (Input.GetKey("left shift") && (horizontalInput != 0 || verticalInput != 0) && (xSpeed != 0 || ySpeed != 0))
            {
                sprintSpeed = sprint;
                stamina -= staminaDrain;
                if (stamina < 0)
                {
                    exhausted = true;
                }
            }
            else
            {
                sprintSpeed = 1;
                if (stamina < maxStamina)
                {
                    stamina += staminaGain;
                }
            }
        }
        else
        {
            sprintSpeed = 0.5f;
            stamina += staminaGain;
            if (stamina >= maxStamina)
            {
                stamina = maxStamina;
                exhausted = false;
            }
        }

        Vector2 position = transform.position;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        var hidingStopper = hidden ? 0 : 1;

        position.x = position.x + speed * horizontalInput * Time.deltaTime * sprintSpeed * hidingStopper;
        position.y = position.y + speed * verticalInput * Time.deltaTime * sprintSpeed * hidingStopper;

        rBD2D.MovePosition(position);

        xSpeed = transform.position.x - prevX;
        ySpeed = transform.position.y - prevY;

        prevX = transform.position.x;
        prevY = transform.position.y;

        Vector2 magnitude = new Vector2(xSpeed, ySpeed);

        playerAnimator.SetFloat("Horizontal", xSpeed);
        playerAnimator.SetFloat("Vertical", ySpeed);
        playerAnimator.SetFloat("Speed", magnitude.sqrMagnitude);

        //xSpeed = speed * horizontalInput * Time.deltaTime;
        //ySpeed = speed * verticalInput * Time.deltaTime;

        //velocity = new Vector2(xSpeed, ySpeed);

        //if (horizontalInput != 0)
        //{
        //    if (horizontalInput > 0) // towards 1 = right
        //    {
        //        //Debug.Log("The player is moving right.");
        //        playerAnimator.runtimeAnimatorController = playerMovingRight;
        //        //SetMovingRightAnimBool();
        //    }
        //    else if (horizontalInput < 0)  // towards -1 = left
        //    {
        //        //Debug.Log("The player is moving left.");
        //        playerAnimator.runtimeAnimatorController = playerMovingLeft;
        //        //SetMovingLeftAnimBool();
        //    }
        //}
        //else if (horizontalInput == 0)
        //{
        //    if (playerAnimator.runtimeAnimatorController == playerMovingRight)
        //    {
        //        playerAnimator.runtimeAnimatorController = playerRightIdle;
        //    }
        //    else if (playerAnimator.runtimeAnimatorController == playerMovingLeft)
        //    {
        //        playerAnimator.runtimeAnimatorController = playerLeftIdle;
        //    }
        //}
        //else if (verticalInput != 0)
        //{
        //    if (verticalInput > 0) // towards 1 = right
        //    {
        //        Debug.Log("The player is moving up.");
        //        playerAnimator.runtimeAnimatorController = playerMovingUp;
        //        //SetMovingRightAnimBool();
        //    }
        //    else if (verticalInput < 0)  // towards -1 = left
        //    {
        //        Debug.Log("The player is moving down.");
        //        playerAnimator.runtimeAnimatorController = playerMovingDown;
        //        //SetMovingLeftAnimBool();
        //    }
        //}
        //else if (verticalInput == 0)
        //{
        //    if (playerAnimator.runtimeAnimatorController == playerMovingUp)
        //    {
        //        playerAnimator.runtimeAnimatorController = playerRightIdle;
        //    }
        //    else if (playerAnimator.runtimeAnimatorController == playerMovingDown)
        //    {
        //        playerAnimator.runtimeAnimatorController = playerLeftIdle;
        //    }
        //}


        //var hAbsolute = Mathf.Abs(xSpeed);
        //var vAbsolute = Mathf.Abs(ySpeed);
        //var difference = hAbsolute - vAbsolute;
        //var absoluteDifference = Mathf.Abs(difference);

        //playerAnimator.SetFloat("X Speed", xSpeed);
        //playerAnimator.SetFloat("Y Speed", ySpeed);
        //playerAnimator.SetFloat("Horizontal Absolute Value", hAbsolute);
        //playerAnimator.SetFloat("Vertical Absolute Value", vAbsolute);
        //playerAnimator.SetFloat("Difference", difference);
        //playerAnimator.SetFloat("Absolute Value Difference", absoluteDifference);
        //Debug.Log(absoluteDifference);

        //if (absoluteDifference < 0.001)
        //{
        //    if (hAbsolute == 0 && vAbsolute == 0)
        //    {
        //        playerAnimator.runtimeAnimatorController = null;
        //    }
        //}
        //else if (hAbsolute > vAbsolute)
        //{
        //    if (xSpeed > 0) playerAnimator.runtimeAnimatorController = playerMovingRight;
        //    else if (xSpeed < 0) playerAnimator.runtimeAnimatorController = playerMovingLeft;
        //    //else if (horizontalInput == 0)
        //    //{
        //    //    if (playerAnimator.runtimeAnimatorController == playerMovingRight) playerAnimator.runtimeAnimatorController = playerRightIdle;
        //    //    else if (playerAnimator.runtimeAnimatorController == playerMovingLeft) playerAnimator.runtimeAnimatorController = playerLeftIdle;
        //    //}
        //}
        //else if (vAbsolute > hAbsolute)
        //{
        //    if (ySpeed > 0) playerAnimator.runtimeAnimatorController = playerMovingUp;
        //    else if (ySpeed < 0) playerAnimator.runtimeAnimatorController = playerMovingDown;
        //    //else if (verticalInput == 0)
        //    //{
        //    //    if (playerAnimator.runtimeAnimatorController == playerMovingUp) playerAnimator.runtimeAnimatorController = playerUpIdle;
        //    //    else if (playerAnimator.runtimeAnimatorController == playerMovingDown) playerAnimator.runtimeAnimatorController = playerDownIdle;
        //    //}
        //}


        //rBD2D.MovePosition(rBD2D.position + velocity);
    }


    //private void SetNoMovingAnimBool()
    //{
    //    playerAnimator.SetBool("MovingLeft", false);
    //    playerAnimator.SetBool("MovingRight", false);
    //    playerAnimator.SetBool("MovingUp", false);
    //    playerAnimator.SetBool("MovingDown", false);
    //}
    //private void SetMovingLeftAnimBool()
    //{
    //    playerAnimator.SetBool("MovingLeft", true);
    //    playerAnimator.SetBool("MovingRight", false);
    //    playerAnimator.SetBool("MovingUp", false);
    //    playerAnimator.SetBool("MovingDown", false);
    //}
    //private void SetMovingRightAnimBool()
    //{
    //    playerAnimator.SetBool("MovingRight", true);
    //    playerAnimator.SetBool("MovingLeft", false);
    //    playerAnimator.SetBool("MovingUp", false);
    //    playerAnimator.SetBool("MovingDown", false);
    //}

    //private void SetMovingUpAnimBool()
    //{
    //    playerAnimator.SetBool("MovingLeft", false);
    //    playerAnimator.SetBool("MovingRight", false);
    //    playerAnimator.SetBool("MovingUp", true);
    //    playerAnimator.SetBool("MovingDown", false);
    //}
    //private void SetMovingDownAnimBool()
    //{
    //    playerAnimator.SetBool("MovingRight", false);
    //    playerAnimator.SetBool("MovingLeft", false);
    //    playerAnimator.SetBool("MovingUp", false);
    //    playerAnimator.SetBool("MovingDown", true);
    //}

}
