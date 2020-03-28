using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;

    GameManager gameManager;
    Rigidbody2D rig;
    SpriteRenderer sprite;

    const float rotationSpeed = 200.0f;
    const float engineForce = 2f;
    const float timeoutBeforeTeleporting = 0.6f;    
    bool canTeleport = true;

    const float invincibilityTimeout = 3;
    float blinkingTimer = 0;
    const float blinkingInterval = 0.15f;
    bool isInvincible = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMoving();
        HandleShooting();
        HandleTeleport();
        HandleBlinking();
    }

    private void HandleBlinking()
    {
        if (isInvincible)
        {
            blinkingTimer += Time.deltaTime;
            if (blinkingTimer > blinkingInterval)
            {
                sprite.enabled = !sprite.enabled;
                blinkingTimer = 0;
            }
        }
    }

    private void HandleMoving()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rig.AddForce(transform.up * engineForce);
        }

        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
    }

    private void HandleTeleport()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canTeleport)
        {              
            StartCoroutine(Teleport());
        }
    }

    public IEnumerator Teleport(bool lifeWasLost = false)
    {
        canTeleport = false;

        // disappear player
        GetComponent<Collider2D>().enabled = false;
        sprite.enabled = false;

        // wait for timeout
        yield return new WaitForSeconds(timeoutBeforeTeleporting);

        // appear at new pos
        if (lifeWasLost) 
        {
            isInvincible = true;
            transform.position = Vector2.zero;
            transform.rotation = Quaternion.identity;
        }
        else // if just teleporting
        {
            var newPos = gameManager.GetRandomPosition();
            transform.position = newPos;
        }

        // stop movement
        rig.velocity = Vector2.zero;
        rig.angularVelocity = 0f;

        // show player, handle invincibility (be invincible if life was lost)
        if (isInvincible)
        {
            Debug.Log($"Inside if (isInvincible) before StartCoroutine(BeInvincible()). isInvincible: {isInvincible}");            
            StartCoroutine(BeInvincible());
        }
        else
        {
            sprite.enabled = true;
            GetComponent<Collider2D>().enabled = true;

            canTeleport = true;
        }
    }

    private IEnumerator BeInvincible()
    {
        Debug.Log($"Inside BeInvincible. isInvincible: {isInvincible}");

        yield return new WaitForSeconds(invincibilityTimeout);

        isInvincible = false;

        sprite.enabled = true;
        GetComponent<Collider2D>().enabled = true;
                
        canTeleport = true;
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }
    }
}
