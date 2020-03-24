using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    public int count;

    public GameObject asteroid;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        for (int i = 0; i < count; i++)
        {
            Instantiate(asteroid, GetRandomPosition(), asteroid.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(-gameManager.horizontalBound, gameManager.horizontalBound),
            Random.Range(-gameManager.verticalBound, gameManager.verticalBound), 
            0);
    }   
}
