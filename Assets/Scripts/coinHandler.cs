using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinHandler : MonoBehaviour {

    private Transform player;
    public float fallSpeed = 12f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.isPlaying)
            return;
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        if (transform.position.y <= player.position.y - 5)
        {
            Destroy(gameObject);
        }
    }
}

