using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    SpawnAsteroids spawner;

    void Start()
    {
        spawner = FindObjectOfType<SpawnAsteroids>();
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

            Destroy(gameObject);
        }
    }
}
