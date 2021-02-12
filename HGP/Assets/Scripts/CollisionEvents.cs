using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Exit")
        {
            SceneManager.LoadScene("Chris_Main_Menu");
            Debug.Log("Exit");
        }
        else if (collision.gameObject.tag == "GameOver")
        {
            Application.Quit();
            Debug.Log("GameOver");
        }
    }

}
