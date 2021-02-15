using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOnDestroy : MonoBehaviour
{
    void OnDestroy()
    {
        Debug.Log("This is when it goes kaput");
    }
}
