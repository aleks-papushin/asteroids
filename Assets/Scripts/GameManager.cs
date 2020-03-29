using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    SpawnAsteroids asteroidsSpawner;
    SpawnUfo ufoSpawner;

    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreTextMesh;
    public TextMeshProUGUI livesTextMesh;

    public TextMeshProUGUI scoreOnGameOver;

    Camera cam;
    public float horizontalBound;
    public float verticalBound;
    Vector2 screenBorders;

    public int Lives { get; private set; }
    private const int initLifesCount = 3;
    private int score;
    private const int bonusScoreIncrement = 3000;
    private int addLifeOn = bonusScoreIncrement;
    

    private bool isGameActive = false;

    public int CurrentWaveNum { get; private set; } = 0;

    public void StartGame()
    {
        isGameActive = true;
        titleScreen.SetActive(false);
        gameOverScreen.SetActive(false);

        Instantiate(player, Vector2.zero, Quaternion.identity);

        asteroidsSpawner = FindObjectOfType<SpawnAsteroids>();
        ufoSpawner = FindObjectOfType<SpawnUfo>();

        cam = Camera.main;
        // gets position of camera top right point 
        screenBorders = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        horizontalBound = screenBorders.x;
        verticalBound = screenBorders.y;

        this.AddLives(initLifesCount);
        this.UpdateScore(0);

        StartCoroutine(HandleWaves());
    }

    public void RestartGame()
    {
        // destroy all asteroids and ufos
        DestroyAsteroids();
        DestroyUfos();

        // reset score and wave number
        CurrentWaveNum = 0;
        score = 0;

        // enable text meshes objects
        scoreTextMesh.gameObject.SetActive(true);
        livesTextMesh.gameObject.SetActive(true);

        // start game
        StartGame();
    }

    public void GameOver()
    {
        scoreTextMesh.gameObject.SetActive(false);
        livesTextMesh.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);

        scoreOnGameOver.text = $"Your score: {score}";

        CurrentWaveNum = -1; // for stopping ufo spawning coroutine
    }

    public void AddLives(int addLives)
    {
        this.Lives += addLives;
        livesTextMesh.text = $"Lives: {this.Lives}";
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        this.HandleBonusLife();
        scoreTextMesh.text = $"Score: {score}";
    }

    public Vector2 GetRandomPosition()
    {
        return new Vector2(
            UnityEngine.Random.Range(-horizontalBound, horizontalBound),
            UnityEngine.Random.Range(-verticalBound, verticalBound));
    }

    public void HandlePlayerDamage(Collider2D player)
    {
        this.AddLives(-1);

        if (Lives > 0)
        {
            StartCoroutine(player.GetComponent<PlayerController>().Teleport(isLifeLost: true));
        }
        else
        {
            isGameActive = false;
            Destroy(player.gameObject);
            this.GameOver();
        }
    }

    private IEnumerator HandleWaves()
    {
        while (isGameActive)
        {
            yield return null;

            if (GetAsteroidsCount() <= 0 && GetUfoCount() <= 0)
            {
                this.SpawnNewWave(CurrentWaveNum++);
            }
        }
    }

    private void SpawnNewWave(int waveNum)
    {
        // Spawn asteroids
        var bigAsteroidsAmount = Waves.waveDescription[waveNum][0];
        asteroidsSpawner.SpawnBig(bigAsteroidsAmount);

        // Spawn ufo
        var ufoAppearingTimeout = Waves.waveDescription[waveNum][1];
        var ufoSpawningInterval = Waves.waveDescription[waveNum][2];
        int[] ufoProbabilities = Waves.ufoProbabilities[waveNum];
        StartCoroutine(
            ufoSpawner.HandleSpawning(ufoAppearingTimeout, ufoSpawningInterval, ufoProbabilities));
    }

    private int GetUfoCount()
    {
        return 
            GameObject.FindGameObjectsWithTag("Ufo_big").Length +
            GameObject.FindGameObjectsWithTag("Ufo_small").Length;
    }

    private int GetAsteroidsCount()
    {
        int asteroidsCount = 0;
        asteroidsCount += GameObject.FindGameObjectsWithTag("Asteroid_big").Length;
        asteroidsCount += GameObject.FindGameObjectsWithTag("Asteroid_middle").Length;
        asteroidsCount += GameObject.FindGameObjectsWithTag("Asteroid_small").Length;

        return asteroidsCount;
    }

    private void DestroyUfos()
    {
        var ufos = GameObject.FindGameObjectsWithTag("Ufo_big").ToList();
        ufos.AddRange(GameObject.FindGameObjectsWithTag("Ufo_small"));

        foreach (var ufo in ufos)
        {
            Destroy(ufo);
        }
    }

    private void DestroyAsteroids()
    {
        var asteroids = GameObject.FindGameObjectsWithTag("Asteroid_big").ToList();
        asteroids.AddRange(GameObject.FindGameObjectsWithTag("Asteroid_middle"));
        asteroids.AddRange(GameObject.FindGameObjectsWithTag("Asteroid_small"));

        foreach (var asteroid in asteroids)
        {
            Destroy(asteroid);
        }
    }

    private void HandleBonusLife()
    {        
        if (score >= addLifeOn)
        {
            this.AddLives(1);
            addLifeOn += bonusScoreIncrement;
        }
    }

    // waveDescription[N][0] is count of big asteroids
    // waveDescription[N][1] is ufoAppearingTimeout
    // waveDescription[N][2] is ufoSpawningInterval
    private static class Waves
    {
        public static List<int[]> waveDescription = new List<int[]>()
        {
            new int[] {1, 20, 15},
            new int[] {2, 20, 10},
            new int[] {3, 15, 10},
            new int[] {4, 15, 10},
            new int[] {5, 10, 10},
            new int[] {6, 10, 5},
            new int[] {7, 10, 5},
            new int[] {7, 8, 4},
            new int[] {7, 6, 3},
            new int[] {7, 5, 2}
        };

        public static int[][] ufoProbabilities = new int[10][]
        {
            new int[] {1,1,1,1,1,1,1,1,1,1},
            new int[] {1,1,1,1,1,1,1,1,1,2},
            new int[] {1,1,1,1,1,1,1,2,2,2},
            new int[] {1,1,1,1,1,1,2,2,2,2},
            new int[] {1,1,1,1,2,2,2,2,2,2},
            new int[] {1,1,1,1,2,2,2,2,2,2},
            new int[] {1,1,1,2,2,2,2,2,2,2},
            new int[] {1,1,1,2,2,2,2,2,2,2},
            new int[] {1,1,1,2,2,2,2,2,2,2},
            new int[] {1,1,2,2,2,2,2,2,2,2},
        };
    }
}
