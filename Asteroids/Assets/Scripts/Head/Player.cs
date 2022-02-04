using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 targetPlayerInput;
    [SerializeField] float maxSpeed;
    [SerializeField] float accelerationSpeed;
    [SerializeField] float rotationSpeed;
    Shooter shooter;
    private float t = 0f;
    float speed;

    private Vector3 minCorner;
    private Vector3 maxCorner;

    private Vector2 playerBoundaries;
    
    void Start()
    {
        minCorner = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        maxCorner = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        playerBoundaries.x = GetComponent<PolygonCollider2D>().bounds.extents.x;
        playerBoundaries.y = GetComponent<PolygonCollider2D>().bounds.extents.y;

        shooter = GetComponent<Shooter>();
    }

    void Update()
    {
        Move();
        OutOfBoundaries();
    }

    private void OnFire(InputValue value)
    {
        shooter.TryShootingBeam();
    }

    private void OnMoveUpRightLeft(InputValue value)
    {
        targetPlayerInput = value.Get<Vector2>();
        
        if (targetPlayerInput.y == 0)
            accelerationSpeed = Mathf.Abs(accelerationSpeed) * (-1);
        else
            accelerationSpeed = Mathf.Abs(accelerationSpeed);
    }

    private void Move()
    {
        speed = Mathf.Lerp(0, maxSpeed, t*t);
        t += Time.deltaTime * accelerationSpeed;
        t = Mathf.Clamp(t,0,1);

        transform.Rotate(new Vector3(0,0,targetPlayerInput.x * rotationSpeed * -1));

        speed *= Time.deltaTime;

        float cosRaw = Mathf.Cos(transform.localEulerAngles.z * Mathf.Deg2Rad);
        float sinRaw = Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad * -1);

        transform.position += new Vector3(speed * sinRaw,speed * cosRaw);
    }

    private void OutOfBoundaries()
    {        
        if (Mathf.Abs(transform.position.x) > maxCorner.x + playerBoundaries.x)
        {
            float offSet = Mathf.Sign(transform.position.x) * (playerBoundaries.x / 3);
            transform.position = new Vector2(transform.position.x * -1 + offSet,transform.position.y);
        }

        if (Mathf.Abs(transform.position.y) > maxCorner.y + playerBoundaries.y)
        {
            float offSet = Mathf.Sign(transform.position.y) * (playerBoundaries.y / 3);
            transform.position = new Vector3(transform.position.x,transform.position.y * -1 + offSet,transform.position.z);
        }
    }

    public float GetSpeed()
    {
        return speed / Time.deltaTime;
    }
    
    public float GetAngle()
    {
        return transform.eulerAngles.z;
    }

    public Vector2 GetCoordinates()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }
}
