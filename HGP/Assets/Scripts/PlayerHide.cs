using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This handles the player hiding behind certain objects.

public class PlayerHide : MonoBehaviour
{
    private bool hiding = false;
    public bool Hiding
    {
        get { return hiding; }
    }
    private float originalXpos;
    private float originalYpos;
    private float newXpos;
    private float newYpos;
    private bool hideAction = false;
    private bool leavingHiding = false;
    [SerializeField]
    private float hideSpeed = 15;

    private bool colliding;
    private Rigidbody2D rBD2D;
    private BoxCollider2D bC2D;
    private BoxCollider2D collidingWith;
    private PlayerController pC;
    [SerializeField]
    private GameObject instructionText;
    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip clothingRackEnter;
    [SerializeField]
    AudioClip clothingRackexit;

    void Awake()
    {
        rBD2D = GetComponent<Rigidbody2D>();

        bC2D = GetComponent<BoxCollider2D>();
        pC = GetComponent<PlayerController>();
        rBD2D.useFullKinematicContacts = true;
        colliding = false;
    }

    void FixedUpdate()
    {
        //This adjust how fast the player shifts into the hiding place.
        float step = hideSpeed * Time.deltaTime;

        //If hiding, then the object moves toward teh given position (which is set elswhere in the script)
        if (hiding)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(newXpos, newYpos), step);
            //rBD2D.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(newXpos,newYpos),step));
        }
        else
        {
            //This moves the player to the original position they were at before hidign was set to true.
            if (leavingHiding)
            {
                //transform.position = new Vector2(originalXpos, originalYpos);
                Vector2 originalPosition = new Vector2(originalXpos, originalYpos);
                transform.position = Vector2.MoveTowards(transform.position, originalPosition, step);
                //Once the object reaches the origianl position, the variables turned off when the player hides are turned back on.
                if ((Vector2)transform.position == originalPosition)
                {
                    leavingHiding = false;
                    rBD2D.isKinematic = false;
                    pC.Hidden = false;
                }
            }
        }
        //Debug.Log(hiding + " " + leavingHiding);
    }

    //This is all in the regular Update method due to a quirk with how Unity detects key inputs.
    void Update()
    {
        //Checks if the object is colliding and makes sure that a BoxCollider2D is set.
        if (colliding && collidingWith != null)
        {
            //Checks if this game object's BoxCollider2D is colliding with the other BoxCollider2D
            if (bC2D.IsTouching(collidingWith))
            {
                //When the spacebar is pressed
                if (Input.GetKeyDown("space"))
                {
     
                    //There's a moment when a text UI element appears while the player is hiding. This turns it off. It is turned on in the sequencer somewhere.

                    instructionText.SetActive(false);
                    //Makes sure the object that is being collided with has the Prop tag.
                    if (collidingWith.gameObject.tag == "Prop")
                    {
                    //If the object is already hiding, hiding is turned off and leavingHiding is set to true.

                        if (hiding)
                        {
                            source.clip = clothingRackexit;
                            source.PlayOneShot(source.clip);
                            hiding = false;
                            leavingHiding = true;
                        }
                        //Otherwise, hiding is set to true, the center of the colliding object is set to the position the player is
                        //moving to, the original position of this object is recorded, Kinematic is set to true (collision issue),
                        //and the script for controlling the player is hidden. I don't remember why I didn't use pC.enabled, but there
                        //must have been a reason for it.
                        else
                        {
                            source.clip = clothingRackEnter;
                            source.PlayOneShot(source.clip);
                            hiding = true;
                            pC.Hidden = true;
                            newXpos = collidingWith.gameObject.transform.position.x;
                            newYpos = collidingWith.gameObject.transform.position.y + 0.01f;
                            originalXpos = transform.position.x;
                            originalYpos = transform.position.y;
                            rBD2D.isKinematic = true;
                        }
                    }
                    //hideAction = false;

                }
            }
        }
        //Debug.Log("colliding");
    }

    //This had issues, might be deleted later, isn't doing anything now.
    void OnCollisionStay2D(Collision2D col)
    {
        //if (hideAction)
        //{
        //    if (col.gameObject.tag == "Prop")
        //    {
        //        if (hiding)
        //        {
        //            hiding = false;
        //            leavingHiding = true;
        //        }
        //        else
        //        {
        //            hiding = true;
        //            newXpos = col.gameObject.transform.position.x;
        //            newYpos = col.gameObject.transform.position.y;
        //            originalXpos = transform.position.x;
        //            originalYpos = transform.position.y;
        //            rBD2D.isKinematic = true;
        //        }
        //    }
        //    hideAction = false;
        //}
        //Debug.Log("colliding");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Sets colliding to true and records the game object being collided with when entering a collision.
        //This is in part due to the issues with the method OnCollisionStay2d
        colliding = true;
        collidingWith = col.gameObject.GetComponent<BoxCollider2D>();
        //Debug.Log("enter");
    }

    void OnCollisionExit2D(Collision2D col)
    {
        //Sets colliding to false when leaving a collision. See above.
        colliding = false;
        //Debug.Log("exit");
    }

    void SetHiding(string prop)
    {
        //This is used with the dialogue system sequencer. It automatically hides the player during a dialogue cutscene, and
        //sets all the necessary variables.
        hiding = true;
        var Prop = GameObject.Find(prop);
        newXpos = Prop.gameObject.transform.position.x;
        newYpos = Prop.gameObject.transform.position.y + 0.01f;
        originalXpos = Prop.gameObject.transform.position.x+4;
        originalYpos = Prop.gameObject.transform.position.y;
        collidingWith = Prop.gameObject.GetComponent<BoxCollider2D>();
    }
}