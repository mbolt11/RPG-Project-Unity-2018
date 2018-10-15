using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour {

    public GameObject transitionPanel;
    public int panelNum;
    public bool down;
    private Animator anim;
    
	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        anim = transitionPanel.GetComponent<Animator>();
        if (down)
            EndTransition();
        else
            anim.enabled = false;
	}

    public void StartTransition()
    {
        string animationName = "TransitionAnimSlideIn" + panelNum;
        anim.enabled = true;
        anim.Play(animationName);
        down = true;
        Time.timeScale = 1;
    }

    public void EndTransition()
    {
        string animationName = "TransitionAnimSlideIn" + panelNum;
        anim.enabled = true;
        //Debug.Log("end animation should happen");
        //Debug.Log(animationName);
        anim.Play(animationName);
        //Debug.Log(anim.GetCurrentAnimatorStateInfo(0));
        down = false;
        Time.timeScale = 1;
        //anim.enabled = false;
    }
}
