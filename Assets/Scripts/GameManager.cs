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
    }

    private void HandleBallBottomBoundary()
    {
        UIManager.Instance.UpdateScore(-100);
        UIManager.Instance.UpdateLives(-1);
    }
}
