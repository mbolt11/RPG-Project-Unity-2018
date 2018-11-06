using UnityEngine;
using UnityEngine.UI;

public class ToolToggles : MonoBehaviour
{
    Toggle thisToggle;

    void Start()
    {
        //Fetch the Toggle GameObject
        thisToggle = GetComponent<Toggle>();

        //Add listener for when the state of the Toggle changes, to take action
        thisToggle.onValueChanged.AddListener(delegate {ToggleValueChanged(thisToggle);});
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle toggleClicked)
    {
        OverworldGameController.gameInfo.ChooseFunction(toggleClicked);
    }
}