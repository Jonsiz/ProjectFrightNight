using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class ClearOnAwake : MonoBehaviour
{
    private DialogueSystemController controller;
    private DatabaseManager manager;
    void Awake()
    {
        //Resets variables in the dialogue manger.

        //controller = GetComponent<DialogueSystemController>();
        //controller.ResetDatabase(DatabaseResetOptions.RevertToDefault);
        //DatabaseManager.Reset(DatabaseResetOptions.KeepAllLoaded);

        //This command is not in the documentation to my knowledge :/ I demand a raise.
        //DialogueManager.ResetDatabase();
        //Debug.Log("Controller Awake");
    }
}