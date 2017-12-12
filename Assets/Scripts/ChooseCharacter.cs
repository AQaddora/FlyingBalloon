using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class is responsible for the character swapping..

/*
* basically the idea of this is to deactivate all the children of this transform, and then set only one back on
* the chosen one is decided by this script, hence the charindex variable >> stands for character index.
* in the awake function we just deactivate all of them using a for loop.
* we will use playerprefs class. to save our selected character in the game's data on the platform.
* 
* read the comments.
*/
public class ChooseCharacter : MonoBehaviour
{
    public static PlayerManager PlayerManagerInstance;
    public int charIndex = 0; //this is what we are going to change to swap between characters.
    private void Awake()
    {
        charIndex = PlayerPrefs.GetInt("CharIndex", 0);
		/*getting the saved selected character from previous runs,
		 * first time it won't be set so we put the default as 0.
		 * for more details google "Playerprefs Unity"
		*/

        for (int i = 0; i < transform.childCount; i++)//loop through all the children of the attached-to game object.
        {
            transform.GetChild(i).gameObject.SetActive(false);//set every child to false.
        }
        transform.GetChild(charIndex).gameObject.SetActive(true); //set the only chosen child to be back on
		
		//set the instance of PlayerManager script to be the activated one.
		PlayerManagerInstance = transform.GetChild(charIndex).GetComponent<PlayerManager>(); 
    }
	
	
	/*
	 * this where the magic happens. xD We call this function on the choose button. it updates the index,
	 * and go through all the previous steps again.
	 * "deactivating all and activating only the chosen one".
	 */
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
