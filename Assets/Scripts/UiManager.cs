using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public void Start_OnClick()
    {
        PlayerManager.Instance.isPlaying = true;
    }
}
