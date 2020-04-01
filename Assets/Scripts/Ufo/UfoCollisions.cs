using UnityEngine;

public class UfoCollisions : MonoBehaviour
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

            gameManager.PlayExplosionSound();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
