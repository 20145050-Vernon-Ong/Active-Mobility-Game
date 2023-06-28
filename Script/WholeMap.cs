using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeMap : MonoBehaviour
{
    public GameObject MapCamera;
    // Start is called before the first frame update
    void Start()
    {
        MapCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void OpenMap()
    {
        MapCamera.SetActive(true);
    }

}
