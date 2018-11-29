using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VillagerManager : MonoBehaviour 
{
    //Arrays that hold the different things that the villagers say
    private string[] message1 = { "Have you seen anything strange going on? I thought I saw a robot over there.", "I heard that the robotics lab shut down recently...", "Do you think anything could have gone wrong?"};
    private string[] message2 = { "Watch out!", "Those robots are dangerous!", "They seem to be broken- one threw a screw at me!" };
    private string[] message3 = { "You have a wrench in your toolkit, right?", "Maybe you can use that to fix the robots.", "If you run into a robot, you can fix it.", "But be careful- they throw parts!" };
    private string[] message4 = { "Someone needs to do something about these robots.", "I thought I saw one stealing a dog.", "This robot was bigger than the rest, and purple."};
    private string[] message5 = { "If you find some more tools, you can use them to fix these malfunctioning robots.", "You may find more tools in the surrounding towns.", "The tools are stored in treasure chests-- let me know if you find one!" };
    private string[] message6 = { "To eradicate this problem, someone needs to defeat the Big Purple Boss robot.", "But I wouldn't attempt that until you have collected all the tools." };
    private string[] message7 = { "I hope you find your dog!" };

    private int messagecount = 1;

    public void Start()
    {
        if (gameObject.CompareTag("Villager"))
        {
            GameObject.Find("VillagerTorso").GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    public void talkToVillager()
    {
        //Select which message this villager will display
        string[] chosenMessage;
        switch (messagecount)
        {
            case 1:
                chosenMessage = message1;
                break;
            case 2:
                chosenMessage = message2;
                break;
            case 3:
                chosenMessage = message3;
                break;
            case 4:
                chosenMessage = message4;
                break;
            case 5:
                chosenMessage = message5;
                break;
            case 6:
                chosenMessage = message6;
                break;
            default:
                chosenMessage = message7;
                break;
        }

        //Increment the message count up to the total number of different messages, then reset to 1
        if (messagecount < 8)
        {
            messagecount++;
        }
        else
        {
            messagecount = 1;
        }
         
        //Add the chosen message as the new dialogue
        DialogueManager.Instance.AddNewDialogue(chosenMessage);
    }

    public void inRangetoTalkText()
    {
        DialogueManager.Instance.EnterKeyTextAppear();
    }
}
