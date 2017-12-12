using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to keep the Audio source and not generate a new one.
public class DontDestroyOnLoadHandler : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);//for more details google "Don't Destroy On Load unity".
        GameObject[] ddols = GameObject.FindGameObjectsWithTag("DDOL");//a FYI >> DDOL stands for "Don't Destroy On Load"
		if (ddols.Length > 1)
            Destroy(ddols[1]);//destroy the new one.
    }
}
