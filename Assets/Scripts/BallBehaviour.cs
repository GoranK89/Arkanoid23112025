using System.Runtime.CompilerServices;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private float screenBoundaryX;
    private float screenBoundaryY;
    private float ballColliderHalfWidth;

    private Vector2 ballVelocity;

    [SerializeField] private float ballSpeed = 15f;

    void Start()
    {
        screenBoundaryX = UtilsClass.GetScreenBoundaryX();
        screenBoundaryY = UtilsClass.GetScreenBoundaryY();
        ballColliderHalfWidth = UtilsClass.GetColliderHalfWitdh(GetComponent<Collider2D>());

        // Initialize velocity with random diagonal direction
        float angle = Random.Range(30f, 150f) * Mathf.Deg2Rad; // Random angle, then converted to radians
        ballVelocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * ballSpeed; // convert angle to x and y components
    }

    void FixedUpdate()
    {
        MoveBall();
        CheckBoundaries();
    }

    private void MoveBall()
    {
        transform.position += (Vector3)ballVelocity * Time.fixedDeltaTime;
    }

    private void CheckBoundaries()
    {
        // Bounce off left/right walls
        if (transform.position.x + ballColliderHalfWidth >= screenBoundaryX ||
            transform.position.x - ballColliderHalfWidth <= -screenBoundaryX)
        {
            ballVelocity.x = -ballVelocity.x;
        }

        // Bounce off top/bottom walls
        if (transform.position.y + ballColliderHalfWidth >= screenBoundaryY ||
            transform.position.y - ballColliderHalfWidth <= -screenBoundaryY)
        {
            ballVelocity.y = -ballVelocity.y;
        }
    }
}
