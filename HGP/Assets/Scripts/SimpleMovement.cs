using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If I remember/understand this right, someone else made this one to temporarily replace my PlayerController script. I don't remember why, but they needed
//something else ASAP. It's not on the player character anymore, and it copies some of the code from the other controller script, so
//ignore this one.

public class SimpleMovement : MonoBehaviour
{
    private float sprint = 5;
    private float sprintSpeed;
    [SerializeField]
    private float maxStamina = 5;
    [SerializeField]
    private float staminaDrain = 0.1f;
    [SerializeField]
    private float staminaGain = 0.2f;
    private float stamina;
    private bool exhausted = false;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    private float speed = 15;
    public Animator playerAnimator;
    Vector2 movement;


    // Start is called before the first frame update
    void Awake()
    {
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime*sprintSpeed);
        playerAnimator.SetFloat("Horizontal", movement.x);
        playerAnimator.SetFloat("Vertical", movement.y);
        playerAnimator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        if (!exhausted)
        {
            if (Input.GetKey("left shift"))
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

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}
