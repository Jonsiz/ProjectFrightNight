using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Aud_CS_ChangingRoomLoop CSLoop;
   
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }
    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Chris_Main_Menu")
        {
            Play("MM_Theme");
        }
        if (scene.name == "ClothingStore")
        {
            Play("CS_Theme");
            Play("CS_Ambience");
        }
        if(scene.name == "ToyStore")
        {
            Play("TS_Theme");
            Play("TS_Ambience");
        }

    }
   

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        //Footsteps
        if (s.name == "Player_Footstep" && !s.source.isPlaying)
        {
            s.volume = UnityEngine.Random.Range(.7f, 1f);
            s.pitch = UnityEngine.Random.Range(.8f, 1.2f);
            s.source.Play();
            Debug.Log("Playing: " + name + ".");
        }
        //Clothing Store Changing Room
        
        s.source.Play();
        Debug.Log("Playing: " + name + ".");
        
       
        


    }
    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volume / 2f, s.volume / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitch / 2f, s.pitch / 2f));

        s.source.Stop();
    }

}
