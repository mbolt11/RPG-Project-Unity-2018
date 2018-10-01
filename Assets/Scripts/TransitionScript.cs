using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour {

    public GameObject transitionPanel;
    public bool down;
    private Animator anim;
    
	// Use this for initialization
	void Start () {
        if (down)
            EndTransition();
        Time.timeScale = 1;
        anim = transitionPanel.GetComponent<Animator>();
        anim.enabled = false;
	}

    public void StartTransition()
    {
        anim.enabled = true;
        anim.Play("TransitionAnimSlideIn");
        down = true;
        Time.timeScale = 0;
    }

    public void EndTransition()
    {
        //Debug.Log("reached endTransition");
        anim.enabled = true;
        anim.Play("TransitionAnimSlideOut");
        down = false;
        Time.timeScale = 1;
        anim.enabled = false;
    }
}
