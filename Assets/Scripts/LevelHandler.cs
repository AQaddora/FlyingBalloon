using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour {

    public static LevelHandler Instance;

    public Transform coinPrefab;
    public Transform[] starPrefab;
    public static int starIndex = 0;

    public static int levelIndex = 0;
    private Transform parent;

    private void Awake()
    {
        parent = GameObject.Find("Level").transform;
        Instance = this;
        LoadLevelOne();
    }

    public void LoadLevelOne()
    {
        Debug.Log("Level1");
        levelIndex = 0;
        float lastYCoin = 10;
        for (int i = 0; i < 71; i++)
        {
            float yDifCoin = Random.Range(5, 8);
            float xPosCoin = Random.Range(-7, 7);

            Transform coin = (Transform)Instantiate(coinPrefab, new Vector3(xPosCoin, lastYCoin + yDifCoin, 0), Quaternion.identity);
            coin.SetParent(parent);
            lastYCoin += yDifCoin;
        }
        float lastYStar = 5;
        for (int i = 0; i < 120; i++)
        {
            float yDifStar = Random.Range(3, 6);
            float xPosStar = Random.Range(-7, 7);

            Transform star = (Transform) Instantiate(starPrefab[starIndex], new Vector3(xPosStar, lastYStar + yDifStar, 0), Quaternion.identity);
            star.SetParent(parent);
            lastYStar += yDifStar;
        }
    }
    public void LoadLevelTwo()
    {
        Debug.Log("Level2");

        levelIndex = 1;
        float lastYCoin = 10;
        for (int i = 0; i < 71; i++)
        {
            float yDifCoin = Random.Range(5, 8);
            float xPosCoin = Random.Range(-7, 7);

            Transform coin = (Transform) Instantiate(coinPrefab, new Vector3(xPosCoin, lastYCoin + yDifCoin, 0), Quaternion.identity);
            coin.SetParent(parent);
            lastYCoin += yDifCoin;
        }
        float lastYStar = 5;
        for (int i = 0; i < 120; i++)
        {
            float yDifStar = Random.Range(3, 6);
            float xPosStar = Random.Range(-7, 7);
            Transform starNew = starPrefab[starIndex];
            starNew.localScale = Vector3.one * Random.Range(1, 1.5f);
            starNew.GetComponent<coinHandler>().fallSpeed = Random.Range(6, 8);
            Transform star = (Transform) Instantiate(starNew, new Vector3(xPosStar, lastYStar + yDifStar, 0), Quaternion.identity);
            star.SetParent(parent);
            lastYStar += yDifStar;
        }
    }
    public void LoadLevelThree()
    {
        Debug.Log("Level3");

        levelIndex = 2;
        float lastYCoin = 10;
        for (int i = 0; i < 71; i++)
        {
            float yDifCoin = Random.Range(5, 8);
            float xPosCoin = Random.Range(-7, 7);

            Transform coin = (Transform) Instantiate(coinPrefab, new Vector3(xPosCoin, lastYCoin + yDifCoin, 0), Quaternion.identity);
            coin.SetParent(parent);
            lastYCoin += yDifCoin;
        }
        float lastYStar = 5;
        for (int i = 0; i < 120; i++)
        {
            float yDifStar = Random.Range(3, 6);
            float xPosStar = Random.Range(-7, 7);
            Transform starNew = starPrefab[starIndex];
            starNew.localScale = Vector3.one * Random.Range(1, 1.7f);
            starNew.GetComponent<coinHandler>().fallSpeed = Random.Range(8, 10);
            Transform star = (Transform) Instantiate(starNew, new Vector3(xPosStar, lastYStar + yDifStar, 0), Quaternion.identity);
            star.SetParent(parent);
            lastYStar += yDifStar;
        }
    }
    public void LoadSameLevel()
    {
        EraseAllLevels();
        LoadLevelByIndex(levelIndex);
    }
    public void LoadNextLevel()
    {
        EraseAllLevels();
        
        if (levelIndex != 2)
            LoadLevelByIndex(levelIndex + 1);
    }
    public void LoadPreviousLevel()
    {
        EraseAllLevels();

        if (levelIndex != 0)
            LoadLevelByIndex(levelIndex - 1);
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
    public void EraseAllLevels()
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
}

