using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MusicManager : MonoBehaviour
{
    public AudioClip MM_THEME;
    public AudioClip CS_THEME;
    public AudioClip TS_THEME;
    public AudioClip TS_HORROR_Theme;
    public AudioMixerSnapshot Normal;
    public AudioMixerSnapshot NormalOff;
    public bool normalOn;
    public AudioSource source1;
    
    public AudioSource source2;

    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        normalOn = true;
        
        scene = SceneManager.GetActiveScene();
        ChooseLevelTrack(scene.name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            source2.clip = TS_THEME;
            source2.Play();
            Switch();
        }
    }
    void ChooseLevelTrack(string name)
    {
        if(name == "Chris_MainMenu")
        {
            source1.clip = MM_THEME;
            source1.Play();
        }
        if(name == "ClothingStore")
        {
            source1.clip = CS_THEME;
            source1.Play();
        }
        if(name == "ToyStore")
        {
            source1.clip = TS_THEME;
        }
        if(name == "ToyStoreHorror")
        {
            source1.clip = TS_HORROR_Theme;
            source1.Play();
        }
    }
    
    public void Switch()
    {
        //Time.timeScale = Time.timeScale;
        
        if (normalOn)
        {
            
            Debug.Log("Switch normal off");
            NormalOff.TransitionTo(.5f);
            
        }
        if (!normalOn)
        {
            Debug.Log("Switch to normal On.");
            Normal.TransitionTo(.5f);
            
        }
        normalOn = !normalOn;
    }
    
    

}
