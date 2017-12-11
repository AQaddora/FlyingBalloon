using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadHandler : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
        GameObject[] ddols = GameObject.FindGameObjectsWithTag("DDOL");
        if (ddols.Length > 1)
            Destroy(ddols[1]);
    }
}
