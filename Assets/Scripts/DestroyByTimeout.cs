using System.Collections;
using UnityEngine;

public class DestroyByTimeout : MonoBehaviour
{
    public float timeoutSeconds;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitTimeout());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitTimeout()
    {
        yield return new WaitForSeconds(timeoutSeconds);
        Destroy(gameObject);
    }
}
