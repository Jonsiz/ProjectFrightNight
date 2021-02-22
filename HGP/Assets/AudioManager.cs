using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public int RandomTime;
    public float timer;
    [SerializeField]
    private GameObject CS_CR_Source01;
    [SerializeField]
    private GameObject CS_CR_Source02;
    private int randomNumber;
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

    }
    public void Update()
    {
        if(timer > RandomTime)
        {
            RandomTime = UnityEngine.Random.Range(60, 180);
            timer = 0.0f;
            randomNumber = UnityEngine.Random.Range(0, 1);
            if(randomNumber == 0)
            {
                Play("CS_CR_ClothingFold");
            }
            if (randomNumber == 1)
            {
                Play("CS_CR_Zipper");
            }

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
        if(s.name == "CS_CR_ClothingFold" || (s.name == "CS_CR_Zipper"))
        {
            randomNumber = UnityEngine.Random.Range(0, 1);
            if (randomNumber == 0)
            {
                s.source = CS_CR_Source01.GetComponent<AudioSource>();
                s.volume = UnityEngine.Random.Range(.7f, 1f);
                s.pitch = UnityEngine.Random.Range(.8f, 1.2f);
                s.source.Play();
                Debug.Log("Playing: " + name + ".");
            }
            if (randomNumber == 1)
            {
                s.source = CS_CR_Source02.GetComponent<AudioSource>();
                s.volume = UnityEngine.Random.Range(.7f, 1f);
                s.pitch = UnityEngine.Random.Range(.8f, 1.2f);
                s.source.Play();
                Debug.Log("Playing: " + name + ".");
            }
        }
        
        s.source.Play();
        Debug.Log("Playing: " + name + ".");
        
       
        


    }

}
