using UnityEngine;

public class PopupManager : MonoBehaviour
{
    private Readxml xl;
    public bool isChecked;
    public static int activatePopups = 1; // This flag controls whether popups are disabled or not

    void Awake()
    {
        xl = GetComponent<Readxml>();
    }

    void Start()
    {
        Debug.Log("activatePopups is set to: " + activatePopups); // Debugging statement

        if (activatePopups == 1) // Check if popups should be disabled (assuming 0 means disabled)
        {
            xl.tutorPopup.SetActive(false);
            xl.popup.SetActive(false);
            isChecked = true;
            xl.DisableAllPopupsPermanently();
        } else
        {
            xl.tutorPopup.SetActive(true);
            isChecked= false;
        }
        // Optionally, you can add an 'else' block here if you want to enable popups.
    }
}
