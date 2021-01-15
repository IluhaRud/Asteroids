using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float asteroidSpeed = 0f;

    [SerializeField] Vector3 centralPoint;
    [SerializeField] Vector3 directionPoint;
    [SerializeField] Vector3 directionVector;

    [SerializeField] Rigidbody2D myRigidbody;

    private void Start()
    {
        if (gameObject.tag == "AsteroidHuge")
            asteroidSpeed = Random.Range(0.1f, 0.3f);

        myRigidbody = GetComponent<Rigidbody2D>();
        centralPoint = transform.localPosition;
        directionPoint = new Vector3(Random.Range(-8, 8), Random.Range(-5, 5));
        directionVector = directionPoint - centralPoint;

        myRigidbody.AddForce(directionVector * asteroidSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GameSpace")
            Destroy(gameObject);
    }
}
