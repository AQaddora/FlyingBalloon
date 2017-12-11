using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    int hits = 2;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("star"))
        {
            Debug.Log(other.name);
            hits--;
            Destroy(other.gameObject);
            if(hits <= 0)
            {
                PlayerManager.Instance.DestroyShield();
            }
        }
    }
}
