using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDepth : MonoBehaviour
{
    [Tooltip("NOTE: This script should be applied to any object with a sprite. Bounding boxes should also be not as tall as the actual sprite, preferably.")]
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        //This sets the sorting order to an inverse of the y position (times 100 in order provide a larger difference between rounded numbers).
        //In other words, objects further down will be drawn on top of objects higher up, to simulate the isometric look.
        //I also recommend that the bounding box of sprites not be as tall as the actual sprite, and lowered accordingly.
        spriteRenderer.sortingOrder = 10000 - Mathf.RoundToInt(transform.position.y * 100);
    }
}
