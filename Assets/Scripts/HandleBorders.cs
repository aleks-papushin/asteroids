using UnityEngine;

public class HandleBorders : MonoBehaviour
{
    GameManager gameManager;

    public bool handleOnlyVertical;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!handleOnlyVertical)
        {
            if (transform.position.x > gameManager.horizontalBound)
            {
                transform.position = new Vector2(-gameManager.horizontalBound, transform.position.y);
            }
            else if (transform.position.x < -gameManager.horizontalBound)
            {
                transform.position = new Vector2(gameManager.horizontalBound, transform.position.y);
            }
        }

        if (transform.position.y > gameManager.verticalBound)
        {
            transform.position = new Vector2(transform.position.x, -gameManager.verticalBound);
        }
        else if (transform.position.y < -gameManager.verticalBound)
        {
            transform.position = new Vector2(transform.position.x, gameManager.verticalBound);
        }
    }
}
