using System.Collections;
using UnityEngine;

public class SpawnUfo : MonoBehaviour
{
    public GameObject bigUfo;
    public GameObject smallUfo;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    Vector2 GetRandomUfoPosition()
    {
        // random picking left or right screen side        
        var xModifiers = new int[] { -1, 1 };
        var xMod = xModifiers[Random.Range(0, xModifiers.Length)];

        // generate position with x as left or right border and random y position
        var position = new Vector2(
            gameManager.horizontalBound * xMod,
            Random.Range(-gameManager.verticalBound, gameManager.verticalBound));

        return position;
    }

    internal IEnumerator HandleSpawning(int ufoAppearingTimeout, int ufoSpawningInterval, int[] ufoProbabilities)
    {
        throw new System.NotImplementedException();
    }
}
