using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    Transform target;
    public float speed = 5;
    Vector3[] path;
    int targetIndex;

    public float targetRange;
    public bool isChasing;

    public Transform[] patrolZones;


    void Start()
    {
        //PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    private void Update()
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
        }
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
        GameObject target = GameObject.FindGameObjectWithTag("Player");

        if (Vector3.Distance(target.transform.position, transform.position) < targetRange)
        {
            return true;
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

        // Gizmo used to display the Unit's targeting range in the editor.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }
}
