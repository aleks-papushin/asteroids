using System.Collections;
using UnityEngine;

public class UfoMoving : MonoBehaviour
{
    public float speed;
    public float changingDirectionTimeout;

    GameManager gameManager;

    bool pickNewDirection = true;
    int xDirection;
    float[] yAngles = new float[] { -0.5f, 0.5f, 0 };
    Vector2 movement;
    float xMovement;
    float? yMovement = null;

    public AudioClip engine;
    AudioSource engineAudio;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        // choose direction opposite to side you've appeared    
        xDirection = transform.position.x < 0 ? 1 : -1;
        xMovement = 0.5f * xDirection;

        engineAudio = gameManager.AddAudio(gameObject, 0.2f);
        engineAudio.clip = engine;
        engineAudio.Play();

        CalculateMovement();        
    }

    void Update()
    {
        Move();
    }

    void Move()
    {   
        if (pickNewDirection)
        {            
            CalculateMovement();            
        }

        transform.Translate(movement * speed * Time.deltaTime);
    }

    // accidentally move by 45 degree for a a couple of seconds, than move horizontally again
    private IEnumerator WaitThenChangeDirection()
    {
        yield return new WaitForSeconds(changingDirectionTimeout);        
        pickNewDirection = true;
    }

    private void CalculateMovement()
    {
        ChangeYDirection();
        movement = new Vector2(xMovement, (float)yMovement);
        movement.Normalize();
        pickNewDirection = false;
        StartCoroutine(WaitThenChangeDirection());
    }

    private void ChangeYDirection()
    {
        // if null pick any value        
        if (yMovement == null)
        {
            yMovement = yAngles[Random.Range(0, yAngles.Length)];
        }
        // if zero pick any value except 0 (the last array member)
        else if (yMovement == 0)
        {            
            yMovement = yAngles[Random.Range(0, yAngles.Length - 1)];
        }
        // if 0.5 or -0.5 or other, make 0
        else 
        {
            yMovement = 0;
        }
    }
}
