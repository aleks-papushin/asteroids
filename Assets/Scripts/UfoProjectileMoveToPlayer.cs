using UnityEngine;

public class UfoProjectileMoveToPlayer : MonoBehaviour
{
    public float speed;

    GameObject player;
    
    Vector2 targetDirection;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targetDirection = GetPlayerPosition() - (Vector2)transform.position;
        targetDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(targetDirection * speed * Time.deltaTime);
    }

    Vector2 GetPlayerPosition()
    {
        return player.transform.position;
    }
}
