using System.Collections;
using UnityEngine;

public class UfoShooting : MonoBehaviour
{
    public GameObject ufoProjectile;

    public float speed;
    public float timeoutSeconds;

    GameManager gameManager;

    bool shouldShoot = true;

    public AudioClip shot;
    AudioSource gunAudio;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gunAudio = gameManager.AddAudio(gameObject, 0.5f);
    }

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
        gunAudio.PlayOneShot(shot);
        yield return new WaitForSeconds(timeoutSeconds);
        shouldShoot = true;
    }
}
