using UnityEngine;

public class PlayerCollisions : MonoBehaviour
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
                spawner.SpawnMiddle(2, other.transform.position);
            }
            else if (other.CompareTag("Asteroid_middle"))
            {
                spawner.SpawnSmall(2, other.transform.position);
            }

            Destroy(other.gameObject);
            game.HandlePlayerDamage(GetComponent<Collider2D>());
        }
        else if (other.CompareTag("Ufo"))
        {
            Destroy(other.gameObject);
            game.HandlePlayerDamage(GetComponent<Collider2D>());
        }
    }
}
