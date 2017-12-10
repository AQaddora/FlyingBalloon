using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class AlphaUI : MonoBehaviour
{ //I DID A VOICE EXPLAIN OVER THIS. CHECK IT ON THIS LINK: https://1drv.ms/v/s!ArjGRsCYX8tkgepGibjSfpxpGor9oQ
    public float duration; 
	private bool work = false;
	private CanvasGroup cg;
	private int direction = -1;

	void Start () {
		cg = GetComponent<CanvasGroup> ();
    }

	void Update () {
		if (work) {
			cg.alpha += direction*Time.deltaTime/duration;
			cg.blocksRaycasts = cg.alpha>=0.5f;
			work = !(cg.alpha==1 || cg.alpha==0);
		}
	}
	public void Show(){
		direction = 1;
		work = true;
	}
    public void ShowAfterDuration()
    {
        Invoke("Show", duration);
    }
    public void HideAfterDuration()
    {
        Invoke("Hide", duration);
    }

    public void Hide(){
		direction = -1;
		work = true;
	}

    public void Toggle() {
        if(cg.alpha==1) {
            Hide();
        } else {
            Show();
        }
    }
}
