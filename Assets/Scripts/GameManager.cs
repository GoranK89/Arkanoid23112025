using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool gameIsOver;
    public bool paddleCanMove;
    public bool ballIsLaunched;
    
	public float ballVelocityIncreaseFactor = 0.2f;
    public int currentScore = 0;
    public int currentLives = 3;
    public int totalBricks;

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
        BrickSpawner.onBricksSpawned += HandleBricksSpawned;
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
        totalBricks--;

		// Check for level completion
		LevelComplete();
    }
    
    private void HandleBricksSpawned(int bricksCount)
    {
        totalBricks = bricksCount;
    }
    
    private void LevelComplete()
    {
        if(totalBricks == 0)
        {
            SceneManager.LoadSceneAsync("GameMenu");
        }
    }

private void OnDestroy()
    {
        // Unsubscribe from events, otherwise causes issues with scene reloads (keeps adding more subscriptions)
       	BallBehaviour.onBallBottomBoundary -= HandleBallBottomBoundary;
        Brick.onBrickHit -= HandleBrickHit;
        BrickSpawner.onBricksSpawned -= HandleBricksSpawned;
    }
}
