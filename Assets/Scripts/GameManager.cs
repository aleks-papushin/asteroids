using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    SpawnAsteroids spawner;

    public TextMeshProUGUI scoreTextMesh;
    public TextMeshProUGUI livesTextMesh;

    Camera cam;
    public float horizontalBound;
    public float verticalBound;
    Vector2 screenBorders;

    public int Lives { get; private set; }
    private const int initLifesCount = 3000;
    private int score;    
    private int addLifeOn = 10000;

    public int currentWaveNum;

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<SpawnAsteroids>();

        cam = Camera.main;
        // gets position of camera top right point 
        screenBorders = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        horizontalBound = screenBorders.x;
        verticalBound = screenBorders.y;

        AddLives(initLifesCount);
    }

    private void Update()
    {
        HandleWaves();
    }

    private void HandleWaves()
    {
        if (GetAsteroidsCount() <= 0 && GetUfoCount() <= 0)
        {
            SpawnNewWave(currentWaveNum++);
        }
    }

    private void SpawnNewWave(int waveNum)
    {
        var bigAsteroidsAmount = Waves.waveDescription[waveNum][0];
        spawner.SpawnBig(bigAsteroidsAmount);
    }

    private int GetUfoCount()
    {
        return GameObject.FindGameObjectsWithTag("Ufo").Length;
    }

    private int GetAsteroidsCount()
    {
        int asteroidsCount = 0;
        asteroidsCount += GameObject.FindGameObjectsWithTag("Asteroid_big").Length;
        asteroidsCount += GameObject.FindGameObjectsWithTag("Asteroid_middle").Length;
        asteroidsCount += GameObject.FindGameObjectsWithTag("Asteroid_small").Length;

        return asteroidsCount;
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

        StartCoroutine(player.GetComponent<PlayerController>().Teleport(isLifeLost: true));

        if (Lives == 0)
        {
            Destroy(player.gameObject);
        }
    }

    private void HandleBonusLife()
    {        
        if (score >= addLifeOn)
        {
            AddLives(1);
            addLifeOn += 10000;
        }
    }

    // int[0] is count of big asteroids
    private static class Waves
    {
        public static List<int[]> waveDescription = new List<int[]>()
        {
            new int[] {1},
            new int[] {2},
            new int[] {6},
            new int[] {7},
            new int[] {8},
            new int[] {9}
        };

        public static int[,] ufoProbabilities = new int[10, 10]
        {
            {1,1,1,1,1,1,1,1,1,1},
            {1,1,1,1,1,1,2,2,2,2},
            {1,1,1,1,1,2,2,2,2,2},
            {1,1,1,1,2,2,2,2,2,2},
            {1,1,1,2,2,2,2,2,2,2},
            {1,1,2,2,2,2,2,2,2,2},
            {1,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2},
        };
    }
}
