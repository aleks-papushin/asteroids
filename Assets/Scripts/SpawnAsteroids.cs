using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    public int count;

    public GameObject asteroidBig;
    public GameObject asteroidMiddle;
    public GameObject asteroidSmall;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        Spawn(asteroidBig, count);
    }

    public void SpawnMedium(int count, Vector2 position)
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
                position = GetRandomPosition();
            }

            Instantiate(asteroid, (Vector2) position, asteroidBig.transform.rotation);
        }
    }

    private Vector2 GetRandomPosition()
    {
        return new Vector2(
            Random.Range(-gameManager.horizontalBound, gameManager.horizontalBound),
            Random.Range(-gameManager.verticalBound, gameManager.verticalBound));
    }   
}
