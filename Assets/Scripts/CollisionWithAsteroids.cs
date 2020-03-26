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

        if (otherTag.Contains("Asteroid"))
        {
            if (otherTag.Contains("big"))
            {
                spawner.SpawnMedium(2, other.transform.position);
            }
            else if (otherTag.Contains("middle"))
            {
                spawner.SpawnSmall(2, other.transform.position);
            }

            Destroy(other.gameObject);

            HandleMyDamage();            
        }
    }

    private void HandleMyDamage()
    {
        if (gameObject.CompareTag("Player"))
        {
            var lives = game.Lives - 1;
            game.SetLives(lives);

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
