using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParticleSwitch : MonoBehaviour
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        ParticleOff();
        StartCoroutine("WaitTime");
        //particle.Stop();
    }

    public void ParticleOn()
    {
        particle.Play();
    }

    public void ParticleOff()
    {
        particle.Stop();
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(60);

        ParticleOn();
    }
}
