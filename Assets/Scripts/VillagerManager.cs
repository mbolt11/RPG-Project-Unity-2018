using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour 
{
    //Arrays that hold the different things that the villagers say
    private string[] message1 = {"Hey have you seen my robot?", "It dissapeared when it was working in my fields yesterday!"};
    private string[] message2 = { "I tried to visit my friend Darian in Snobsville, and I met these moving boxes.", "They threw pieces of themseleves at me!", "I still have the bruises..." };
    private string[] message3 = { "I love visiting Mushroom village.", "I feel like a fairy princess, when I talk to the elves and sit in the mushroom houses." };
    private string[] message4 = { "Hey, these robot mobs are becoming an issue.", "Do you think you can fix this problem for us?", "I'll make you my official best friend, if you do!", "I would do it, and I could do it better than you.", "But I'm too busy making sure my front yard grass grows correctly.", "No, it really is important that you watch over your front yard grass." };
    private string[] message5 = { "I kind of like our new robot neighbors.", "They are such an interesting new development of nature.", "Hey, why do you have that wrench?", "Don't you dare try to 'fix' them! They deserve free will!!" };
    private string[] message6 = { "What? There is a robot apacolypse?", "I didn't notice." };
    private string[] message7 = { "I heard that if you find the 10 mystical ducks hiding throughout the land, the gods bestow upon you the divine 'weapon'!", "It might be just a rumor though..." };


    private int messagecount = 1;

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
