using UnityEngine;

public class UfoProjectileCollisions : MonoBehaviour
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

            gameManager.HandleObjectExplosion(other);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            gameManager.HandleObjectExplosion(other);
            gameManager.HandlePlayerDamage(other);
        }
    }
}
