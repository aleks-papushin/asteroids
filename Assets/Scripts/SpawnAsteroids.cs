using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    public GameObject asteroidBig;
    public GameObject asteroidMiddle;
    public GameObject asteroidSmall;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SpawnBig(int count, Vector2? position = null)
    {
        Spawn(asteroidBig, count, position);
    }

    public void SpawnMiddle(int count, Vector2 position)
    {
        Spawn(asteroidMiddle, count, position);
    }

    public void SpawnSmall(int count, Vector2 position)
    {
        Spawn(asteroidSmall, count, position);
    }

    private void Spawn(GameObject asteroid, int count, Vector2? position = null)
    {
        bool isGeneratePosition = false;

        if (position == null)
        {
            isGeneratePosition = true;
        }

        for (int i = 0; i < count; i++)
        {
            if (isGeneratePosition)
            {
                position = gameManager.GetRandomPosition();
            }

            Instantiate(asteroid, (Vector2)position, asteroidBig.transform.rotation);
        }
    }
}
