using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This sets the opacity of a GUI element that fades based on the player's stamina. Ideally. Currently it's not implemented,
//it will probably be built on later if time permits.

public class DarknessFade : MonoBehaviour
{
    private Image image;
    private float fadeGoal;
    private Color color;
    private float fadeSpeed = 0.01f;
    [SerializeField]
    private PlayerController childStamina;
    void Start()
    {
        image = GetComponent<Image>();
        fadeGoal = 0;
        color = image.color;
        color.a = 0;
        image.color = color;
    }

    void FixedUpdate()
    {
        //Sets the alpha of the image. See above. Originally it would just fade in or out as an environmental effect.
        color = image.color;
        //if (color.a < fadeGoal)
        //{
        //    color.a += fadeSpeed;
        //}
        //else if (color.a > fadeGoal)
        //{
        //    color.a -= fadeSpeed;
        //}
        var exhaustion = 1.0f - childStamina.Stamina / childStamina.MaxStamina;
        color.a = exhaustion;
        color.a = Mathf.Round(color.a * 100f) / 100f;
        image.color = color;
    }

    void Update()
    {
        //if (Input.GetKeyDown("k"))
        //{
        //    if (fadeGoal == 1)
        //    {
        //        fadeGoal = 0;
        //    }
        //    else if (fadeGoal == 0)
        //    {
        //        fadeGoal = 1;
        //    }
        //}
    }
}
