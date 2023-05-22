using UnityEngine;

public class HomeScreen : MonoBehaviour
{
    public GameObject Screen;
    public GameObject questButton;
    public GameObject messageButton;
    public GameObject callButton;
    public GameObject openQuestList;
    public GameObject openSMS;
    public GameObject minimisedPhone;
    public GameObject winPanel;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetActive(true);
        openQuestList.SetActive(false);
        openSMS.SetActive(false);
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

    public void ClosePhone()
    {
        if (Screen.activeInHierarchy)
        {
            minimisedPhone.transform.position = new Vector3(900, -110, 0);
        } else if (openQuestList.activeInHierarchy)
        {
            Screen.SetActive(true);
            openQuestList.SetActive(false);
            winPanel.SetActive(false);
        } else if (openSMS.activeInHierarchy)
        {
            Screen.SetActive(true);
            openSMS.SetActive(false);
        }
    }

}
