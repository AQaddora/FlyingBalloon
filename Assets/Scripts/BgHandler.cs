using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgHandler : MonoBehaviour
{

    public float scrollSpeed = 10f;
    private void FixedUpdate()
    {
        if (!ChooseCharacter.PlayerManagerInstance.isPlaying)
            return;

        if (transform.position.y <= -45)
        {
            if (gameObject.name.Equals("Background"))
            {
                gameObject.SetActive(false);
                return;
            }
            transform.position = new Vector3(0, 22.5f, 0);
        }
        else
        {
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        }
    }
}
