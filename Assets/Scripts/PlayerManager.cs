using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Hence the name. manages the player..
public class PlayerManager : MonoBehaviour
{
	public Slider timerForShield;
	bool shieldIsOn = false;
    public static PlayerManager Instance;//make an instance of this script.
    public int score = 0; //SCORE!? WHAT IS SCORE!? >_<
    public int coinsLeft = 0;//need to know this. to add it to score and if the sum is 70. then lose();

	//audio effects
    public AudioClip coinHitEffect, starHitEffect, movementSfx, shieldGetEffect, shieldFinishedSFX;
	public float speed= 10;//speed?
    private int tries = 3;//how money hits do i get. 4 actually xD
    private bool coolDown = true; //a boolean that is to avoid decrease tries in one hit to the star, we set it to false, first time we hit then we reset it after a tiny bit of time.
    public bool isPlaying = false;//the major controller of the game.
    public Image[] triesView;//three hearts on the top of screen.

	private float minX, maxX;
	private void Awake()
    {
        Instance = this;
		minX = Camera.main.ViewportToWorldPoint(Vector3.zero).x + transform.localScale.x;
		maxX = Camera.main.ViewportToWorldPoint(Vector3.one).x - transform.localScale.x;

	}

    void Update()
	{
		if (shieldIsOn)
		{
			timerForShield.GetComponent<AlphaUI>().Show();
			timerForShield.value -= Time.fixedDeltaTime;
			if (timerForShield.value <= 0)
			{
				DestroyShield();
				CancelInvoke();
			}
		}
        if (!isPlaying)//return if blah blah
            return;

        if (score >= 20 && LevelHandler.levelIndex == 0)//first level condition to pass. 40 coins. 
        {
            score = 0;//reset score.

			//Quick tip: if you are on visual studio. right click on any called method in the callee,
			//choose "GoToDefinition" to see where it actually is and what it does try it with the next method "BetweenLevels()".
			UiManager.Instance.BetweenLevels("You completed Level One in " + UiManager.Instance.timerString + " seconds . Press continue to complete your challenge!");//show the between levels. call the Instance of UiManager.
            UiManager.Instance.UpdateScore(score);//update what???!
			LevelHandler.Instance.EraseAllLevels(); //erase to be so sure about it
        }
        else if (score >= 40 && LevelHandler.levelIndex == 1)//same things but lvl 2 50 coins.
        {
			score = 0;
			UiManager.Instance.BetweenLevels("You completed Level Two in "+ UiManager.Instance.timerString +" seconds . Press continue to complete your challenge!");
			UiManager.Instance.UpdateScore(score);
			LevelHandler.Instance.EraseAllLevels(); ;
		}
        else if (score >= 50 && LevelHandler.levelIndex == 2)//level 3 and 60 coins, hard to believe this but they won. xD
        {
			UiManager.Instance.UpdateScore(score);
			UiManager.Instance.UpdateTimer();
			UiManager.Instance.Win();//this is the only place this method is called.. ppl who reach here are not losers at all.
        }

		//we add the score to the ones left check if they reaches 70.. coinsLeft is updated in coin handler.
        if (coinsLeft + score >= 70) //another loser who couldn't catch enough coins to pass
            UiManager.Instance.Lose();


		//if any movement key is down, play the whoosh sound effect 
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            UiManager.Instance.audioSource.PlayOneShot(movementSfx);
        }

		//move using the Input class. just left and right. multiply by speed and Time.deltaTime to settle up 
		//the issues of different platforms, again a device can run up until 60fps, 
		//so Update gets called 60 timer per second. another device runs 30fps.
		//so differences are handled by multiplying by the factor of time.
        transform.Translate (Vector3.right * speed * Time.deltaTime * Input.GetAxis ("Horizontal"));

		//clamp the position of the balloon to stay in the screen.
		transform.position =new Vector3( Mathf.Clamp (transform.position.x,minX, maxX) ,transform.position.y,transform.position.z);
	}

    void OnTriggerEnter2D(Collider2D other)//gets called when the player hits another collider.. NOTE:: the rigidbody2D component is very important for this to be called.
    {
        if (other.name.Contains("coins"))//if it was "Coin", increase the score. play the effect. and destroy the coin
        {
            score++;
            UiManager.Instance.audioSource.PlayOneShot(coinHitEffect);
            UiManager.Instance.UpdateScore(score);
            Destroy(other.gameObject);
        }
        else if (other.name.Contains("star")&coolDown)
        {
			//else if it was a star.. and remember the cooldown bool variable. if they were true, 
			//we set the cooldown to false, Reset it after 0.2f seconds, play the effect, and check tries.
			coolDown = false;
            Invoke("ResetCoolDown", 0.2f);
            UiManager.Instance.audioSource.PlayOneShot(starHitEffect);
            if (tries == 3)//first time do nothing buy destroy.
            {
                triesView[0].GetComponent<CanvasGroup>().alpha = 0.5f;//decrease the alpha of the picture, we could use the color, but canvas group is easier xD
                Destroy(other.transform.parent.gameObject);
            }
            else if (tries == 2)//2nd try..
            {
                score -= (int)Mathf.Round(score * 0.05f);//decrease the score by 5%
                LevelHandler.Instance.LoadSameLevel();//load same level
                UiManager.Instance.UpdateScore(score);

                triesView[1].GetComponent<CanvasGroup>().alpha = 0.5f;//same as above. 
                Destroy(other.transform.parent.gameObject);
            }
            else if (tries == 1)//same as above but 10% and load the first level
            {
                score -= (int)Mathf.Round(score * 0.1f);
                LevelHandler.Instance.LoadFirst();
                Debug.Log(score);
                UiManager.Instance.UpdateScore(score);

                triesView[2].GetComponent<CanvasGroup>().alpha = 0.5f;
                Destroy(other.transform.parent.gameObject);
            }
            tries--;
            if (tries == -1)//LOSER! 4th hit
            {
                UiManager.Instance.Lose();
            }
        }
        else if (other.name.Contains("Shield"))//if the hit obj was Shield
        {
			shieldIsOn = true;
            transform.GetChild(0).gameObject.SetActive(true);//activate the child transform which is always the shield. "it was there all the time xD"
			UiManager.Instance.audioSource.PlayOneShot(shieldGetEffect);//play the effect.
            Invoke("DestroyShield", 5);//Destroy the shield after 5 seconds.
            Destroy(other.gameObject);//Destroy the picked bubble.
        }

    }

    public void DestroyShield()
    {
		shieldIsOn = false;
		timerForShield.GetComponent<AlphaUI>().Hide();
		UiManager.Instance.audioSource.PlayOneShot(shieldFinishedSFX);
        transform.GetChild(0).gameObject.SetActive(false);//Set the thing back false
		timerForShield.value = 5;
	}
	void ResetCoolDown()//R E S E T  C O O L D O W N
    {
        coolDown = true;
    }

	//You might wanna change the icon from PlayerSettings with your icon.
	//also change the bundle identifier << was that spelled correctly?!
	//That was it.
	//Cya soon ;D
}
