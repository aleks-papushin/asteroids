using System.Collections;
using UnityEngine;

public class SpawnUfo : MonoBehaviour
{
    public GameObject bigUfo;
    public GameObject smallUfo;

    GameManager gameManager;

    float ufoPollingInterval = 3;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    internal IEnumerator HandleSpawning(int ufoAppearingTimeout, int ufoSpawningInterval, int[] ufoProbabilities)
    {
        yield return new WaitForSeconds(ufoAppearingTimeout - ufoSpawningInterval);

        while (true)
        {
            yield return new WaitForSeconds(ufoSpawningInterval);

            ChooseAndSpawnUfo(ufoProbabilities);
        }
    }

    private void ChooseAndSpawnUfo(int[] ufoProbabilities)
    {
        // get random from probabilities array
        var picker = ufoProbabilities[Random.Range(0, ufoProbabilities.Length)];
        GameObject ufo = null;

        switch (picker)
        {
            case 1:
                ufo = bigUfo;
                break;
            case 2:
                ufo = smallUfo;
                break;
        }

        Instantiate(ufo, GetRandomUfoPosition(), ufo.transform.rotation);
    }

    private Vector2 GetRandomUfoPosition()
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
}
