using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour {
    public AudioClip preLevelEffect, nextLevelEffect;
    public static LevelHandler Instance;
    public Text levelView;
    public Transform coinPrefab, shieldPrefab;
    public Transform[] starPrefab;
    public int starIndex = 0;

    public static int levelIndex = 0;
    private Transform parent;

    private void Awake()
    {
        parent = GameObject.Find("Level").transform;
        Instance = this;
        starIndex = PlayerPrefs.GetInt("StarIndex", 0);
    }

    public void LoadLevelOne()
    {
        Debug.Log("Level1");
        LevelView("LEVEL 1");
        levelIndex = 0;
        float lastYCoin = 10;
        float lastYShield = 20;
        for (int i = 0; i < 3; i++)
        {
            float yDifCoin = Random.Range(100, 150);
            float xPosCoin = Random.Range(-7, 7);
            Transform shield = (Transform)Instantiate(shieldPrefab, new Vector3(xPosCoin, lastYShield + yDifCoin, 0), Quaternion.identity);
            shield.SetParent(parent);
            lastYShield += yDifCoin;
        }
        for (int i = 0; i < 71; i++)
        {
            float yDifCoin = Random.Range(5, 8);
            float xPosCoin = Random.Range(-7, 7);

            Transform coin = (Transform)Instantiate(coinPrefab, new Vector3(xPosCoin, lastYCoin + yDifCoin, 0), Quaternion.identity);
            coin.SetParent(parent);
            lastYCoin += yDifCoin;
        }
        float lastYStar = 5;
        for (int i = 0; i < 300; i++)
        {
            float yDifStar = Random.Range(5, 8);
            float xPosStar = Random.Range(-7, 7);
            Debug.Log(starIndex);
            Transform star = (Transform) Instantiate(starPrefab[starIndex], new Vector3(xPosStar, lastYStar + yDifStar, 0), Quaternion.identity);
            star.SetParent(parent);
            lastYStar += yDifStar;
        }
    }
    public void LoadLevelTwo()
    {
        Debug.Log("Level2");
        LevelView("LEVEL 2");
        levelIndex = 1;
        float lastYCoin = 10;
        float lastYShield = 20;
        for (int i = 0; i < 3; i++)
        {
            float yDifCoin = Random.Range(100, 150);
            float xPosCoin = Random.Range(-7, 7);
            Transform shield = (Transform)Instantiate(shieldPrefab, new Vector3(xPosCoin, lastYShield + yDifCoin, 0), Quaternion.identity);
            shield.SetParent(parent);
            lastYShield += yDifCoin;
        }
        for (int i = 0; i < 71; i++)
        {
            float yDifCoin = Random.Range(5, 8);
            float xPosCoin = Random.Range(-7, 7);

            Transform coin = (Transform) Instantiate(coinPrefab, new Vector3(xPosCoin, lastYCoin + yDifCoin, 0), Quaternion.identity);
            coin.SetParent(parent);
            lastYCoin += yDifCoin;
        }
        float lastYStar = 5;
        for (int i = 0; i < 300; i++)
        {
            float yDifStar = Random.Range(3, 6);
            float xPosStar = Random.Range(-7, 7);
            Transform starNew = starPrefab[starIndex];
            Transform star = (Transform) Instantiate(starNew, new Vector3(xPosStar, lastYStar + yDifStar, 0), Quaternion.identity);
            star.localScale = Vector3.one * Random.Range(1, 1.4f);
            star.GetComponentInChildren<coinHandler>().fallSpeed = 7;
            star.SetParent(parent);
            lastYStar += yDifStar;
        }
    }
    public void LoadLevelThree()
    {
        Debug.Log("Level3");
        LevelView("LEVEL 3");
        levelIndex = 2;
        float lastYCoin = 10;
        float lastYShield = 20;
        for (int i = 0; i < 3; i++)
        {
            float yDifCoin = Random.Range(100, 150);
            float xPosCoin = Random.Range(-7, 7);
            Transform shield = (Transform)Instantiate(shieldPrefab, new Vector3(xPosCoin, lastYShield + yDifCoin, 0), Quaternion.identity);
            shield.SetParent(parent);
            lastYShield += yDifCoin;
        }
        for (int i = 0; i < 71; i++)
        {
            float yDifCoin = Random.Range(5, 8);
            float xPosCoin = Random.Range(-7, 7);

            Transform coin = (Transform) Instantiate(coinPrefab, new Vector3(xPosCoin, lastYCoin + yDifCoin, 0), Quaternion.identity);
            coin.SetParent(parent);
            lastYCoin += yDifCoin;
        }
        float lastYStar = 5;
        for (int i = 0; i < 300; i++)
        {
            float yDifStar = Random.Range(3, 6);
            float xPosStar = Random.Range(-7, 7);
            Transform starNew = starPrefab[starIndex];
            Transform star = (Transform) Instantiate(starNew, new Vector3(xPosStar, lastYStar + yDifStar, 0), Quaternion.identity);
            star.localScale = Vector3.one * Random.Range(1.2f, 1.7f);
            star.GetComponentInChildren<coinHandler>().fallSpeed = 9;
            star.SetParent(parent);
            lastYStar += yDifStar;
        }
    }
    public void LoadSameLevel()
    {
        UiManager.Instance.audioSource.PlayOneShot(preLevelEffect);

        EraseAllLevels();
        LoadLevelByIndex(levelIndex);
    }
    public void LoadNextLevel()
    {
        UiManager.Instance.audioSource.PlayOneShot(nextLevelEffect);

        EraseAllLevels();
        if (levelIndex != 2)
            LoadLevelByIndex(levelIndex + 1);
    }
    public void LoadFirst()
    {
        UiManager.Instance.audioSource.PlayOneShot(preLevelEffect);

        EraseAllLevels();
        LoadLevelOne();
    }
    public void LoadLevelByIndex(int index)
    {
        if (index == 0)
            LoadLevelOne();
        else if (index == 1)
            LoadLevelTwo();
        else if (index == 2)
            LoadLevelThree();
    }
    public void LevelView(string s)
    {
        levelView.text = s;
        levelView.GetComponent<AlphaUI>().Show();
        Invoke("HideLevelView", 0.5f);
    }
    public void HideLevelView()
    {
        levelView.GetComponent<AlphaUI>().Hide();
    }
    public void EraseAllLevels()
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    public void ChooseStar(int index)
    {
        starIndex = index;
        PlayerPrefs.SetInt("StarIndex", index);
    }
}

