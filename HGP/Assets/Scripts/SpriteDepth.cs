using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDepth : MonoBehaviour
{
    //Wow. The one time I thought to use a Unity tooltip. I wonder how many people have seen it.
    [Tooltip("NOTE: This script should be applied to any object with a sprite. Bounding boxes should also be not as tall as the actual sprite, preferably.")]
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        //This sets the sprite sorting order to an inverse of the y position (times 100 in order provide a larger difference between rounded numbers).
        //In other words, objects further down will be drawn on top of objects higher up, to simulate the isometric look.
        //This has called for adjusting hitboxes and origin points of sprites, and it also doesn't work with sprite layers. There might be
        //a better solution, but it works for now.
        spriteRenderer.sortingOrder = 10000 - Mathf.RoundToInt(transform.position.y * 100);
    }
}
