using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;

    GameManager gameManager;
    Rigidbody2D rig;
    private SpriteRenderer shipSprite;
    private SpriteRenderer fireSprite;

    const float rotationSpeed = 300.0f;
    const float engineForce = 8f;
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

    public AudioClip shot;
    public AudioClip engine;
    AudioSource gunAudio;
    AudioSource engineAudio;

    public bool IsMovementAllowed
    {
        get
        {
            return 
                (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && 
                !isTeleportingNow;
        }
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rig = GetComponent<Rigidbody2D>();
        shipSprite = GetComponent<SpriteRenderer>();
        fireSprite = transform.Find("Fire").GetComponent<SpriteRenderer>();

        gunAudio = gameManager.AddAudio(gameObject, 0.5f);
        engineAudio = gameManager.AddAudio(gameObject, 0.2f);
        engineAudio.clip = engine;

        StartCoroutine(HandleEngineFire());
    }

    void FixedUpdate()
    {
        HandleEngine();        
    }

    void Update()
    {        
        HandleMovementFeatures();
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

    private void HandleEngine()
    {
        if (IsMovementAllowed)
        {
            rig.AddForce(transform.up * engineForce);
        }
    }

    // all about movement except adding force for forward moving
    private void HandleMovementFeatures()
    {
        if (IsMovementAllowed)
        {
            isShowEngineFire = true;

            if (!engineAudio.isPlaying)
            {
                engineAudio.Play();
            }            
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            isShowEngineFire = false;
            fireSprite.enabled = false;

            engineAudio.Stop();            
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
        yield return null;

        while (isInvincible)
        {
            yield return null;

            shipBlinkingTimer += Time.deltaTime;
            if (shipBlinkingTimer > shipBlinkingInterval)
            {
                shipSprite.enabled = !shipSprite.enabled;
                shipBlinkingTimer = 0;
            }
        }
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var projectilePos = transform.position + new Vector3(0, 0, 0.1f); 
            Instantiate(projectile, projectilePos, transform.rotation);
            gunAudio.PlayOneShot(shot);
        }
    }

    private IEnumerator HandleEngineFire()
    {
        while (true)
        {
            yield return null;

            while (isShowEngineFire && !isTeleportingNow)
            {
                yield return null;

                fireBlinkingTimer += Time.deltaTime;
                if (fireBlinkingTimer > fireBlinkingInterval)
                {
                    fireSprite.enabled = !fireSprite.enabled;
                    fireBlinkingTimer = 0;
                }
            }

            fireSprite.enabled = false;            
        }
    }
}
