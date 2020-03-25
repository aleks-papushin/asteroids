using UnityEngine;

public class UfoProjectileRandomMovement : MonoBehaviour
{
    public float speed;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        movement.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
