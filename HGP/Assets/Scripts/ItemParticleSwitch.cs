using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class ItemParticleSwitch : MonoBehaviour
{
    private ParticleSystem particle;
    private Color color;
    private Usable usable;
    [SerializeField]
    private float waitTime = 15;
    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip clip;
    //private float fadeSpeed = 0.001f;
    //private float fadeTarget = 0;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip;
        particle = GetComponentInChildren<ParticleSystem>();
        usable = GetComponent<Usable>();
        particle.GetComponent<Renderer>().sortingOrder = 32000;
        ParticleOff();
        //color = particle.startColor;
        //color.a = 1;
        //particle.startColor = color;
        //StartCoroutine("WaitTime");
        //particle.Stop();
    }

    private void Update()
    {
        //color = particle.startColor;

        //if (color.a < fadeTarget)
        //{
        //    color.a += fadeSpeed;
        //}
        //else if (color.a > fadeTarget)
        //{
        //    color.a -= fadeSpeed;
        //}
        //color.a = Mathf.Round((color.a * 10000)) / 10000;
        //Debug.Log(color.a);

        //particle.startColor = color;
    }

    public void ParticleOn()
    {
        //particle.Play();
        //fadeTarget = 1.0f;
        StartCoroutine("WaitTime");
    }

    public void ParticleOff()
    {
        source.loop = false;
        source.Stop();
        particle.Stop();
        //fadeTarget = 0.0f;
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(waitTime);
        source.loop = true;
        source.Play();
        particle.Play();
        //fadeTarget = 1.0f;
    }
}
