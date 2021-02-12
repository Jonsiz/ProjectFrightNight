using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
   // public AudioSource musicSource;
    public AudioSource buttonSource;
   // public AudioClip music;
    public AudioClip buttonClick;
    public AudioClip buttonHover;
    void Start()
    {
       // musicSource = GetComponent<AudioSource>();
        buttonSource = GetComponent<AudioSource>();
       // musicSource.clip = music;
       // musicSource.loop = true;
       // musicSource.Play();
    }

    public void Hover()
    {
        buttonSource.clip = buttonHover;
        buttonSource.Play();
    }

    public void Click()
    {
        buttonSource.clip = buttonClick;
        buttonSource.Play();
    } 


}
