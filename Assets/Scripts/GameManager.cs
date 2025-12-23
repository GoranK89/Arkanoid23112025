using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool gameIsOver;
    public bool paddleCanMove;
    public bool ballIsLaunched;

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
        UIManager.Instance.UpdateScore(-100);
        UIManager.Instance.UpdateLives(-1);
    }

    private void HandleBrickHit(int points)
    {
        UIManager.Instance.UpdateScore(points);
    }
}
