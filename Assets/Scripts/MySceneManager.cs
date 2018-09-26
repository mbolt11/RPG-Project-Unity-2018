using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour 
{
    //Create static script variable
    public static MySceneManager mySceneManager;

    //Variable to hold if the game has started already
    bool gameStart;
	
	void Awake () 
    {
        if(!gameStart)
        {
            //Set sceneManager to be this script
            mySceneManager = this;

            //Load scene 1, which is the overworld
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

            //Show the game has started
            gameStart = true;
        }
		
	}
	
	// Method to unload a scene
	public void UnloadScene(int scene) 
    {
        StartCoroutine(Unload(scene));
	}

    //Coroutine for Unload Scene
    IEnumerator Unload(int scene)
    {
        yield return null;

        SceneManager.UnloadSceneAsync(scene);
    }
}
