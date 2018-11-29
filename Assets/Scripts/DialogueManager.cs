using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour 
{
    //Singleton
    public static DialogueManager Instance { get; set; }

    //References to UI elements
    public GameObject dialoguePanel;
    public GameObject talkPromptPanel;
    public GameObject previousPromptPanel;
    public GameObject nextPromptPanel;
    public GameObject villagerNamePanel;
    private Text dialogueText;
    private Text enterKeyPrompt;
    private Text previousKeyPrompt;
    private Text nextKeyPrompt;
    private Text villagerName;
    private bool talking;
    private bool narrating;
    private string[] narrative;
    private int dialogueIndex;

    //List of dialogue lines
    private List<string> dialogueLines = new List<string>();

	// Use this for initialization
	void Awake ()
	{
        narrating = false;

        //Access the text box of the dialogue panel
        dialogueText = dialoguePanel.GetComponentInChildren<Text>();
        dialoguePanel.SetActive(false);

        enterKeyPrompt = talkPromptPanel.GetComponentInChildren<Text>();
        talkPromptPanel.SetActive(false);
        talking = false;

        previousKeyPrompt = previousPromptPanel.GetComponentInChildren<Text>();
        previousPromptPanel.SetActive(false);
        previousKeyPrompt.text = "Press <-- for previous";

        nextKeyPrompt = nextPromptPanel.GetComponentInChildren<Text>();
        nextPromptPanel.SetActive(false);
        nextKeyPrompt.text = "Press --> for next";

        villagerName = villagerNamePanel.GetComponentInChildren<Text>();
        villagerNamePanel.SetActive(false);

        //Instantiate the script singleton
        if (Instance != null && Instance != this)
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

        if (narrating)
            CreateNarration();
        else
        {
            talking = true;

            //Call method to create dialogue in UI
            CreateDialogue();
        }
    }

    public void EnterKeyTextAppear()
    {
        //Debug.Log("appear");
        enterKeyPrompt.text = "Press ENTER to talk";
        talkPromptPanel.SetActive(true);
    }

    public void EnterKeyTextDissapear()
    {
        talkPromptPanel.SetActive(false);
    }

    public bool stillTalking()
    {
        return talking;
    }

    public void PreviousKeyTextAppear()
    {
        if(!previousKeyPrompt.IsActive())
            previousPromptPanel.SetActive(true);
    }

    public void PreviousKeyTextDissapear()
    {
        if (previousKeyPrompt.IsActive())
            previousPromptPanel.SetActive(false);
    }

    public void NextKeyTextAppear()
    {
        if (!nextKeyPrompt.IsActive())
            nextPromptPanel.SetActive(true);
    }

    public void NextKeyTextDissapear()
    {
        if (nextKeyPrompt.IsActive())
            nextPromptPanel.SetActive(false);
    }

    public bool NextDialogueExist()
    {
        if (dialogueIndex < dialogueLines.Count)
            return true;
        else
            return false;
    }

    public bool PreviousDialogueExist()
    {
        if (dialogueIndex > 0)
            return true;
        else
        {
            //Debug.Log("index " + dialogueIndex);
            //Debug.Log("dialogueLines " + dialogueLines.Count);
            return false;
        }
    }

    //Method that sets the UI text according to the correct dialogue lines
    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        dialoguePanel.SetActive(true);
        villagerName.text = PlayerController.Instance.getVillagerName();
        villagerNamePanel.SetActive(true);
        EnterKeyTextDissapear();
    }

    public void CreateNarration()
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
            villagerNamePanel.SetActive(false);
            talking = false;
            narrating = false;
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

    public void forceEndDialogue()
    {
        dialogueIndex = dialogueLines.Count;
        ContinueDialogue();
    }

    //Check the key input to see if player wants to advance or go back in conversation
    private void Update()
    {
        if (PreviousDialogueExist() && (stillTalking() || narrating))
        {
            PreviousKeyTextAppear();
        }
        else
            PreviousKeyTextDissapear();

        if (NextDialogueExist() && (stillTalking() || narrating))
        {
            NextKeyTextAppear(); 
        }
        else
            NextKeyTextDissapear();

        if(dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.RightArrow))
        {
            ContinueDialogue();
        }
        if(dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousDialogueLine();
        }
    }

    //Methods for the narration

    //Initial Game Message
    public void BeginMessage()
    {
        narrating = true;
        narrative = new string[1];
        narrative[0] = "Welcome to Farmerville! This morning, you discovered that your dog, Fido, is missing. Try talking to some of your neighbors to see what is going on. Use WASD to move, Spacebar to jump, and hold Shift to run.";
        AddNewDialogue(narrative);
        dialoguePanel.SetActive(true);
        nextPromptPanel.SetActive(true);
    }

    //Chest openend message
    public void ChestOpenedMessage(string toolname)
    {
        narrating = true;
        narrative[0] = "You discovered the " + toolname + "! You can activate it in the tools menu.";
        AddNewDialogue(narrative);
        dialoguePanel.SetActive(true);
        nextPromptPanel.SetActive(true);
    }
}
