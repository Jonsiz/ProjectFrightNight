using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUD_PlayOneShot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
