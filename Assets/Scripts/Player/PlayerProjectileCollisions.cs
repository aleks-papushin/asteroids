using UnityEngine;

public class PlayerProjectileCollisions : MonoBehaviour
{
    GameManager gameManager;
    SpawnAsteroids spawner;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
                spawner.SpawnMiddle(2, other.transform.position);
            }
            else if (other.CompareTag("Asteroid_middle"))
            {
                spawner.SpawnSmall(2, other.transform.position);
            }

            gameManager.PlayExplosion();
            HandleScore(other);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (otherTag.Contains("Ufo"))
        {
            gameManager.PlayExplosion();
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

        gameManager.UpdateScore(addScore);
    }
}
