using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSource : MonoBehaviour
{
    float time = 0f;
    bool check = false;
    [SerializeField] float reloadTime = 0f;

    [SerializeField] List<GameObject> asteroids = null;

    private void Start()
    {
        reloadTime = Random.Range(5f, 10f);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= reloadTime)
        {
            time = 0;
            InstantiateAsteroid();
        }
    }

    void InstantiateAsteroid()
    {
        GameObject asteroid = asteroids[Random.Range(0, asteroids.Count)];
        reloadTime = Random.Range(5f, 10f);
        Instantiate(asteroid, transform.position, transform.rotation);
    }
}