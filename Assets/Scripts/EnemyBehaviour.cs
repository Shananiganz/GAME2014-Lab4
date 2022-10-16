using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Boundary horizontalBoundary;
    public Boundary verticalBoundary;
    public Boundary screenBounds;
    public float horizontalSpeed;
    public float verticalSpeed;
    public Color randomColor;
    public SpriteRenderer spriteRenderer;

    [Header("Bullet Properties")]
    public Transform bulletSpawnPoint;
    public BulletManager bulletManager;
    public float fireRate = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bulletManager = FindObjectOfType<BulletManager>();
        ResetEnemy();
        InvokeRepeating("FireBullets", 0.0f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    void Move()
    {
        var boundaryLength = horizontalBoundary.max - horizontalBoundary.min;
        transform.position = new Vector3(Mathf.PingPong(Time.time * horizontalSpeed, boundaryLength) - horizontalBoundary.max,
            transform.position.y - verticalSpeed * Time.deltaTime, 0.0f);
    }

    public void CheckBounds()
    {
        if (transform.position.y < screenBounds.min)
        {
            ResetEnemy();
        }
    }

    public void ResetEnemy()
    {
        var startingXPosition = Random.Range(horizontalBoundary.min, horizontalBoundary.max);
        var startingYPosition = Random.Range(verticalBoundary.min, verticalBoundary.max);
        horizontalSpeed = Random.Range(1.0f, 6.0f);
        verticalSpeed = Random.Range(1.0f, 3.0f);
        transform.position = new Vector3(startingXPosition, startingYPosition, 0.0f);

        var colorArray = new List<Color>();
        colorArray.Add(Color.red);
        colorArray.Add(Color.yellow);
        colorArray.Add(Color.magenta);
        colorArray.Add(Color.cyan);
        colorArray.Add(Color.white);
        colorArray.Add(Color.white);

        randomColor = colorArray[Random.Range(0, 6)];
        spriteRenderer.material.SetColor("_Color", randomColor);
    }

    void FireBullets()
    {
        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.ENEMY);
    }
}
