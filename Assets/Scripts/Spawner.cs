using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Asteroid asteroid;
    [SerializeField] private float spawnRate = 2;
    [SerializeField] private float spawnDistance = 14;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, spawnRate);
    }

    private void Spawn()
    {
        Vector2 spawnPoint = Random.insideUnitCircle.normalized * spawnDistance;

        float angle = Random.Range(-15, 15);
        Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        Asteroid instantAsteroid = Instantiate(asteroid, spawnPoint, rotation);

        Vector2 direction = rotation * -spawnPoint;
        float mass = Random.Range(.5f, 1.1f);
        instantAsteroid.Kick(mass, direction);
    }
}
