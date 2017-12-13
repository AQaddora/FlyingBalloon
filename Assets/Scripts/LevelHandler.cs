using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour {
    public AudioClip preLevelEffect, nextLevelEffect; //sound effects, notice the name..
    public static LevelHandler Instance;//Makes an instance of this class
    public Text levelView; //a Text that will show the level.
    public Transform coinPrefab, shieldPrefab; //our prefabs
    public Transform[] starPrefab; //still a prefab tho 3 instead of one, we will be generating one of them using the index of array. follow up the upcoming comments.
    public int starIndex = 0;//the index just like in ChooseCharacter script.

    public static int levelIndex = 0;//this is the currently loaded level index. currently 0
    private Transform parent;//we will parent every thing we create to this parent, so we can erase every thing and start over so fast.

    private void Awake()
    {
        parent = GameObject.Find("Level").transform;//Find the Level empty game object to parent it to the new created objects.
        Instance = this;
        starIndex = PlayerPrefs.GetInt("StarIndex", 0);// get the saved star index if there was, or get the default one which is number 0 in the array.
    }

	//Load level one.
    public void LoadLevelOne()
    {
        LevelView("LEVEL 1");//shows the levelView text that says "LEVEL 1".
        levelIndex = 0;//currently loading one so level index is set 0.
        float lastYCoin = 10;//the first possiple y positin. for a coin.
        float lastYShield = 20;//the first possiple y positin. for a shield.
		for (int i = 0; i < 3; i++)//create three shields in a random positing.
        {
            float yDifCoin = Random.Range(100, 150); //randomize the y
            float xPosCoin = Random.Range(-7, 7); // and the X 
            Transform shield = (Transform)Instantiate(shieldPrefab, new Vector3(xPosCoin, lastYShield + yDifCoin, 0), Quaternion.identity);
            shield.SetParent(parent);//the line above instantiates and instance of the prefab and position it at that position.
            lastYShield += yDifCoin;//the new possiple position of our next shield
        }
        for (int i = 0; i < 81; i++)//same as shields but larger number
        {
            float yDifCoin = Random.Range(5, 8);
            float xPosCoin = Random.Range(-7, 7);

            Transform coin = (Transform)Instantiate(coinPrefab, new Vector3(xPosCoin, lastYCoin + yDifCoin, 0), Quaternion.identity);
            coin.SetParent(parent);
            lastYCoin += yDifCoin;
        }
        float lastYStar = 5;
        for (int i = 0; i < 300; i++)//same as above
        {
            float yDifStar = Random.Range(5, 8);
            float xPosStar = Random.Range(-7, 7);
            Debug.Log(starIndex);
            Transform star = (Transform) Instantiate(starPrefab[starIndex], new Vector3(xPosStar, lastYStar + yDifStar, 0), Quaternion.identity);
            star.SetParent(parent);
            lastYStar += yDifStar;
        }
    }

	//in the upcoming methods we just scale stars up a bit randomly and we set the fall speed to some ather amount. otherwise it stays the same as levelone.
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
            float yDifStar = Random.Range(4, 7);
            float xPosStar = Random.Range(-7, 7);
            Transform starNew = starPrefab[starIndex];
            Transform star = (Transform) Instantiate(starNew, new Vector3(xPosStar, lastYStar + yDifStar, 0), Quaternion.identity);
            star.localScale = Vector3.one * Random.Range(1, 1.4f);
            star.GetComponentInChildren<coinHandler>().fallSpeed = 6;
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
        for (int i = 0; i < 61; i++)
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
            star.GetComponentInChildren<coinHandler>().fallSpeed = 7;
            star.SetParent(parent);
            lastYStar += yDifStar;
        }
    }

	//to load the same running level again, we erase everything then start from scratch.
    public void LoadSameLevel()
    {
		Invoke("HideLevelView", 2);
		UiManager.Instance.audioSource.PlayOneShot(preLevelEffect);//play the effect of prelevel.
        EraseAllLevels();
        LoadLevelByIndex(levelIndex);//call another method.
    }
    public void LoadNextLevel()
    {
        UiManager.Instance.audioSource.PlayOneShot(nextLevelEffect);//play the effect.
		Invoke("HideLevelView", 2);
		EraseAllLevels();
        if (levelIndex != 2) //check if we aren't already at the last level.
            LoadLevelByIndex(levelIndex + 1);//if we aren't load the level that is next.
    }

	//loads the first level with the sound effect a loser likes.
    public void LoadFirst()
    {
		Invoke("HideLevelView", 2);
        UiManager.Instance.audioSource.PlayOneShot(preLevelEffect);//play the effect.

        EraseAllLevels();
        LoadLevelOne();
    }

	//calls the other methods the first three methods above after the awake, based on an index.
    public void LoadLevelByIndex(int index)
    {
		//a typical switch : case
        if (index == 0)
            LoadLevelOne();
        else if (index == 1)
            LoadLevelTwo();
        else if (index == 2)
            LoadLevelThree();
    }

	//shows the level name.
    public void LevelView(string s)
    {
        levelView.text = s;
        levelView.GetComponent<AlphaUI>().Show();
    }

	//Hides the level name and is called from UiManager OnStartClick, NextLevelOnClick as well.
    public void HideLevelView()
    {
        levelView.GetComponent<AlphaUI>().Hide();
    }

    public void EraseAllLevels()//Delete everything in the level folder.
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    public void ChooseStar(int index)//is put on the stars icons in the settings panel just as the characters.
    {
        starIndex = index;
        PlayerPrefs.SetInt("StarIndex", index);//set the index of the chosen one just like charIndex. in ChooseCharacter.
    }
}

