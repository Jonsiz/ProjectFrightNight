using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Vector2 targetVector;
    //private Vector2 targetVectoerStorage;
    private Rigidbody2D rBD2D;
    private Vector2 additionalVelocity;
    //private Vector2 enemy;
    [SerializeField]
    private float walkSpeed = 10.0f;
    [SerializeField]
    private float runSpeed = 15.0f;
    private RaycastHit2D hit;
    private float horizontalDirection;
    private float verticalDirection;
    private float directionRayDistance = 2;
    private bool avoiding;
    [SerializeField]
    private float sightRadius = 10;
    private NodePath nodePath;
    [SerializeField]
    private bool active;
    private float xSpeed;
    private float ySpeed;
    private float prevX;
    private float prevY;
    private Animator enemyAnimator;

    void Awake()
    {
        //targetVector = target.position;
        //enemy = transform.position;
        rBD2D = GetComponent<Rigidbody2D>();
        //targetVector = target.position;
        avoiding = false;
        nodePath = GetComponent<NodePath>();
        prevX = transform.position.x;
        prevY = transform.position.y;
        enemyAnimator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        if (active)
        {
            //Sets up the distance moved this frame.
            float step = runSpeed * Time.deltaTime;

            //Casts a ray towards the player.
            RaycastHit2D hit = Physics2D.Raycast(transform.position, ((Vector2)target.position - (Vector2)transform.position), sightRadius);
            //Draws a ray towards the position the enemy is moving towards
            Debug.DrawRay(transform.position, (targetVector - (Vector2)transform.position), Color.red);
            //If the cast ray hits the player, the position the enemy is moving towards updates.
            if (hit && hit.collider.gameObject.tag == "Player")
            {
                PlayerHide playerHide = hit.collider.gameObject.GetComponent<PlayerHide>();
                if (!playerHide.Hiding)
                {
                    nodePath.Pathing = false;
                    step = runSpeed * Time.deltaTime;
                    targetVector = target.position;
                }
            }
            else
            {
                //step = walkSpeed * Time.deltaTime;
                targetVector = transform.position;
                nodePath.Pathing = true;
            }
            horizontalDirection = Mathf.Sign(targetVector.x - transform.position.x);
            verticalDirection = Mathf.Sign(targetVector.y - transform.position.y);

            //Moves the enemy towards the targeted position.
            if (!avoiding)
            {
                rBD2D.MovePosition(Vector2.MoveTowards(transform.position, targetVector, step));
            }
            else
            {
                rBD2D.MovePosition((Vector2)transform.position + additionalVelocity);
            }

            additionalVelocity = new Vector2(0, 0);
        }

        xSpeed = transform.position.x - prevX;
        ySpeed = transform.position.y - prevY;

        prevX = transform.position.x;
        prevY = transform.position.y;

        Vector2 magnitude = new Vector2(xSpeed, ySpeed);

        enemyAnimator.SetFloat("HorizontalMovement", xSpeed);
        enemyAnimator.SetFloat("VerticalMovement", ySpeed);
        enemyAnimator.SetFloat("Speed", magnitude.sqrMagnitude);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        AvoidObstacles();
    }

    private void AvoidObstacles()
    {
        //avoiding = true;
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        var bounds = GetComponent<BoxCollider2D>().bounds.extents;
        var xExtent = new Vector2(bounds.x, 0);
        var yExtent = new Vector2(0, bounds.y);
        Vector2[] lineCastDimensions = new Vector2[8];
        lineCastDimensions[0] = (Vector2)transform.position + yExtent + (Vector2)transform.right * directionRayDistance;
        lineCastDimensions[1] = (Vector2)transform.position + yExtent - (Vector2)transform.right * directionRayDistance;
        lineCastDimensions[2] = (Vector2)transform.position - yExtent + (Vector2)transform.right * directionRayDistance;
        lineCastDimensions[3] = (Vector2)transform.position - yExtent - (Vector2)transform.right * directionRayDistance;
        lineCastDimensions[4] = (Vector2)transform.position + xExtent + (Vector2)transform.up * directionRayDistance;
        lineCastDimensions[5] = (Vector2)transform.position + xExtent - (Vector2)transform.up * directionRayDistance;
        lineCastDimensions[6] = (Vector2)transform.position - xExtent + (Vector2)transform.up * directionRayDistance;
        lineCastDimensions[7] = (Vector2)transform.position - xExtent - (Vector2)transform.up * directionRayDistance;

        Debug.DrawLine(lineCastDimensions[0], lineCastDimensions[1]);
        Debug.DrawLine(lineCastDimensions[2], lineCastDimensions[3]);
        Debug.DrawLine(lineCastDimensions[4], lineCastDimensions[5]);
        Debug.DrawLine(lineCastDimensions[6], lineCastDimensions[7]);

        if (Physics2D.Linecast(lineCastDimensions[0], lineCastDimensions[1], layerMask)
            || Physics2D.Linecast(lineCastDimensions[2], lineCastDimensions[3], layerMask)
            || Physics2D.Linecast(lineCastDimensions[1], lineCastDimensions[0], layerMask)
            || Physics2D.Linecast(lineCastDimensions[3], lineCastDimensions[2], layerMask))
        {
            //Debug.Log("Bumping horizontal lines");
            additionalVelocity = transform.up * runSpeed * Time.deltaTime * verticalDirection;
        }
        if (Physics2D.Linecast(lineCastDimensions[4], lineCastDimensions[5], layerMask)
            || Physics2D.Linecast(lineCastDimensions[6], lineCastDimensions[7], layerMask)
            || Physics2D.Linecast(lineCastDimensions[5], lineCastDimensions[4], layerMask)
            || Physics2D.Linecast(lineCastDimensions[7], lineCastDimensions[6], layerMask))
        {
            //Debug.Log("Bumping vertical lines");
            additionalVelocity = transform.right * runSpeed * Time.deltaTime * horizontalDirection;
        }
    }

    void ActivateEnemy(string boolean)
    {
        active = Boolean.Parse(boolean);
    }
}
