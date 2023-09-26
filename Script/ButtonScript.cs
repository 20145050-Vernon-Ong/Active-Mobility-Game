using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private Toggle toggle;
    private string toggleKey = "ToggleState"; // Key for PlayerPrefs

    private void Start()
    {
        toggle = GetComponent<Toggle>();

        // Load the toggle state from PlayerPrefs and set it on the toggle
        if (PlayerPrefs.HasKey(toggleKey))
        {
            bool savedToggleState = PlayerPrefs.GetInt(toggleKey) == 1;
            toggle.isOn = savedToggleState;
        }
    }

    public void ToggleValueChanged()
    {
        // Save the toggle state to PlayerPrefs when it changes
        int toggleState = toggle.isOn ? 1 : 0;
        PlayerPrefs.SetInt(toggleKey, toggleState);
        PlayerPrefs.Save();

        PopupManager.activatePopups = toggleState;

        // Handle your game logic based on the toggle state here

        // Log the toggle state
        Debug.Log("Toggle State: " + toggle.isOn);
    }
}

        
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class ButtonScript : MonoBehaviour
// {
//     private bool popupsToggled = false;
//     public Text buttonText; // Reference to the UI Text component for the button text
//     public string toggleOnText = "Toggle On Text";
//     public string toggleOffText = "Toggle Off Text";

//     private void Start()
//     {
//         // Get the UI Text component from the button
//         buttonText = GetComponentInChildren<Text>();
//         UpdateButtonText();
//     }

//     public void ButtonClick()
//     {
//         // Toggle the popups state
//         popupsToggled = !popupsToggled;

//         // Set the activatePopups variable based on the toggle state
//         PopupManager.activatePopups = popupsToggled;

//         // Log the toggle state and activatePopups value
//         Debug.Log("Toggle State: " + popupsToggled);
//         Debug.Log("activatePopups Value: " + PopupManager.activatePopups);

//         // Update the button text
//         UpdateButtonText();

//         // Load Scene2
//         // SceneManager.LoadScene("MainScene");
//     }

//     private void UpdateButtonText()
//     {
//         // Change the button's text based on the toggle state
//         if (popupsToggled)
//         {
//             buttonText.text = toggleOnText; // Set to the text for the toggle on state
//         }
//         else
//         {
//             buttonText.text = toggleOffText; // Set to the text for the toggle off state
//         }
//     }
// }


