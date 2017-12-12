using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Background scrolling and repeatability.

/*
 * the main idea is to have 2 doublicated backgrounds. and when ever one goes down the view i reset its position to a specific one.
 * above the another one.
 */
public class BgHandler : MonoBehaviour
{
    public float scrollSpeed = 10f;
    private void FixedUpdate()
    {
        if (!ChooseCharacter.PlayerManagerInstance.isPlaying)//return of not playing
            return;

        if (transform.position.y <= -45)
        {
            if (gameObject.name.Equals("Background"))//to disable the first foreground "the one with the tiles" its name is "Background" the others are "BG1" and "BG2"
            {
                gameObject.SetActive(false);//just deactivated for instance, and return.
                return;
            }
            transform.position = new Vector3(0, 22.5f, 0);//move the BG up.
        }
        else
        {
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime); //else we keep scrolling.
        }
    }
}
