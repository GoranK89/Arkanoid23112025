using System;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public static Action onBallBottomBoundary;

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
        float angle = UnityEngine.Random.Range(30f, 150f) * Mathf.Deg2Rad; // Random angle, then converted to radians
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
        if (transform.position.y + ballColliderHalfWidth >= screenBoundaryY)
        {
            ballVelocity.y = -ballVelocity.y;
        }

        // Destroy ball if it goes below the screen and fire event
        if (transform.position.y - ballColliderHalfWidth <= -screenBoundaryY)
        {
            onBallBottomBoundary?.Invoke();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ball collision logic
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D playerCollider = collision.gameObject.GetComponent<Collider2D>();
            float playerLeftEdge = playerCollider.bounds.min.x;
            float playerRightEdge = playerCollider.bounds.max.x;
            float playerCenter = (playerLeftEdge + playerRightEdge) / 2f;

            float ballX = transform.position.x;
            float relativePosition = (ballX - playerCenter) / (playerRightEdge - playerCenter);
            relativePosition = Mathf.Clamp(relativePosition, -1f, 1f);

            // Apply angle based on player position
            float angleOffset = relativePosition * 45f; // Max 45deg deviation
            ballVelocity = Quaternion.AngleAxis(angleOffset, Vector3.forward) * ballVelocity;
        }

        // Normal deflection (walls, bricks)
        Vector2 normal = collision.GetContact(0).normal;
        ballVelocity = Vector2.Reflect(ballVelocity, normal);
    }
}
