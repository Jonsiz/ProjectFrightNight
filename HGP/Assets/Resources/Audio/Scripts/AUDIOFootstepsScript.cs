using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUDIOFootstepsScript : MonoBehaviour
{

    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip[] concretefootstepclip;
    [SerializeField] 
    AudioClip[] carpetFootstepClip;
    string material;
    void Start()
    {
        material = "concrete";
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    public void FootSteps()
    {
        if (!source.isPlaying && material == "concrete") 
        { 
        source.clip = concretefootstepclip[Random.Range(0, concretefootstepclip.Length)];
        source.PlayOneShot(source.clip); 
        }
        if (!source.isPlaying && material == "carpet")
        {
            source.clip = carpetFootstepClip[Random.Range(0, carpetFootstepClip.Length)];
            source.PlayOneShot(source.clip);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Aud_Carpet")
            material = "carpet";
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Aud_Carpet")
        {
            material = "concrete";
        }
    }
}
