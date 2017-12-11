using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance;
    public int score = 0;
    public int coinsLeft = 0;
    public AudioClip coinHitEffect, starHitEffect, movementSfx;
	public float speed= 10;
    private int tries = 3;
    private bool coolDown = true;
    public bool isPlaying = false;
    public Image[] triesView;

    private void Awake()
    {
        Instance = this;
    }
    void Update()
	{
        if (!isPlaying)
            return;
        if (score >= 40 && LevelHandler.levelIndex == 0)
        {
            score = 0;
            UiManager.Instance.UpdateScore(score);
            LevelHandler.Instance.LoadNextLevel();
        }
        else if (score >= 50 && LevelHandler.levelIndex == 1)
        {
            score = 0;
            UiManager.Instance.UpdateScore(score);
            LevelHandler.Instance.LoadNextLevel();
        }
        else if (score >= 60 && LevelHandler.levelIndex == 2)
        {
            UiManager.Instance.EndShow();
            UiManager.Instance.UpdateScore(score);
            UiManager.Instance.UpdateTimer();
        }

        if (coinsLeft + score >= 70)
            UiManager.Instance.EndShow();

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            UiManager.Instance.audioSource.PlayOneShot(movementSfx);
        }
        transform.Translate (Vector3.right * speed * Time.deltaTime * Input.GetAxis ("Horizontal"));
		transform.position =new Vector3( Mathf.Clamp (transform.position.x,-7.5f,7.5f) ,transform.position.y,transform.position.z);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name + "     " + other.name.Contains("coins"));
        if (other.name.Contains("coins"))
        {
            score++;
            UiManager.Instance.audioSource.PlayOneShot(coinHitEffect);
            UiManager.Instance.UpdateScore(score);
            Debug.Log(score);
            Destroy(other.gameObject);
        }
        else if (other.name.Contains("star")&coolDown)
        {
            coolDown = false;
            Invoke("ResetCoolDown", 0.2f);
            UiManager.Instance.audioSource.PlayOneShot(starHitEffect);
            if (tries == 3)
            {
                Debug.Log("1st try");
                triesView[0].GetComponent<CanvasGroup>().alpha = 0.5f;
                Destroy(other.transform.parent.gameObject);
            }
            else if (tries == 2)
            {
                Debug.Log(score);
                score -= (int)Mathf.Round(score * 0.05f);
                LevelHandler.Instance.LoadSameLevel();
                Debug.Log(score);
                UiManager.Instance.UpdateScore(score);

                triesView[1].GetComponent<CanvasGroup>().alpha = 0.5f;
                Destroy(other.transform.parent.gameObject);
            }
            else if (tries == 1)
            {
                score -= (int)Mathf.Round(score * 0.1f);
                LevelHandler.Instance.LoadFirst();
                Debug.Log(score);
                UiManager.Instance.UpdateScore(score);

                triesView[2].GetComponent<CanvasGroup>().alpha = 0.5f;
                Destroy(other.transform.parent.gameObject);
            }
            tries--;
            if (tries == -1)
            {
                UiManager.Instance.EndShow();
            }
        }
        else if (other.name.Contains("Shield"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Invoke("DestroyShield", 5);
            Destroy(other.gameObject);
        }

    }

    public void DestroyShield()
    {
        CancelInvoke();
        transform.GetChild(0).gameObject.SetActive(false);
    }
    void ResetCoolDown()
    {
        coolDown = true;
    }
}
