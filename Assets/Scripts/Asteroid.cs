using System;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float pointsModifier = 10;

    [Header("Sprites")]
    [SerializeField] private Sprite[] sprites;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PolygonCollider2D pc;

    public static event Action<int> OnHit;
    public static event Action OnCrash;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pc = GetComponent<PolygonCollider2D>();
    }

    public void Kick(float theMass, Vector2 direction)
    {
        sr.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];

        List<Vector2> path = new List<Vector2>();
        sr.sprite.GetPhysicsShape(0, path);
        pc.SetPath(0, path.ToArray());

        rb.mass = theMass;
        float width = UnityEngine.Random.Range(0.75f, 1.33f);
        float height = 1 / width;
        transform.localScale = new Vector2(width, height) * theMass;

        rb.velocity = direction.normalized * speed;
        rb.AddTorque(UnityEngine.Random.Range(-4f, 4f));
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Background")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            SoundManager.Instance.PlaySound(SoundManager.SoundTypes.Boom);
            if (rb.mass > 0.7f)
            {
                Split();
                Split();
            }
            OnHit?.Invoke((int)(rb.mass * pointsModifier));
            Destroy(gameObject);
        }
        if (other.tag == "Planet")
        {
            OnCrash?.Invoke();
            Destroy(gameObject);
        }
    }

    void Split()
    {
        Vector2 position = this.transform.position;
        position += UnityEngine.Random.insideUnitCircle * 0.5f;

        Asteroid small = Instantiate(this, position, this.transform.rotation);
        Vector2 direction = UnityEngine.Random.insideUnitCircle;
        float mass = rb.mass / 2;
        small.Kick(mass, direction);
    }
}
