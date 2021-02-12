﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class CameraCutScript : MonoBehaviour
{
    public GameObject dialogueSystem;
    private DialogueSystemController dialogueSystemController;
    //private Predicate<string> predicate = CheckVariable;
    private int testInt = 0;

    private void Awake()
    {
        dialogueSystemController = dialogueSystem.GetComponent<DialogueSystemController>();
    }
    public void Debugging()
    {
        //dialogueSystemController.initialDatabase.variables.Find(predicate);
        testInt += 1;
        dialogueSystemController.initialDatabase.variables[0].InitialValue = Convert.ToString(testInt);
        Debug.Log(dialogueSystemController.initialDatabase.variables[0].InitialValue);
    }

    private static bool CheckVariable(string variable)
    {
        return false;
    }
}
