using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointActivate : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public void ActivateCheckpoint()
    {
        gameObject.SetActive(true);
        transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        //Debug.Log("Checkpointed");
    }
}
