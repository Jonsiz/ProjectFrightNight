using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUDIOFootstepsScript : MonoBehaviour
{

    void Start()
    {
        //Getting the audiosource component from the footstep sound object. 
      
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    public void FootSteps()
    {
        FindObjectOfType<AudioManager>().Play("Player_Footstep");
    }
}
