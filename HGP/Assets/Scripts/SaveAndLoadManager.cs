using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveAndLoadManager : MonoBehaviour
{
    [SerializeField]
    private GameObject FadePanel;
    void SavingdaGame()
    {

    }

    public void LoadingdaGame()
    {

        PixelCrushers.SaveSystem.LoadFromSlot(1);
    }
}
