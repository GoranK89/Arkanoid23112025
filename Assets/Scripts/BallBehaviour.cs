using System;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public static Action onBallBottomBoundary;

    [SerializeField] private float ballSpeed = 10f;
    [SerializeField] private AudioClip[] ballCollisionSounds;

    private PlayerController PlayerController;

    private float screenBoundaryX;
    private float screenBoundaryY;
    private float ballColliderHalfWidth;

    private Vector2 ballVelocity;

    void Start()
    {
        screenBoundaryX = UtilsClass.GetScreenBoundaryX();
        screenBoundaryY = UtilsClass.GetScreenBoundaryY();
        ballColliderHalfWidth = UtilsClass.GetColliderHalfWitdh(GetComponent<Collider2D>());
        PlayerController = FindFirstObjectByType<PlayerController>();
        
        Brick.onBrickHit += IncreaseBallSpeed;

        // Initialize velocity with random diagonal direction
        float angle = UnityEngine.Random.Range(30f, 150f) * Mathf.Deg2Rad; // Random angle, then converted to radians
        ballVelocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * ballSpeed; // convert angle to x and y components
    }

    void Update()
    {
        MoveBall();
        CheckBoundaries();
    }

    private void MoveBall()
    {
        if (GameManager.Instance.ballIsLaunched)
        {
            transform.position += (Vector3)ballVelocity * Time.deltaTime;
        }
        else
        {
            // ball is "glued" to player paddle
            transform.position = new Vector3(PlayerController.GetCurrentPlayerPositionX(), -11f, 0);
        }


    }

    private void CheckBoundaries()
    {
        // Bounce off left/right walls
        if (transform.position.x + ballColliderHalfWidth >= screenBoundaryX ||
            transform.position.x - ballColliderHalfWidth <= -screenBoundaryX)
        {
            ballVelocity.x = -ballVelocity.x;
            SoundFXManager.Instance.PlayRandomSoundFXClip(ballCollisionSounds, transform, 1f);
        }

        // Bounce off top wall
        if (transform.position.y + ballColliderHalfWidth >= screenBoundaryY)
        {
            ballVelocity.y = -ballVelocity.y;
            SoundFXManager.Instance.PlayRandomSoundFXClip(ballCollisionSounds, transform, 1f);
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

            SoundFXManager.Instance.PlayRandomSoundFXClip(ballCollisionSounds, transform, 1f);
        }

        // Normal deflection (walls, bricks)
        Vector2 normal = collision.GetContact(0).normal;
        ballVelocity = Vector2.Reflect(ballVelocity, normal);
    }
    
    private void IncreaseBallSpeed(int points)
    {
        ballVelocity = ballVelocity.normalized * (ballVelocity.magnitude + GameManager.Instance.ballVelocityIncreaseFactor);
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from events, otherwise causes issues with scene reloads (keeps adding more subscriptions)
        Brick.onBrickHit -= IncreaseBallSpeed;
    }
}
