using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private Transform livesContainer;

    private int currentScore = 0;
    private int currentLives = 2;

    private void Awake()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        livesContainer = transform.Find("LivesContainer");
        UpdateLives(currentLives);
        UpdateScore(0);
    }

    private void UpdateLives(int lifeChange)
    {
        if (lifeChange > 0)
        {
            Image lifeImage = livesContainer.Find("image").GetComponent<Image>();
            for (int i = 0; i < lifeChange; i++)
            {
                Vector3 offset = new Vector3(-i * 30f, 0, 0);
                Instantiate(lifeImage, livesContainer.position + offset, Quaternion.identity, livesContainer);
                currentLives += lifeChange;
            }
        }
        else if (lifeChange < 0)
        {
            for (int i = 0; i < -lifeChange; i++)
            {
                if (livesContainer.childCount > 0)
                {
                    Transform lastLife = livesContainer.GetChild(livesContainer.childCount - 1);
                    Destroy(lastLife.gameObject);
                    currentLives += lifeChange;
                }
            }
        }

    }

    public void UpdateScore(int AddedScore)
    {
        currentScore += AddedScore;
        scoreText.SetText("Score: " + currentScore);
    }
}
