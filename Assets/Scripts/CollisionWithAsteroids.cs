using UnityEngine;

public class CollisionWithAsteroids : MonoBehaviour
{
    GameManager game;
    SpawnAsteroids spawner;

    void Start()
    {
        spawner = FindObjectOfType<SpawnAsteroids>();
        game = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        
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
            HandleMyDamage();            
        }
    }

    private void HandleScore(Collider2D other)
    {
        int addScore = 0;

        // refactor to switch statement
        if (gameObject.CompareTag("PlayerProjectile"))
        {
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
        }

        game.UpdateScore(addScore);
    }

    private void HandleMyDamage()
    {
        if (gameObject.CompareTag("Player"))
        {
            game.AddLives(-1);

            if (game.Lives == 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
