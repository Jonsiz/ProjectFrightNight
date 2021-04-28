using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    bool displayRangeGizmo;
    [SerializeField]
    bool canPathfind;

    public bool CanPathFind
    {
        get { return canPathfind; }
    }

    [SerializeField]
    float speed;
    [SerializeField]
    float maxSpeed;

    [SerializeField]
    Transform[] patrolZones;
    [SerializeField]
    float targetRange;

    Transform target;
    Vector3[] path;
    int targetIndex;
    bool isChasing;

    private float xSpeed;
    private float ySpeed;
    private float prevX;
    private float prevY;
    private Animator enemyAnimator;

    private PixelCrushers.DialogueSystem.Usable usable;


    void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        usable = GetComponent<PixelCrushers.DialogueSystem.Usable>();
    }

    private void Update()
    {
        if (canPathfind)
        {
            if (CanFindPlayer())
            {
                isChasing = true;
                target = GameObject.FindGameObjectWithTag("Player").transform;
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            }
            else
            {
                isChasing = false;
                StopCoroutine("FollowPath");
            }
        }

        //The code below calculates the speed the enemy is moving in order to inform Unity's animator.
        //xSpeed = transform.position.x - prevX;
        //ySpeed = transform.position.y - prevY;

        //prevX = transform.position.x;
        //prevY = transform.position.y;

        //Vector2 magnitude = new Vector2(xSpeed, ySpeed);

        //enemyAnimator.SetFloat("Horizontal", xSpeed);
        //enemyAnimator.SetFloat("Vertical", ySpeed);
        //enemyAnimator.SetFloat("Speed", magnitude.sqrMagnitude);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;

                if (targetIndex >= path.Length)
                {
                    yield break;
                }

                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    /*public void PatrolArea()
    {
        for (int i = 0; i < patrolZones.Length; i++)
        {
            target = patrolZones[i];

            while (transform.position != target.position)
            {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            }
        }
    }*/

    public bool CanFindPlayer()
    {
        // Find the Player in the scene, and get a reference to the Player Controller script.
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        PlayerController targetScript = target.GetComponent<PlayerController>();

        // If the Player is not hidden, check the distance between the Unit and the Player.
        // Returns true if the Player is within range, otherwise returns false.
        if (!targetScript.Hidden)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < targetRange)
            {
                return true;
            }
        }

        return false;

    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }

        // Gizmo used to display the Unit's targeting range in the editor, if this feature is enabled.
        if (displayRangeGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, targetRange);
        }
    }

    void ActivateEnemy(string boolean)
    {
        //Sets the enemy to active, in relation to the nodepath script.
        canPathfind = Boolean.Parse(boolean);
        usable.enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
