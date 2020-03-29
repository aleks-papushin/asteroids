using UnityEngine;

public class PlayerProjectileMoving : MonoBehaviour
{
    GameObject player;

    public float speed;
    public float playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerVelocity = player.GetComponent<Rigidbody2D>().velocity.magnitude;
        speed += playerVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
