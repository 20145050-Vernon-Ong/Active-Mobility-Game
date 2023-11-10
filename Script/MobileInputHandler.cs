using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using SystemInfo = UnityEngine.Device.SystemInfo;
public class MobileInputHandler : MonoBehaviour, IPointerClickHandler
{
    public TMP_InputField inputField; // Use TMP_InputField for TextMesh Pro input fields.

    private TouchScreenKeyboard keyboard;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the application is running on a mobile platform.
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.Other)
        {
            // Open the mobile keyboard and set the initial text.
            keyboard = TouchScreenKeyboard.Open(inputField.text, TouchScreenKeyboardType.Default, false, false, false, false);
        }
    }

    private void Update()
    {
        if (keyboard != null)
        {
            // Update the TMP_InputField text with the keyboard input.
            inputField.text = keyboard.text;
        }
    }
}
