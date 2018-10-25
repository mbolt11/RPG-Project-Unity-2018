using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour 
{
    //Arrays that hold the different things that the villagers say
    private string[] message1 = {"This is the first message", "It has 2 lines"};
    private string[] message2 = { "This is the second message", "It has 3 lines", "Watch out for the robots!" };
    private string[] message3 = { "This is the third message woo yeah!!" };
    private string[] message4 = { "This is the fourth message", "It is really long", "This villager is very talkative", "Robots robots robots", "I really hope this works!", "Hi Steven and Amanda!" };

    private int messagecount = 1;

    //Method which is called when the player interacts with the villager
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
            default:
                chosenMessage = message4;
                break;
        }

        //Increment the message count up to the total number of different messages, then reset to 1
        if (messagecount < 4)
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
}
