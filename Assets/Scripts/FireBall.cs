using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBall : MonoBehaviour
{
    public static int Score = 0;

    [SerializeField] float fireBallSpeed;

    [SerializeField] Rigidbody2D myRigidbody;

    [SerializeField] Vector3 directionVector;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        directionVector = GameObject.Find("MainShip").GetComponent<ShipController>().DirectionVector;
        myRigidbody.AddForce(directionVector * fireBallSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AsteroidHuge")
        {
            Score += 100;
            SetNewScore();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    void SetNewScore()
    {
        GameObject.Find("[UI]").transform.Find("ScoreText").GetComponent<Text>().text = $"Score: {Score}";
    }
}
