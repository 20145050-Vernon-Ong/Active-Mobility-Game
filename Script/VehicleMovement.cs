using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    private float speed = 0.03f;
    public GameObject vehicle;
    private Vector3 PosX;
    // Start is called before the first frame update
    void Start()
    {
        PosX = vehicle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (vehicle.transform.rotation.z == 0)
        {
            vehicle.transform.localPosition += Vector3.up * speed;
        } else
        {
            vehicle.transform.localPosition += Vector3.down * speed;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stop") || collision.CompareTag("car"))
        {
            speed = 0;
        }

        if (collision.CompareTag("resetCarPos"))
        {
            Debug.Log("Touched");
            if (vehicle.transform.localPosition.x == PosX.x)
            {
                if (vehicle.transform.localPosition.x < 4 || (vehicle.transform.localPosition.x > 34 && vehicle.transform.localPosition.x < 36))
                {
                    vehicle.transform.localPosition = new Vector3(PosX.x, -99, 0);
                } else if (vehicle.transform.localPosition.x >= 4 && vehicle.transform.localPosition.x <= 6 || (vehicle.transform.localPosition.x >= 36 && vehicle.transform.localPosition.x < 38))
                {
                    vehicle.transform.localPosition = new Vector3(PosX.x, -14, 0);
                } 
                
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stop") || collision.CompareTag("car"))
        {
            speed = 0.03f;
        }
    }

}
