using UnityEngine;
using UnityEngine.UI;

public class HomeScreen : MonoBehaviour
{
    public GameObject Screen;
    public GameObject questButton;
    public GameObject openQuestList;
    public GameObject minimisedPhone;
    public GameObject winPanel;
    public GameObject map;
    public Transform cam;
    public Camera wholeMap;
    public RectTransform mapImage;
    private CanvasScaler can;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetActive(true);
        openQuestList.SetActive(false);
        map.SetActive(false);
        can = GetComponent<CanvasScaler>();
    }

    public void OpenQuestList()
    {
        Screen.SetActive(false);
        openQuestList.SetActive(true);
    }

    public void OpenMap()
    {
        Screen.SetActive(false);
        map.SetActive(true);
        Time.timeScale = 0;
    }
    public void ClosePhone()
    {
        if (Screen.activeInHierarchy)
        {
            minimisedPhone.transform.localPosition = new Vector3(-5, -464.6f, 0);
        } else if (openQuestList.activeInHierarchy)
        {
            Screen.SetActive(true);
            openQuestList.SetActive(false);
            winPanel.SetActive(false);
        } else if (map.activeInHierarchy)
        {
            Screen.SetActive(true);
            map.SetActive(false);
            winPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void MoveVerticalUp()
    {
        if (cam.localPosition.y < 0 && wholeMap.orthographicSize == 18)
        {
            cam.localPosition += new Vector3(0, 2, 0);
        } else if (wholeMap.orthographicSize < 18)
        {
            cam.localPosition += new Vector3(0, 2, 0);
        }
        
    }

    public void MoveVerticalDown()
    {
        if (cam.localPosition.y > -4)
        {
            cam.localPosition -= new Vector3(0, 2, 0);
        }
        
    }

    public void MoveHorizontalRight()
    {
        if (cam.localPosition.x < 4)
        {
            cam.localPosition += new Vector3(2, 0, 0);
        }
        
    }
    public void MoveHorizontalLeft()
    {
        if (cam.localPosition.x > -134)
        {
            cam.localPosition -= new Vector3(2, 0, 0);
        }
        
    }

    public void Zoom()
    {
        if (Input.GetAxis("MouseScrollWheel") > 0)
        {
            if (wholeMap.orthographicSize >= 2)
            {
                wholeMap.orthographicSize -= 1;
            }
        }
        else
        {
            if (wholeMap.orthographicSize <= 17)
            {
                wholeMap.orthographicSize += 1;
            }
            
        }
    }

    public void Rotate()
    {
        
        if (minimisedPhone.transform.rotation.z == 0)
        {
            minimisedPhone.transform.Rotate(0, 0, 90);
            cam.transform.Rotate(0, 0, 90);
            
        } else if (minimisedPhone.transform.rotation.z == 90)
        {
            minimisedPhone.transform.Rotate(0, 0, 0);
            cam.transform.Rotate(0, 0, 0);
        }
    }

    public void Maxismise()
    {
        
    }
   
}
