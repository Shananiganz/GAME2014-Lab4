using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBehaviour : MonoBehaviour
{
    public float verticalSpeed;
    public Boundary boundary;

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
        Move(); 
    }

    public void Move()
    {
        transform.position -= new Vector3(0, verticalSpeed, 0);
    }

    public void CheckBounds()
    {
        if (transform.position.y < boundary.min)
        {
            ResetStars();
        }
    }

    public void ResetStars()
    {
        transform.position = new Vector2(0, boundary.max);
    }
    
}
