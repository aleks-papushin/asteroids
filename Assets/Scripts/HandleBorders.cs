using UnityEngine;

public class HandleBorders : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (transform.position.x > gameManager.horizontalBound)
        {
            transform.position = new Vector3(-gameManager.horizontalBound, transform.position.y, 0);
        }
        else if (transform.position.x < -gameManager.horizontalBound)
        {
            transform.position = new Vector3(gameManager.horizontalBound, transform.position.y, 0);
        }

        else if (transform.position.y > gameManager.verticalBound)
        {
            transform.position = new Vector3(transform.position.x, -gameManager.verticalBound, 0);
        }

        else if (transform.position.y < -gameManager.verticalBound)
        {
            transform.position = new Vector3(transform.position.x, gameManager.verticalBound, 0);
        }
    }
}
