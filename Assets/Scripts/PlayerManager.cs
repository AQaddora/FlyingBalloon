using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance;
    public int score = 0;
	public float speed= 10;
    private int tries = 3;
    public bool isPlaying = false;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
	{
        if (!isPlaying)
            return;
        if (score >= 40)
        {
            LevelHandler.Instance.LoadNextLevel();
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
            Debug.Log(score);
            Destroy(other.gameObject);
        }
        else if (other.name.Contains("star"))
        {
            if (tries == 3)
            {
                Debug.Log("1st try");
            }
            else if (tries == 2)
            {
                score = (int)(score * 0.05f);
                LevelHandler.Instance.LoadSameLevel();
                Debug.Log(score);
            }
            else if (tries == 1)
            {
                score = (int)(score * 0.1f);
                LevelHandler.Instance.LoadPreviousLevel();
                Debug.Log(score);
            }
            tries--;
        }

    }
}
