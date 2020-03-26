using System.Collections;
using UnityEngine;

public class SpawnUfo : MonoBehaviour
{
    public GameObject bigUfo;
    public float bigUfoSpawningTimeout;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnBig());        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnBig()
    {
        yield return new WaitForSeconds(bigUfoSpawningTimeout);
        Instantiate(bigUfo, GetRandomUfoPosition(), bigUfo.transform.rotation);
    }

    Vector2 GetRandomUfoPosition()
    {
        // for random picking left or right screen side        
        var xModifiers = new int[] { -1, 1 };
        var xMod = xModifiers[Random.Range(0, xModifiers.Length)];

        // generate position with x as left or right border and random y position
        var position = new Vector2(
            gameManager.horizontalBound * xMod, 
            Random.Range(0f, gameManager.verticalBound));

        return position;
    }
}
