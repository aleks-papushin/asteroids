using System.Collections;
using UnityEngine;

public class UfoProjectileMoveToPlayer : MonoBehaviour
{
    public float speed;

    GameObject player;    
    Vector2 targetDirection;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");

        targetDirection = GetPlayerPosition() - (Vector2)transform.position;
        targetDirection.Normalize();
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (gameManager.IsGameActive)
        {
            yield return null;
            transform.Translate(targetDirection * speed * Time.deltaTime);
        }        
    }

    Vector2 GetPlayerPosition()
    {
        Vector2 position;

        if (player != null)
        {
            position = player.transform.position;
        }
        else
        {
            position = gameManager.GetRandomPosition();
        }

        return position;
    }
}
