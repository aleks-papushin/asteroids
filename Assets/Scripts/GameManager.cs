using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreTextMesh;
    public TextMeshProUGUI livesTextMesh;

    Camera cam;
    public float horizontalBound;
    public float verticalBound;
    Vector2 screenBorders;

    public int Lives { get; set; } = 3;
    private int score;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        // gets position of camera top right point 
        screenBorders = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        horizontalBound = screenBorders.x;
        verticalBound = screenBorders.y;

        SetLives(Lives);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreTextMesh.text = $"Score: {score}";
    }

    public void SetLives(int lives)
    {
        Lives = lives;
        livesTextMesh.text = $"Lives: {Lives}";
    }
}
