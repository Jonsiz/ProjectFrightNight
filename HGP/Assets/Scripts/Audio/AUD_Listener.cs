using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUD_Listener : MonoBehaviour
    
{
    public AudioSource[] sources;
    public float maxDistance = 10f;
    public float distanceToTarget;
    GameObject[] Props;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Props = GameObject.FindGameObjectsWithTag("Aud_Prop");
        for(int i = 0; i < Props.Length; i++)
        {
            sources[i] = Props[i].GetComponent<AudioSource>();
           
        }
        foreach (AudioSource source in sources)
        {
            distanceToTarget = Vector3.Distance(transform.position, source.transform.position);
            source.volume = 1 - (distanceToTarget / maxDistance);
        }

    }
}
