using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]

//This one handles moving the player around.

public class PlayerController : MonoBehaviour
{
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
    [SerializeField]
    [Range(0.0f,1.0f)]
    private float staminaEnergyPercentage = 0.2f;
    private float stamina;
    public float Stamina
    {
        get {return stamina;}
    }
    private bool exhausted = false;
    public bool Exhausted
    {
        get { return exhausted; }
    }
    private bool resting = false;
    private Vector2 velocity;
    private Rigidbody2D rBD2D;
    private float xSpeed;
    private float ySpeed;
    private float prevX;
    private float prevY;
    private bool hidden = false;
    public AudioClip[] panting;
    public AudioSource source;
    public bool Hidden
    {
        get {return hidden;}
        set { hidden = value; }
    }
    private Animator playerAnimator;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        rBD2D = GetComponent<Rigidbody2D>();
        stamina = maxStamina;
        prevX = transform.position.x;
        prevY = transform.position.y;
        //SetNoMovingAnimBool();
    }


    void FixedUpdate()
    {
        //Checks if the player is not exhausted
        if (!resting)
        {
            //Checks if the shift key is pressed, if either horizontal or vertical input are not 0, and if the x or y Speed
            //is not 0. It then increases the player's speed and drains their stamina.
            if (Input.GetKey("left shift") && (horizontalInput != 0 || verticalInput != 0) && (xSpeed != 0 || ySpeed != 0))
            {
                sprintSpeed = sprint;
                stamina -= staminaDrain;
                if (stamina < maxStamina * staminaEnergyPercentage)
                {
                    exhausted = true;
                }
                //If stamina goes below 0, exhausted is set to true.
                if (stamina < 0)
                {
                    resting = true;
                }
            }
            else
            //If one of the above isn't true, the player's speed is normal.
            {
                sprintSpeed = 1;
                //Increases stamina if stamina is less than the maximum amount.
                if (stamina < maxStamina)
                {
                    stamina += staminaGain;
                }
                if (stamina < maxStamina * staminaEnergyPercentage)
                {
                    resting = true;
                }
            }
        }
        else
        //If the player is exhausted, the player's speed is halfed and they start getting their stamina back.
        {
            if(!source.isPlaying)
            source.PlayOneShot(panting[Random.Range(0, panting.Length)]);
            sprintSpeed = 0.5f;
            stamina += staminaGain;
            //This caps it off at the max stamina
            if (stamina >= maxStamina)
            {
                stamina = maxStamina;
                exhausted = false;
                resting = false;
            }
        }

        //Gets the position of the player, changes the x and y values based on input, the player's speed, the sprintspeed variable,
        //and whether or not the player is hiding. It moves the rigidbody for collision reasons.
        Vector2 position = transform.position;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        var hidingStopper = hidden ? 0 : 1;

        position.x = position.x + speed * horizontalInput * Time.deltaTime * sprintSpeed * hidingStopper;
        position.y = position.y + speed * verticalInput * Time.deltaTime * sprintSpeed * hidingStopper;

        rBD2D.MovePosition(position);

        //The following code calculates the player's movement for Unity's animator (it is done this way to compensate for cases where
        //the player is being automatically moved, such as when they're moving to a hiding position.

        xSpeed = transform.position.x - prevX;
        ySpeed = transform.position.y - prevY;

        prevX = transform.position.x;
        prevY = transform.position.y;

        GetComponent<AnimatorMovementUpdate>().AnimationUpdate();

        //Vector2 magnitude = new Vector2(xSpeed, ySpeed);

        //playerAnimator.SetFloat("Horizontal", xSpeed);
        //playerAnimator.SetFloat("Vertical", ySpeed);
        //playerAnimator.SetFloat("Speed", magnitude.sqrMagnitude);
    }

    void Update()
    {
        //xSpeed = transform.position.x - prevX;
        //ySpeed = transform.position.y - prevY;

        //prevX = transform.position.x;
        //prevY = transform.position.y;

        //Vector2 magnitude = new Vector2(xSpeed, ySpeed);

        //playerAnimator.SetFloat("Horizontal", xSpeed);
        //playerAnimator.SetFloat("Vertical", ySpeed);
        //playerAnimator.SetFloat("Speed", magnitude.sqrMagnitude);

        ////Debug.Log("X:" + xSpeed + " Y:" + ySpeed + " Magnitude:" + magnitude.sqrMagnitude);
        //Debug.Log("X:" + xSpeed +  " Y:" + ySpeed);
    }
}
