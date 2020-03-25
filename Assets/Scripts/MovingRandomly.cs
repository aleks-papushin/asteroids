using UnityEngine;

public class MovingRandomly : MonoBehaviour
{
    public float speed;    
    public float rotatingSpeed;

    Vector2 movement;
    float rotation;

    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector2(Random.Range(-speed, speed), Random.Range(-speed, speed));
        rotation = Random.Range(-rotatingSpeed, rotatingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movement * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward, rotation * Time.deltaTime);
    }
}
