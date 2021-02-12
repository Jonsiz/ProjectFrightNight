using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUD_Loop : MonoBehaviour
{
    // Start is called before the first frame update
    //This script plays an audio file in a continuous loop.

    //Audio Source and Clip

    [SerializeField]
    private AudioClip clip;
    private AudioSource source;
    //Floats

    [Range(0, 1)]
    [SerializeField]
    private float volume;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.loop = true;
        source.Play();
    }

    private void Update()
    {
        source.volume = volume;
    }

    // Update is called once per frame

}
