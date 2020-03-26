using System.Collections;
using UnityEngine;

public class UfoMoving : MonoBehaviour
{
    public float speed;
    public float changingDirectionTimeout;
    bool shouldChangeDirection = false;

    int movementXDirection;
    float[] yAngles = new float[] { -0.5f, 0, 0.5f };
    Vector2 movement;
    float xMovement;
    float yMovement;


    // Start is called before the first frame update
    void Start()
    {
        // choose direction opposite to side you've appeared    
        movementXDirection = transform.position.x < 0 ? 1 : -1;
        xMovement = 0.5f * movementXDirection;        
        CalculateMovement();        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {   
        transform.Translate(movement * speed * Time.deltaTime);

        if (shouldChangeDirection)
        {
            CalculateMovement();            
        }
    }

    // accidentally move by 45 degree for a a couple of seconds, than move horizontally again
    private IEnumerator WaitTimeout()
    {
        yield return new WaitForSeconds(changingDirectionTimeout);
        shouldChangeDirection = true;
    }

    private void CalculateMovement()
    {
        yMovement = yAngles[Random.Range(0, yAngles.Length)];
        movement = new Vector2(xMovement, yMovement);
        movement.Normalize();
        shouldChangeDirection = false;
        StartCoroutine(WaitTimeout());
    }
}
