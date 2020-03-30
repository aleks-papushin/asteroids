using UnityEngine;

public class PlayerProjectileMoving : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
