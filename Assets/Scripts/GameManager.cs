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

    public int Lives { get; private set; }
    private const int initLifesCount = 3000;
    private int score;    
    private int addLiveOn = 10000;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        // gets position of camera top right point 
        screenBorders = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        horizontalBound = screenBorders.x;
        verticalBound = screenBorders.y;

        AddLives(initLifesCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLives(int addLives)
    {
        this.Lives += addLives;
        livesTextMesh.text = $"Lives: {this.Lives}";
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        HandleBonusLife();
        scoreTextMesh.text = $"Score: {score}";
    }

    public Vector2 GetRandomPosition()
    {
        return new Vector2(
            Random.Range(-horizontalBound, horizontalBound),
            Random.Range(-verticalBound, verticalBound));
    }

    public void HandlePlayerDamage(Collider2D player)
    {
        this.AddLives(-1);

        if (Lives == 0)
        {
            Destroy(player.gameObject);
        }
    }

    private void HandleBonusLife()
    {        
        if (score - addLiveOn >= 0)
        {
            AddLives(1);
            addLiveOn += 10000;
        }
    }
}
