using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUDIOFootstepsScript : MonoBehaviour
{

    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip[] footstepclip;
    void Start()
    {
        source.clip = footstepclip[Random.Range(0, footstepclip.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    public void FootSteps()
    {
        if (!source.isPlaying)
            source.PlayOneShot(source.clip);
    }
}
