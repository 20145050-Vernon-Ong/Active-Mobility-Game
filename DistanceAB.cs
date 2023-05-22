using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DistanceAB : MonoBehaviour
{
    private float distance;

    [SerializeField]
    private TMP_Text distanceText;

    public List<Transform> pointList = new();
    // Update is called once per frame
    void Update()
    {
        
    }
}
