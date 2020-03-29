using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;

    GameManager gameManager;
    Rigidbody2D rig;
    public SpriteRenderer shipSprite;
    public SpriteRenderer fireSprite;

    const float rotationSpeed = 200.0f;
    const float engineForce = 2f;
    const float timeoutBeforeTeleporting = 0.6f;    
    bool canTeleport = true;
    bool isTeleportingNow = false;

    const float invincibilityTimeout = 3;
    float shipBlinkingTimer = 0;
    const float shipBlinkingInterval = 0.15f;
    bool isInvincible = false;

    float fireBlinkingTimer = 0;
    float fireBlinkingInterval = 0.04f;
    bool isShowEngineFire = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rig = GetComponent<Rigidbody2D>();
        shipSprite = GetComponent<SpriteRenderer>();
        fireSprite = transform.Find("Fire").GetComponent<SpriteRenderer>();

        StartCoroutine(HandleEngineFire());
    }

    void Update()
    {
        HandleMoving();
        HandleShooting();
        HandleTeleport();
    }

    public IEnumerator Teleport(bool isLifeLost = false)
    {
        isTeleportingNow = true;
        canTeleport = false;

        // disappear player
        GetComponent<Collider2D>().enabled = false;
        shipSprite.enabled = false;

        // wait for timeout
        yield return new WaitForSeconds(timeoutBeforeTeleporting);

        // appear at new pos
        if (isLifeLost)
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
            StartCoroutine(BeInvincible());
        }
        else
        {
            shipSprite.enabled = true;
            GetComponent<Collider2D>().enabled = true;

            canTeleport = true;
        }

        isTeleportingNow = false;
    }

    private void HandleMoving()
    {
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !isTeleportingNow)
        {
            rig.AddForce(transform.up * engineForce);
            isShowEngineFire = true;            
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            isShowEngineFire = false;
            fireSprite.enabled = false;
        }

        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
    }

    private void HandleTeleport()
    {
        if (GetTeleportKeysDown() && canTeleport)
        {            
            StartCoroutine(Teleport());
        }
    }

    private bool GetTeleportKeysDown()
    {
        return
            Input.GetKeyDown(KeyCode.LeftShift) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow);
    }

    private IEnumerator BeInvincible()
    {
        StartCoroutine(ShipSpriteBlink());

        yield return new WaitForSeconds(invincibilityTimeout);

        isInvincible = false;

        shipSprite.enabled = true;
        GetComponent<Collider2D>().enabled = true;
                
        canTeleport = true;
    }

    private IEnumerator ShipSpriteBlink()
    {
        while (isInvincible)
        {
            shipBlinkingTimer += Time.deltaTime;
            if (shipBlinkingTimer > shipBlinkingInterval)
            {
                shipSprite.enabled = !shipSprite.enabled;
                shipBlinkingTimer = 0;
            }

            yield return null;
        }

        yield return null;
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }
    }

    private IEnumerator HandleEngineFire()
    {
        while (true)
        {
            while (isShowEngineFire && !isTeleportingNow)
            {
                Debug.Log($"Inside while loop of HandleEngineFire. isShowEngineFire: {isShowEngineFire}");

                fireBlinkingTimer += Time.deltaTime;
                if (fireBlinkingTimer > fireBlinkingInterval)
                {
                    fireSprite.enabled = !fireSprite.enabled;
                    fireBlinkingTimer = 0;
                }

                yield return null;
            }

            fireSprite.enabled = false;
            yield return null;
        }
    }
}
