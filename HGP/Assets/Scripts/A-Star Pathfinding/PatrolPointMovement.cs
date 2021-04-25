using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    bool isOn;
    [SerializeField]
    Transform center;

    void Start()
    {
        
    }

    void Update()
    {
        if (isOn)
        {
            transform.RotateAround(center.position, Vector3.forward, moveSpeed * Time.deltaTime);
        }
    }
}
