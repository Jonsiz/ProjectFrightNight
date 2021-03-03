using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoad : MonoBehaviour
{
    public GameObject checkpoint;
    void Update()
    {
        //if (Input.GetKeyDown("h"))
        //{
        //    Debug.Log(GameObject.Find("SecurityGuard").GetComponent<EnemyAI>().testLoad);
        //}
        //if (Input.GetKeyDown("j"))
        //{
        //    GameObject.Find("SecurityGuard").GetComponent<EnemyAI>().testLoad = 100;
        //}

        if (Input.GetKeyDown("l"))
        {
            GameObject.Find("SaveAndLoadObject").GetComponent<SaveAndLoadManager>().LoadingdaGame();
        }

        if (Input.GetKeyDown("k"))
        {
            //PixelCrushers.SaveSystem.SaveToSlot(1);
            //GameObject checkpoint = GameObject.Find("SaveCollider");
            checkpoint.GetComponent<CheckpointActivate>().ActivateCheckpoint();
        }
    }
}
