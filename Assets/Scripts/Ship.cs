using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    [SerializeField] private float forceAmount = 10;
    [SerializeField] private float torqueAmount = .5f;
    [SerializeField] private float rotationSpeed = .2f;
    [SerializeField] private Bullet bullet;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool forceOn = false;
    private bool isRotating = false;
    private float torqueDirection = 0;

    public static event Action OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bullet instantBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            instantBullet.Shoot(transform.up);
            SoundManager.Instance.PlaySound(SoundManager.SoundTypes.Shoot);
        }

        forceOn = Input.GetKey(KeyCode.W);

        if (Input.GetKeyDown(KeyCode.S) && !isRotating)
        {
            StartCoroutine(RotateShip(180, rotationSpeed));
        }

        if (Input.GetKey(KeyCode.A))
        {
            torqueDirection = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            torqueDirection = -1;
        }
        else
        {
            torqueDirection = 0;
        }

        warpAroundBoundary();
    }

    void FixedUpdate()
    {
        if (forceOn)
        {
            rb.AddForce(transform.up * forceAmount);
        }

        if (torqueAmount != 0)
        {
            rb.AddTorque(torqueDirection * torqueAmount);
        }
    }

    IEnumerator RotateShip(float angle, float duration)
    {
        isRotating = true;
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation * Quaternion.Euler(0, 0, angle);

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.rotation = Quaternion.Slerp(from, to, t / duration);
            yield return null;
        }

        transform.rotation = to;
        isRotating = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            OnDeath?.Invoke();
            SceneManager.LoadScene("DeathScene");
        }
    }

    private void warpAroundBoundary()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (x > 8)
            x -= 16;
        if (x < -8)
            x += 16;
        if (y > 4.5f)
            y -= 9;
        if (y < -4.5f)
            y += 9;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
