using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_CS_ChangingRoomLoop : MonoBehaviour
{
    // Start is called before the first frame update
    public int RandomTime;
    public float timer;
    private int randomNumber;
    private AudioSource source;
    [SerializeField]
    private AudioClip[] fold;
    [SerializeField]
    private AudioClip[] zipper;
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > RandomTime)
        {
            RandomTime = UnityEngine.Random.Range(0, 5);
            timer = 0.0f;
            ChooseSound();
            source.Play();
}
        
        timer += Time.deltaTime;
        
    }

    private void ChooseSound()
    {
        randomNumber = UnityEngine.Random.Range(0, 2);
        if (randomNumber == 0 && !source.isPlaying)
        {
            randomNumber = Random.Range(0, fold.Length);
            //Debug.Log("Fold");
            source.clip = fold[randomNumber];
            

        }
        if (randomNumber == 1 && !source.isPlaying)
        {
            //Debug.Log("Zipper");
            source.clip = zipper[randomNumber];
            

        }
    }
}
