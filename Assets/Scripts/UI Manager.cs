using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private TextMeshProUGUI scoreText;
    private Transform livesContainer;
    private Image lifeImage;

    private void Awake()
    {
        Instance = this;

        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        livesContainer = transform.Find("LivesContainer");
        lifeImage = livesContainer.Find("image").GetComponent<Image>();
    }
    
    void Start()
    {
        InitUI();
        
        // Subscribe to events
        BallBehaviour.onBallBottomBoundary += RemoveLife;
        Brick.onBrickHit += UpdateScore;
    }

    private void InitUI()
    {
        scoreText.SetText("Score: " + GameManager.Instance.currentScore);
        
        // Generate life images
        for (int i = 0; i < GameManager.Instance.currentLives; i++)
        {
            Vector3 offset = new Vector3(-i * 30f, 0, 0);
            Instantiate(lifeImage, livesContainer.position + offset, Quaternion.identity, livesContainer);
        }
    }
  
    private void RemoveLife()
    {
            for (int i = 0; i < GameManager.Instance.currentLives; i++)
            {
                if (livesContainer.childCount > 0)
                {
                    Transform lastLife = livesContainer.GetChild(livesContainer.childCount - 1);
                    Destroy(lastLife.gameObject);
                }
            }
    }
    
    private void UpdateScore(int points)
    {
        scoreText.SetText("Score: " + GameManager.Instance.currentScore);
    }
}
