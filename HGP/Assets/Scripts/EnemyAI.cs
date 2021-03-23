using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

//This is for enemies that chase the player. Keep in mind I haven't changed or looked at this script for weeks.
//I'm still not sure if I want to try and elaborate on what's here or overhaul it to be a straight up pathfinding script.
//Unity Navmesh doesn't work by default in a 2D unity scene, you see. Fortunately the scenes probably won't have very
//complex layouts.

//Also I just discovered I commented some of this code before, so that's nice.

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
    public bool Active
    {
        get { return active; }
        set { active = value; }
    }
    private float xSpeed;
    private float ySpeed;
    private float prevX;
    private float prevY;
    private Animator enemyAnimator;

    public int testLoad = 10;

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
        //if (Input.GetKeyDown("j"))
        //{
        //    testLoad = 100;
        //}
        //if (Input.GetKeyDown("h"))
        //{
        //    Debug.Log(testLoad);
        //}
        //Makes sure the enemy is actively following the player.
        if (active)
        {
            //Sets up the distance moved this frame.
            float step = runSpeed * Time.deltaTime;

            //Casts a ray towards the player.
            RaycastHit2D hit = Physics2D.Raycast(transform.position, ((Vector2)target.position - (Vector2)transform.position), sightRadius);
            //Draws a ray towards the position the enemy is moving towards. Debugging purposes.
            Debug.DrawRay(transform.position, (targetVector - (Vector2)transform.position), Color.red);
            //If the cast ray hits the player, the position the enemy is moving towards updates.
            if (hit && hit.collider.gameObject.tag == "Player")
            {
                //Checks if the player is hiding, and if not, sets the targetVector to the player's position.
                PlayerHide playerHide = hit.collider.gameObject.GetComponent<PlayerHide>();
                if (!playerHide.Hiding)
                {
                    nodePath.Pathing = false;
                    step = runSpeed * Time.deltaTime;
                    targetVector = target.position;
                }
            }
            else
            //Otherwise, the targetvector is set to self to stop it. Or at least that's what it used to do. Now it turns on
            //the nodepathing component too, which is another script.
            {
                //step = walkSpeed * Time.deltaTime;
                targetVector = transform.position;
                nodePath.Pathing = true;
            }
            //Checks and records the direction the enemy is moving as signed values, to determine if it's moving up or down/left or right
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

            //Resets whatever velocity was added to this frame
            additionalVelocity = new Vector2(0, 0);
        }

        //The code below calculates the speed the enemy is moving in order to inform Unity's animator.
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
        //Tells the object to (try to) avoid anything it collides with.
        AvoidObstacles();
    }

    private void AvoidObstacles()
    {
        //So um, the following was my attempt to get the enemy to move around objects it's run into. It is a very flawed solution,
        //and back when I was testing it not all of its big issues had been resolved; however, other coding tasks were prioritized since
        //the player isn't chased very much in the starting scene we ended up finishing.

        //To sum it up, it casts eight rays that extend from the corners of the object's collision box. If the object collides with something,
        //it checks which corner was collided with at which direction, then adds to the objects current velocity to move it in a direction
        //around the object. This of course breaks if either both or neither corners are touching the object being collided with. As such,
        //this blurb here is probably going to be removed in favor of whatever better solution comes around.
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

    //void ActivateEnemy(string boolean)
    //{
    //    //Sets the enemy to active, in relation to the nodepath script.
    //    active = Boolean.Parse(boolean);
    //}
}
