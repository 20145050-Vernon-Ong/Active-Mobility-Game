using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public float speed = 0.03f;
    public GameObject vehicle;
    public GameObject reset;
    private Vector3 PosX;
    // Start is called before the first frame update
    void Awake()
    {
        PosX = vehicle.transform.position;     
    }

    // Update is called once per frame
    void Update()
    {
        if (vehicle.transform.rotation.z == 0)
        {
            vehicle.transform.localPosition += Vector3.up * speed;
        }
        else
        {
            vehicle.transform.localPosition += Vector3.down * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stop"))
        {
            speed = 0;
        }
        else if (collision.CompareTag("car"))
        {
            speed = 0;
        } else if (collision.CompareTag("resetCarPos"))
        {
            if (vehicle.transform.rotation.z == 0)
            {
                vehicle.transform.localPosition = new Vector3(PosX.x, 0, 0);
            } else
            {
                vehicle.transform.localPosition = new Vector3(PosX.x, 0, 0);
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Stop") || collision.CompareTag("car"))
        {
            speed = 0.03f;
        }
    }

}

