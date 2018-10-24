using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    //Singleton
    public static DialogueManager Instance { get; set; }

    //References to UI elements
    public GameObject dialoguePanel;
    private Text dialogueText;

    private int dialogueIndex;

    //List of dialogue lines
    private List<string> dialogueLines = new List<string>();

	// Use this for initialization
	void Awake ()
	{
        //Access the text box of the dialogue panel
        dialogueText = dialoguePanel.GetComponentInChildren<Text>();
        dialoguePanel.SetActive(false);

        //Instantiate the script singleton
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
	}
	
    //This method gets called in the VillagerManager script
	public void AddNewDialogue(string[] lines)
    {
        //Create the list of dialogue lines based on the array input
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        //Call method to create dialogue in UI
        CreateDialogue();
    }

    //Method that sets the UI text according to the correct dialogue lines
    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        dialoguePanel.SetActive(true);
    }

    //Method that advances the dialogue to the next line
    public void ContinueDialogue()
    {
        //Check if there are more lines in the list
        if(dialogueIndex < dialogueLines.Count - 1)
        {
            //Advance to next line
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        //If no more lines, deactivate the panel
        else
        {
            dialoguePanel.SetActive(false);
        }
    }

    //Method sets the dialogue back to the previous line
    public void PreviousDialogueLine()
    {
        //Check if you are on the first line
        if (dialogueIndex > 0)
        {
            //Go back to the previous line
            dialogueIndex--;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
    }

    //Check the key input to see if player wants to advance or go back in conversation
    private void Update()
    {
        if(dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.RightArrow))
        {
            ContinueDialogue();
        }
        if(dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousDialogueLine();
        }
    }
}
