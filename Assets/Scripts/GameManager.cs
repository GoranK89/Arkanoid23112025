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
        paddleCanMove = true;
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
    }

    private void HandleBrickHit(int points)
    {
        currentScore += points;
    }
}
