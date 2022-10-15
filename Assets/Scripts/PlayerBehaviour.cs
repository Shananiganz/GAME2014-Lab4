using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Properties")]
    public float speed = 2.0f;
    public Boundary boundary;
    public float verticalPosition;
    public bool usingMobileInput = false;
    public ScoreManager scoreManager;

    [Header("Bullet Properties")]
    public Transform bulletSpawnPoint;
    public BulletManager bulletManager;
    public float fireRate = 0.2f;

    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();

        scoreManager = FindObjectOfType<ScoreManager>();

        transform.position = new Vector2(0.0f, verticalPosition);
        camera = Camera.main;

        // Platform Detection for input
        usingMobileInput = Application.platform == RuntimePlatform.Android ||
                           Application.platform == RuntimePlatform.IPhonePlayer;

        InvokeRepeating("FireBullets", 0.0f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (usingMobileInput)
        {
            GetMobileInput();
        }
        else
        {
            GetConventionalInput();
        }

        Move();

        if (Input.GetKeyDown(KeyCode.K))
        {
            scoreManager.AddPoints(10);
        }
    }

    void GetConventionalInput()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        transform.position += new Vector3(x, 0, 0);
    }

    void GetMobileInput()
    {

        foreach (Touch touch in Input.touches)
        {
            var destination = camera.ScreenToWorldPoint(touch.position);
            transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime * speed);
        }
    }

    void Move()
    {
        float clampedXPosition = Mathf.Clamp(transform.position.x, boundary.min, boundary.max);
        transform.position = new Vector2(clampedXPosition, verticalPosition);
    }

    void FireBullets()
    {
        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletDirection.UP);
    }

}