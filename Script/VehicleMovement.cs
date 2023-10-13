using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public float speed;
    public float exitSpeed;
    public GameObject vehicle;
    public GameObject tutor;
    public GameObject popup;
    private Vector3 PosX;
    // Start is called before the first frame update
    void Start()
    {
        exitSpeed = speed;
        PosX = vehicle.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (vehicle.transform.rotation.z == 0)
        {
            vehicle.transform.position += Vector3.up * speed;
        }
        else
        {
            vehicle.transform.position += Vector3.down * speed;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("car") || collision.CompareTag("Stop"))
        {
            speed = 0;
        }
        else if (collision.CompareTag("resetCarPos"))
        {
            if (vehicle.transform.rotation.z == 0)
            {
                vehicle.transform.position = new Vector3(PosX.x, GameObject.Find("carSpawn").transform.position.y, 0);
            }
            else
            {
                vehicle.transform.position = new Vector3(PosX.x, GameObject.Find("carSpawn (1)").transform.position.y, 0);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("car") || collision.CompareTag("Stop"))
        {
            speed = exitSpeed;
        } 
    }

}

