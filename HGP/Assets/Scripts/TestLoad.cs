﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoad : MonoBehaviour
{
    public GameObject checkpoint;
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            GameObject.Find("SaveAndLoadObject").GetComponent<SaveAndLoadManager>().LoadingdaGame();
        }

        if (Input.GetKeyDown("k"))
        {
            //PixelCrushers.SaveSystem.SaveToSlot(1);
            //GameObject checkpoint = GameObject.Find("SaveCollider");
            checkpoint.GetComponent<CheckpointActivate>().Activate("gazebos");
        }
    }
}