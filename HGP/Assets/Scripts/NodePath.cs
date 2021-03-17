using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This causes objects to move towards nodes in the scene in a set pattern, like NPCs or patrolling enemies. Side note: There is a
//Node prefab, but technically anything could be placed in as a node, though if it has collision this probably won't work.

public class NodePath : MonoBehaviour
{
    [SerializeField]
    List<GameObject> nodes;
    [SerializeField]
    float speed = 5.0f;
    int nodeCount = 0;
    [SerializeField]
    int waitTime = 3;
    bool waiting = false;
    [SerializeField]
    private bool pathing = true;
    [SerializeField]
    private bool looping = false;
    public bool Pathing
    {
        get{return pathing;}
        set {pathing = value;}
    }

    void FixedUpdate()
    {
        //Checks to make sure pathing is enabled.
        if (pathing)
        {
            //Sets the speed, and moves the player toward the next node.
            float step = speed * Time.deltaTime;
            //Debug.Log(nodeCount);
            transform.position = Vector2.MoveTowards(transform.position, nodes[nodeCount].transform.position, step);

            //If the object position is the same as the next node, starts a coroutine.
            if ((Vector2)transform.position == (Vector2)nodes[nodeCount].transform.position && !waiting)
            {
                StartCoroutine("WaitTime");
            }
            //Debug.Log(pathing);
        }
    }

    IEnumerator WaitTime()
    {
        waiting = true;
        //Waits the specified amount of time.
        yield return new WaitForSeconds(waitTime);

        //Increments the nodeCount if not all of the nodes have been touched.
        if (nodeCount < nodes.Count - 1)
        {
            nodeCount++;
        }
        else
        {
            //If all the nodes have been touched and looping is set to true, nodeCount is set to 0;
            if (looping)
            {
                nodeCount = 0;
            }
        }

        waiting = false;
    }
}
