using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 direction)
    {
        rb.velocity = direction.normalized * speed;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Background")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
    }
}
