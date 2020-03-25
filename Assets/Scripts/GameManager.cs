using UnityEngine;

public class GameManager : MonoBehaviour
{
    Camera cam;
    public float horizontalBound;
    public float verticalBound;
    Vector2 screenBorders;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        // gets position of camera top right point 
        screenBorders = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        horizontalBound = screenBorders.x;
        verticalBound = screenBorders.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
