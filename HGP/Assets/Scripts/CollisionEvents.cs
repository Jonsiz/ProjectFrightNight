using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionEvents : MonoBehaviour
{

    //This was for debugging collisions with the exit door and enemies (which would trigger Application.Quit() or at least a "GameOver" debug log.


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Exit")
        {
            SceneManager.LoadScene("Chris_Main_Menu");
            //Debug.Log("Exit");
        }
        else if (collision.gameObject.tag == "GameOver")
        {
            //Application.Quit();
            //Debug.Log("GameOver");
            //PixelCrushers.SaveSystem.LoadFromSlot(1);
            GetComponent<PlayerController>().enabled = false;
            collision.gameObject.GetComponent<Unit>().enabled = false;
            GameObject.Find("SaveAndLoadObject").GetComponent<SaveAndLoadManager>().LoadingdaGame();
        }
    }

}
