using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public GameObject Screen;
    public GameObject questButton;
    public GameObject gpsBtn;
    public GameObject callButton;
    public GameObject openQuestList;
    public GameObject openSMS;
    public GameObject openGPS;
    public GameObject minimisedPhone;
    public GameObject winPanel;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetActive(true);
        openQuestList.SetActive(false);
        openSMS.SetActive(false);
        openGPS.SetActive(false);
    }

    public void OpenQuestList()
    {
        Screen.SetActive(false);
        openQuestList.SetActive(true);
    }

    public void OpenSMS()
    {
        Screen.SetActive(false);
        openSMS.SetActive(true);
    }

    public void OpenGPS()
    {
        Screen.SetActive(false);
        openGPS.SetActive(true);
    }

    public void ClosePhone()
    {
        if (Screen.activeInHierarchy)
        {
            minimisedPhone.transform.localPosition = new Vector3(5, -445, 0);
        } else if (openQuestList.activeInHierarchy)
        {
            Screen.SetActive(true);
            openQuestList.SetActive(false);
            winPanel.SetActive(false);
        } else if (openSMS.activeInHierarchy)
        {
            Screen.SetActive(true);
            openSMS.SetActive(false);
        } else if (callButton.activeInHierarchy)
        {

        } else if (openGPS.activeInHierarchy)
        {
            Screen.SetActive(true);
            openGPS.SetActive(false);
        }
    }

}
