using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (pathing)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, nodes[nodeCount].transform.position, step);

            if ((Vector2)transform.position == (Vector2)nodes[nodeCount].transform.position)
            {
                if (nodeCount + 1 == nodes.Count)
                {
                    if (looping)
                    {
                        nodeCount = 0;
                    }
                }
                else
                {
                    if (!waiting)
                    {
                        StartCoroutine("WaitTime");
                    }
                }
            }
            //Debug.Log(pathing);
        }
    }

    IEnumerator WaitTime()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        waiting = false;
        nodeCount++;
    }
}
