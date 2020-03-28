using UnityEngine;

public class UfoCollisions : MonoBehaviour
{
    SpawnAsteroids spawner;

    // Start is called before the first frame update
    void Start()
    {
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

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
