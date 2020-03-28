using System.Collections;
using UnityEngine;

public class DestroyOutOfHorizontalBounds : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(DestroyIfOutOfBounds());
    }

    private IEnumerator DestroyIfOutOfBounds()
    {
        while (true)
        {
            if (transform.position.x < -gameManager.horizontalBound ||
                transform.position.x > gameManager.horizontalBound)
            {
                Destroy(gameObject);
            }

            yield return null;
        }
    }
}
