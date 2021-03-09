using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParticleSwitch : MonoBehaviour
{
    private ParticleSystem particle;
    private Color color;
    private float fadeSpeed = 0.001f;
    private float fadeTarget = 0;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        ParticleOff();
        //color = particle.startColor;
        //color.a = 1;
        //particle.startColor = color;
        //StartCoroutine("WaitTime");
        //particle.Stop();
    }

    private void Update()
    {
        color = particle.startColor;

        //if (color.a < fadeTarget)
        //{
        //    color.a += fadeSpeed;
        //}
        //else if (color.a > fadeTarget)
        //{
        //    color.a -= fadeSpeed;
        //}
        color.a = Mathf.Round((color.a * 10000)) / 10000;
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
        particle.Stop();
        fadeTarget = 0.0f;
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(5);

        particle.Play();
        fadeTarget = 1.0f;
    }
}
