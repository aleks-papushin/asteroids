using UnityEngine;

public class UfoProjectileCollisions : MonoBehaviour
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
        // extract to separate function
        if (other.gameObject.tag.Contains("Asteroid"))
        {
            if (other.CompareTag("Asteroid_big"))
            {
                spawner.SpawnMiddle(2, other.transform.position);
            }
            else if (other.CompareTag("Asteroid_middle"))
            {
                spawner.SpawnSmall(2, other.transform.position);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            game.HandlePlayerDamage(other);
        }
    }
}
