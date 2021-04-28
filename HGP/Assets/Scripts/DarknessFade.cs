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
    [SerializeField]
    private Color color;
    private float fadeSpeed = 0.01f;
    [SerializeField]
    private PlayerController childStamina;
    [SerializeField]
    private Color exhaustionColor;
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
        color.a = image.color.a;
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
        exhaustionColor.a = color.a;

        if (childStamina.Exhausted)
        {
            image.color = exhaustionColor;
        }
        else
        {
            image.color = color;
        }

        //Debug.Log(childStamina.Exhausted);
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
