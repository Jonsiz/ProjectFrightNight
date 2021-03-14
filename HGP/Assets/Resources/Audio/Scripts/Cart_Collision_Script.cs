using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart_Collision_Script : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource source;
    [SerializeField]
    AudioClip[] clip;
    float volume;
    float pitch;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip[Random.Range(0, clip.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with" + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            
            volume = Random.Range(.7f, 1f);
            pitch = Random.Range(.7f, 1.2f);
            source.volume = volume;
            source.pitch = pitch;
            source.Play();
            Debug.Log("Sound Played");
        }
    }
}
