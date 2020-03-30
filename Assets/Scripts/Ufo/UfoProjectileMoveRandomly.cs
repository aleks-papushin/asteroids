using UnityEngine;

public class UfoProjectileMoveRandomly : MonoBehaviour
{
    public float speed;

    Vector2 movement;

    void Start()
    {
        movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        movement.Normalize();
    }

    void Update()
    {
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
