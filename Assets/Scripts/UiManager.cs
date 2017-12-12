using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This script is responsible for most of the Ui elements management OnClicks and stuff.
public class UiManager : MonoBehaviour
{
    public static UiManager Instance; //making an instance of this script so we can access public variables and methods from other scripts.
    public AudioSource audioSource; //This is the only audio source in the game, we currently play music on it. and we use it to play effects. 
	
	//now these are all the Text components that are gonna be updated during the game.
	public Text scoreView, TimerView, bestTimerView, timerViewLose, timerViewBetween, timerViewWin, bestTimerViewWin;

	public float timer = 0; //here is our timer. it's gonna be increased by one each single second.

	//these are all the Instances of the AlphaUI script on our panels. so we can show and hide panels
	public AlphaUI blackScreen, hudPanel, lostPanel, winPanel, betweenLevelsPanel;

    private string timerString, bestTimerString; //we use this to store the formated string, and use it in all the Texts related

    private void Awake()
    {
		//Here we make sure we get the instances of all the private variabls linking them to the attached components.
        audioSource = GameObject.FindGameObjectsWithTag("DDOL")[0].GetComponent<AudioSource>();
		/*
		 * The line above is to make sure that we get the running audio source.
		 * Remember that in our Don'tDestroyOnLoad script we set the audio source as Undestroyable in order to keep the looping music
		 * satisfying. so we always destroy the new Audio source and get our instance of the old one "the 0th child". 
		 */

		blackScreen.Hide();//Hides the black screen.
        float bestTimer = PlayerPrefs.GetFloat("BestTimer", 0);//now we bring the Best timer so far from our saved data. if it was there. else we get the defaul value 0.

		bestTimerString = string.Format("Best timer so far: {0:00}:{1:00}:{2:00}", bestTimer / 60, bestTimer % 60, (bestTimer % 60 * 100) % 100);
		//the line above is to format our bestTimer like this "00:00:00" using String.Format, for more details google "string.Format C#".

		bestTimerView.text = bestTimerString; //Set the Text's text to the string we formatted.
        TimerView.text = "00:00:00";
        scoreView.text = "Score: 0";
        Instance = this;//Set the instance to this script
    }

    private void FixedUpdate()//FixedUpdate usually get called fixed amount of times in one second. unlike the Update which is called every frame. so it differs from a device to another.
    {
        if (!PlayerManager.Instance.isPlaying)//everything is disabled if the player haven't pressed Play.
            return;
        timer =  Time.time; //timer gets updated as the second we press play, keeps increasing.
        timerString = string.Format("{0:00}:{1:00}:{2:00}", timer / 60, timer % 60, (timer % 60 * 100) % 100); //format the timer to string like "00:00:00"
        TimerView.text = timerString;//set the text to the string we just formatted. this gets updated every part of the second.
    }
    public void UpdateTimer()//we'll call this whenever the player wins. to check if he scored the best timer or not.
    {
        if(timer > PlayerPrefs.GetFloat("BestTimer", 0))//if he passed the previous best timer.
        {
            PlayerPrefs.SetFloat("BestTimer", timer); //set the Best to the new value.
            float bestTimer = PlayerPrefs.GetFloat("BestTimer", 0);//update the value of our string in these 2 lines
            bestTimerString = string.Format("Best timer so far: {0:00}:{1:00}:{2:00}", bestTimer / 60, bestTimer % 60, (bestTimer % 60 * 100) % 100);
        }
    }
    public void Start_OnClick()//when they click on Play.
    {
		Invoke("HideLevelView", 2); //hides the level name after 2 seconds, follow up in LevelHandler
		LevelHandler.Instance.LoadLevelByIndex(0);//Call the load first level in LevelHandler scrit through the Instance we made.
        ChooseCharacter.PlayerManagerInstance.isPlaying = true;//also set the isPlaying to true to start.
    }

	public void NextLevelOnClick()//this is called when he passes a level and presses continue.
	{
		Invoke("HideLevelView", 2); //hides the level name after 2 seconds, follow up in LevelHandler
		PlayerManager.Instance.isPlaying = true;//isPlaying is true again
		LevelHandler.Instance.LoadLevelByIndex(++LevelHandler.levelIndex);//increase the level index then load level by index. using the LevelHandler.
		betweenLevelsPanel.Hide();//Hide the betweenLevelsPanel. and show the hudPanel.
		hudPanel.Show();
	}

	void HideLevelView()//is called from the two methods above after 2 seconds.
	{
		LevelHandler.Instance.HideLevelView();
	}

	//only one way to get out loser! QUIT!
	public void Quit()//calls when quit is pressed 
	{
		Application.Quit();//simply exit the app.

		//or if you were in the editor. just set the EditorApplication.isPlaying to false.
#if Unity_Editor
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}

    public void AudioToggle(bool toggle)//hence the name. this get called on a dynamic bool component like a toggle button
    {
		//since the true puts on the check mark. we set the check mark as a red "X" and reverse the concept of it
		//so true means muted. false means unmuted
        audioSource.volume = toggle ? 0 : 1;//this is simply a single line if statement "if(toggle) volume = 0; else volume = 1;.
    }

    public void UpdateScore(int score)//this is called when we hit a coin. or more particularly when the score is updated to show the updated value. 
	{
        scoreView.text = "Score: " + score;
    }

	public void Lose()//losers come here a lot.
	{
		PlayerManager.Instance.isPlaying = false;//firstly set the isPlaying to false.
		timerViewLose.text = timerString;//show the timer in the lostPanel.
		lostPanel.Show();//show the lost, and hide the hud unneeded tho I think
		hudPanel.Hide();
	}
	public void Win()//rarly gets called. when a weird little boy wins.
	{
		PlayerManager.Instance.isPlaying = false;//same as lost nearly.. no need to explain all of it again. lazy! ;D
		bestTimerViewWin.text = bestTimerString;
		timerViewWin.text = timerString;
		winPanel.Show();
		hudPanel.Hide();
	}

	public void BetweenLevels()//this gets called when he passes a level. specifically in PlayerManagerScript >> Update method. 
	{
		PlayerManager.Instance.isPlaying = false;//still the same things most likely.
		timerViewBetween.text = timerString;
		betweenLevelsPanel.Show();
		hudPanel.Hide();
	}

    public void HomeButton_OnClick()//when he presses home.
    {
        blackScreen.Show();//show the black screen so the it fades to black
        Invoke("Reload", lostPanel.duration);//after going all black, Load the same scene. the invoke waits amount of seconds before executing the method.
	}

    void Reload()//Invoked above
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//reload the same scene, make sure you are using SceneManagement. check out line 5 of this script.
    }
}
