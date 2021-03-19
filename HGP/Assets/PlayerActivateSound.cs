using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivateSound : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioClip[] clip;
    AudioSource source;
    private bool hasEntered;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip[Random.Range(0, clip.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (hasEntered)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                source.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     if(collision.tag == "Player")
        {
            hasEntered = true;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hasEntered = false;
        }
    }
}
