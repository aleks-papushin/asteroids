using UnityEngine;

public class GameManager : MonoBehaviour
{
    Camera cam;
    public float horizontalBound;
    public float verticalBound;
    Vector3 screenBorders;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        // gets position of camera top right point 
        screenBorders = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        horizontalBound = screenBorders.x;
        verticalBound = screenBorders.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
