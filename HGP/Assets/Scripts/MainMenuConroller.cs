using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

//Controls the main menu. I think someone else made this, don't remember, maybe not.

public class MainMenuConroller : MonoBehaviour
{
    public GameObject CreditsMenuUI;
    public GameObject MainMenuUI;
    //public DatabaseManager letMeSpeakToYourManager;

    /// <summary>
    /// Sets credits panel to active and Main Menu panel inactive
    /// </summary>
    public void Awake()
    {
        //Turns off credits and turns on the menu.
        CreditsMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void PlayGame ()
    {
        //Loads the next scene
        DialogueManager.ResetDatabase();
        PixelCrushers.SaveSystem.ClearSavedGameData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }

    public void Credits()
    {
        //Turns off the menu to show the credits
        MainMenuUI.SetActive(false);
        CreditsMenuUI.SetActive(true);
    }

    public void Quit()
    {
        //Closes the application
        Application.Quit();
        Debug.Log("Application is closed");
    }

    /// <summary>
    /// Sets main menu panel to active and credits panel inactive
    /// </summary>
    public void Return()
    {
        //Turns off credits to return to the main menu.
        CreditsMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }
}

