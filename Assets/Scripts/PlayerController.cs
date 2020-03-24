using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;

    float rotationSpeed = 200.0f;
    float engineForce = 2f;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

        GetComponent<Rigidbody2D>().AddForce(transform.up * engineForce * Input.GetAxis("Vertical"));

        HandleShooting();
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }
    }
}
