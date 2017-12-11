using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacter : MonoBehaviour
{
    public static PlayerManager PlayerManagerInstance;
    public int charIndex = 0;
    private void Awake()
    {
        charIndex = PlayerPrefs.GetInt("CharIndex", 0);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(charIndex).gameObject.SetActive(true);
        PlayerManagerInstance = transform.GetChild(charIndex).GetComponent<PlayerManager>();
    }

    public void Choose(int index)
    {
        PlayerPrefs.SetInt("CharIndex", index);
        charIndex = index;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(charIndex).gameObject.SetActive(true);
        PlayerManagerInstance = transform.GetChild(charIndex).GetComponent<PlayerManager>();
    }
}
