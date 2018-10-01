using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour {

    public GameObject transitionPanel;
    private Animator anim;
    //private bool isTransit = false;
    //private bool loading = false;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        anim = transitionPanel.GetComponent<Animator>();
        anim.enabled = false;
	}
	
	// Update is called once per frame
	/*void Update () {
        if (isTransit && !loading)
            StartTransition();
        else if (isTransit && loading)
            EndTransition();
	}*/

    public void StartTransition()
    {
        anim.enabled = true;
        anim.Play("TransitionAnimSlideIn");
        //loading = true;
        Time.timeScale = 0;
    }

    public void EndTransition()
    {
        //loading = false;
        anim.Play("TransitionAnimSlideOut");
        Time.timeScale = 1;
    }
}
