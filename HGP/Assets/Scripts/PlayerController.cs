using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]

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

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        rBD2D = GetComponent<Rigidbody2D>();
        stamina = maxStamina;
        prevX = transform.position.x;
        prevY = transform.position.y;
        //SetNoMovingAnimBool();
    }


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
                if (stamina < maxStamina * staminaEnergyPercentage)
                {
                    exhausted = true;
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
    }
}
