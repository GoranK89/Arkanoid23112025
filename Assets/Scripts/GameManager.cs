using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool gameIsOver;
    public bool paddleCanMove;
    public bool ballIsLaunched;
    
    public int currentScore = 0;
    public int currentLives = 3;

    private void Awake()
    {
        Instance = this;

        gameIsOver = false;
        ballIsLaunched = false;
    }

    void Start()
    {
        BallBehaviour.onBallBottomBoundary += HandleBallBottomBoundary;
        Brick.onBrickHit += HandleBrickHit;
    }

    private void HandleBallBottomBoundary()
    {
        currentLives--;
        ballIsLaunched = false;
        BallSpawner.Instance.SpawnBall();
        
        if (currentLives == 0)
        {
            gameIsOver = true;
        }
    }

    private void HandleBrickHit(int points)
    {
        currentScore += points;
    }

private void OnDestroy()
    {
        // Unsubscribe from events, otherwise causes issues with scene reloads (keeps adding more subscriptions)
       	BallBehaviour.onBallBottomBoundary -= HandleBallBottomBoundary;
        Brick.onBrickHit -= HandleBrickHit;
    }
}
