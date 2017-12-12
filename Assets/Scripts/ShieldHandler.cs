using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)//if the shield hits a star it destroys it.
    {
        if (other.name.Contains("star"))
        {
            Destroy(other.gameObject);
        }
    }
}
