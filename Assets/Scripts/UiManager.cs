using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;
    public AudioSource audioSource;
    public Text scoreView, TimerView, bestTimerView, timerViewLose, timerViewBetween, timerViewWin, bestTimerViewWin;
    public float timer = 0;
    public AlphaUI hudPanel, lostPanel, winPanel, betweenLevelsPanel;
    public AlphaUI blackScreen;
    private string timerString, bestTimerString;

    private void Awake()
    {
        audioSource = GameObject.FindGameObjectsWithTag("DDOL")[0].GetComponent<AudioSource>();
        blackScreen.Hide();
        float bestTimer = PlayerPrefs.GetFloat("BestTimer", 0);
        bestTimerString = string.Format("Best timer so far: {0:00}:{1:00}:{2:00}", bestTimer / 60, bestTimer % 60, (bestTimer % 60 * 100) % 100);
        bestTimerView.text = bestTimerString;
        TimerView.text = "0";
        scoreView.text = "Score: 0";
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.Instance.isPlaying)
            return;
        timer =  Time.time;
        timerString = string.Format("{0:00}:{1:00}:{2:00}", timer / 60, timer % 60, (timer % 60 * 100) % 100);
        TimerView.text = timerString;
    }
    public void UpdateTimer()
    {
        if(timer > PlayerPrefs.GetFloat("BestTimer", 0))
        {
            Debug.Log("newBest");
            PlayerPrefs.SetFloat("BestTimer", timer);
            float bestTimer = PlayerPrefs.GetFloat("BestTimer", 0);
            bestTimerString = string.Format("Best timer so far: {0:00}:{1:00}:{2:00}", bestTimer / 60, bestTimer % 60, (bestTimer % 60 * 100) % 100);
        }
    }
    public void Start_OnClick()
    {
        LevelHandler.Instance.LoadLevelByIndex(0);
        ChooseCharacter.PlayerManagerInstance.isPlaying = true;
    }

	public void NextLevelOnClick()
	{
		PlayerManager.Instance.isPlaying = true;
		LevelHandler.Instance.LoadLevelByIndex(++LevelHandler.levelIndex);
		betweenLevelsPanel.Hide();
		hudPanel.Show();
	}

	public void Quit()
	{
		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}

    public void AudioToggle(bool toggle)
    {
        audioSource.volume = toggle ? 0 : 1;
    }

    public void UpdateScore(int score)
    {
        scoreView.text = "Score: " + score;
    }

	public void Lose()
	{
		PlayerManager.Instance.isPlaying = false;
		timerViewLose.text = timerString;
		lostPanel.Show();
		hudPanel.Hide();
	}
	public void Win()
	{
		Debug.Log("YouWon!!");
		PlayerManager.Instance.isPlaying = false;
		bestTimerViewWin.text = bestTimerString;
		timerViewWin.text = timerString;
		winPanel.Show();
		hudPanel.Hide();
	}

	public void BetweenLevels()
	{
		PlayerManager.Instance.isPlaying = false;
		timerViewBetween.text = timerString;
		betweenLevelsPanel.Show();
		hudPanel.Hide();
	}

    public void HomeButton_OnClick()
    {
        blackScreen.Show();
        Invoke("Reload", lostPanel.duration);
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
