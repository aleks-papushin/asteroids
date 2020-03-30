using System.Collections;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject ufoProjectile;

    public float speed;
    public float timeoutSeconds;

    bool shouldShoot = true;

    void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (shouldShoot)
        {
            StartCoroutine(ShootAndWaitCooldown());
            shouldShoot = false;
        }
    }

    private IEnumerator ShootAndWaitCooldown()
    {
        Instantiate(ufoProjectile, transform.position, transform.rotation);
        yield return new WaitForSeconds(timeoutSeconds);
        shouldShoot = true;
    }
}
