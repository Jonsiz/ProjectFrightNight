using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimatorMovementUpdate : MonoBehaviour
{
    private float xSpeed;
    private float ySpeed;

    private float prevX;
    private float prevY;

    private bool slice = true;

    private Animator animator;
    void Awake()
    {
        prevX = transform.position.x;
        prevY = transform.position.y;

        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (slice)
        {
            xSpeed = transform.position.x - prevX;
            ySpeed = transform.position.y - prevY;

            prevX = transform.position.x;
            prevY = transform.position.y;
        }

        slice = !slice;

        Vector2 magnitude = new Vector2(xSpeed, ySpeed);

        animator.SetFloat("Horizontal", xSpeed);
        animator.SetFloat("Vertical", ySpeed);
        animator.SetFloat("Speed", magnitude.sqrMagnitude);
    }
}
