using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DistanceCalculator2 : MonoBehaviour
{
    public GC script;
    private int userCoins;
    public Image compassArrow;
    //public Transform player;           // Reference to the player's transform
    //public string[] targetTags;        // Tags of target objects
    public TextMeshProUGUI distanceText; // Reference to the Text GameObject for displaying distance
    public float activationDistance = 5f; // The distance at which the object becomes active
    private Transform nearestObject;  // Reference to the nearest object
    private float nearestDistance; // Initially set to positive infinity
    private float angle;
    private void Update()
    {
        userCoins = script.totalpoints;

        nearestObject = null; // Reset nearestObject on each update
        nearestDistance = Mathf.Infinity; // Reset nearestDistance on each update

        /*foreach (string targetTag in targetTags)
        {*/
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("gemTag");

            foreach (GameObject obj in taggedObjects)
            {
                // Calculate the distance between the player and the current target object
                float distance = Vector3.Distance(GameObject.FindWithTag("Player").transform.position, obj.transform.position);

                // If this object is closer than the previous nearest, update the nearest object and distance
                if (distance < nearestDistance)
                {
                    nearestObject = obj.transform;
                    nearestDistance = distance;
                }

                // Check if the object is within activation distance and set it active if true
                if (distance <= activationDistance)
                {
                    obj.SetActive(true);
                }
                else
                {
                    obj.SetActive(false);
                }
            }
        //}

        // Display the distance to the nearest object
        if (nearestObject != null)
        {
            // Convert the nearest distance to an integer
            // Display the integer distance in the Text GameObject
            Vector3 directionToNearest = nearestObject.position - GameObject.FindWithTag("Player").transform.position;
            angle = Mathf.Atan2(directionToNearest.y, directionToNearest.x) * Mathf.Rad2Deg;
            compassArrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            distanceText.text = "Nearest Gem: " + nearestDistance.ToString("0") + " m";
        }
        else
        {
            // No nearest object found, display a message or handle this case as needed
            distanceText.text = "No Gems Nearby";
            compassArrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            //compassArrow.gameObject.SetActive(false);
        }

        // Update the rotation of the compass arrow
        if (nearestObject != null)
        {
            

            
        }
        else
        {
            // If no nearest object, reset the rotation of the compass arrow
            
        }
    }
}
