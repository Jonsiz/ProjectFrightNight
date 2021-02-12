using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuConroller : MonoBehaviour
{
    public GameObject CreditsMenuUI;
    public GameObject MainMenuUI;

    /// <summary>
    /// Sets credits panel to active and Main Menu panel inactive
    /// </summary>
    public void Awake()
    {
        CreditsMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }

    public void Credits()
    {
        MainMenuUI.SetActive(false);
        CreditsMenuUI.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application is closed");
    }

    /// <summary>
    /// Sets main menu panel to active and credits panel inactive
    /// </summary>
    public void Return()
    {
        CreditsMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }
}

