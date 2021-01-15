using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float speedRotation = 0f;
    [SerializeField] float engineThrust = 0f;

    [SerializeField] GameObject flameForward;
    [SerializeField] GameObject flameBack;
    [SerializeField] GameObject fireBallPrefab;

    [SerializeField] Rigidbody2D myRigidbody;

    [SerializeField] Transform centralPoint;
    [SerializeField] Transform directionPoint;
    [SerializeField] Transform fireGun1;
    [SerializeField] Transform fireGun2;

    [SerializeField] Vector3 directionVector;
    [SerializeField] Vector3 rotationSpeed;

    public Vector3 CentralPoint { get { return centralPoint.position; } }
    public Vector3 DirectionPoint { get { return directionPoint.position; } }
    public Vector3 DirectionVector 
    {   get 
            { 
                return directionVector; 
            }
        set 
            {
                directionVector = value;   
        }
    }

    private void Awake()
    {
        CalculateDirectionVector();
    }

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rotationSpeed != Vector3.zero)
            transform.eulerAngles += rotationSpeed;

        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Q))
            rotationSpeed += AddRotationSpeed();
        if (Input.GetKey(KeyCode.W) || Input.GetKeyUp(KeyCode.W))
            MainEngineIgnition();
        if (Input.GetKey(KeyCode.S) || Input.GetKeyUp(KeyCode.S))
            BrakeEnginesIgnition();
        if (Input.GetKeyDown(KeyCode.Space))
            Fire();
    }

    Vector3 CalculateDirectionVector()
    {
        DirectionVector = DirectionPoint - CentralPoint;
        return DirectionVector;
    }

    Vector3 AddRotationSpeed()
    {
        if (Input.GetKey(KeyCode.E))
            return new Vector3(0, 0, -speedRotation * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q))
            return new Vector3(0, 0, speedRotation * Time.deltaTime);
        return Vector3.zero;
    }

    void MainEngineIgnition()
    {
        myRigidbody.AddForce(CalculateDirectionVector() * engineThrust, ForceMode2D.Force);
        flameForward?.SetActive(true);

        if (Input.GetKeyUp(KeyCode.W))
            flameForward.SetActive(false);
    }

    void BrakeEnginesIgnition()
    {
        myRigidbody.AddForce(CalculateDirectionVector() * -engineThrust, ForceMode2D.Force);
        flameBack?.SetActive(true);

        if (Input.GetKeyUp(KeyCode.S))
            flameBack.SetActive(false);
    }

    void Fire()
    {
        CalculateDirectionVector();
        GameObject.Instantiate(fireBallPrefab, fireGun1.position, fireGun1.rotation);
        GameObject.Instantiate(fireBallPrefab, fireGun2.position, fireGun2.rotation);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GameSpace")
        {
            if (transform.position.y > 5.3f || transform.position.y < -5.3f)
                transform.position = new Vector3(transform.position.x, -transform.position.y);
            if (transform.position.x > 9.2f || transform.position.x < -9.2f)
                transform.position = new Vector3(-transform.position.x, transform.position.y);
        }
    }
}
