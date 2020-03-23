using UnityEngine;

public class HandleBorders : MonoBehaviour
{
    Camera cam;
    float horizontalBound;
    float verticalBound;
    Vector3 screenBorders;

    void Start()
    {
        cam = Camera.main;
        // gets position of camera right top point 
        screenBorders = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        horizontalBound = screenBorders.x;
        verticalBound = screenBorders.y;
    }

    void Update()
    {
        if (transform.position.x > horizontalBound)
        {
            transform.position = new Vector3(-horizontalBound, transform.position.y, 0);
        }
        else if (transform.position.x < -horizontalBound)
        {
            transform.position = new Vector3(horizontalBound, transform.position.y, 0);
        }

        else if (transform.position.y > verticalBound)
        {
            transform.position = new Vector3(transform.position.x, -verticalBound, 0);
        }

        else if (transform.position.y < -verticalBound)
        {
            transform.position = new Vector3(transform.position.x, verticalBound, 0);
        }
    }
}
