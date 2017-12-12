using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is responsible for the falling coins and star, I've named it coin handler then used it also with stars and shields. it's Ok! xD
public class coinHandler : MonoBehaviour {
    private Transform player; //get the player's position through it's transform tho it's steady, we could've just ignored this and compared to -1. see next lines to know what i am talking about.
    public float fallSpeed = 12f;//fall speed >_<

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //Get the player.
    }
    private void FixedUpdate()
    {
        if (!ChooseCharacter.PlayerManagerInstance.isPlaying)//everything is paused until it starts, so if not isPlaying we just return.
            return;
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);//go down....
        if (transform.position.y <= player.position.y - 5)//if we passed way down the player we destroy ourselves.
        {
            if(name.Contains("coin")) //but wait. we are counting the passed coins, the ones that the players couldn't or haven't picked up. so we get knowledge of how far the level is still on.
                ChooseCharacter.PlayerManagerInstance.coinsLeft++;
            Destroy(gameObject);//Destroy anyways tho.
        }
    }
}

