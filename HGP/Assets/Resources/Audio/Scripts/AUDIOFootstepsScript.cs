using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUDIOFootstepsScript : MonoBehaviour
{
    // Start is called before the first frame update
    //Where the audio plays from. 
    AudioSource source;
    //An array for footstep sound clips. You can add as many or take as away as many you'd like in the editor. 
    [SerializeField]
    private AudioClip[] footstep;
    //Bools
    bool walking = false;
    //Floats
    [SerializeField]
    private float pitchMin = .75f;
    [SerializeField]
    private float pitchMax = 1.5f;
    private float pitch;

    void Start()
    {
        //Getting the audiosource component from the footstep sound object. 
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    public void FootSteps()
    {
        if (!source.isPlaying)
        {
            // Sets audioclip
            source.clip = footstep[Random.Range(0, footstep.Length)];
            //Instantiating new float for pitch using a random.range to calculate between to variables (for variation)
            pitch = Random.Range(pitchMin, pitchMax);
            //Sets the pitch to the float
            source.pitch = pitch;
            //Plays the footstep. 
            source.Play();
        }
    }
}
