using UnityEngine;

public class PlayerProjectileCollisions : MonoBehaviour
{
    GameManager game;
    SpawnAsteroids spawner;

    void Start()
    {
        game = FindObjectOfType<GameManager>();
        spawner = FindObjectOfType<SpawnAsteroids>();        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherTag = other.gameObject.tag;

        // extract to separate function
        if (otherTag.Contains("Asteroid"))
        {
            if (other.CompareTag("Asteroid_big"))
            {
                spawner.SpawnMedium(2, other.transform.position);
            }
            else if (other.CompareTag("Asteroid_middle"))
            {
                spawner.SpawnSmall(2, other.transform.position);
            }

            HandleScore(other);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void HandleScore(Collider2D other)
    {
        int addScore = 0;

        // refactor to switch statement
        if (other.CompareTag("Asteroid_big"))
        {
            addScore = 20;
        }
        else if (other.CompareTag("Asteroid_middle"))
        {
            addScore = 50;
        }
        else if (other.CompareTag("Asteroid_small"))
        {
            addScore = 100;
        }
        else if (other.CompareTag("Ufo_big"))
        {
            addScore = 200;
        }
        else if (other.CompareTag("Ufo_small"))
        {
            addScore = 1000;
        }

        game.UpdateScore(addScore);
    }
}
