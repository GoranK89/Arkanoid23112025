using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BallBehaviour.onBallBottomBoundary += HandleBallBottomBoundary;
    }

    private void HandleBallBottomBoundary()
    {
        UIManager.Instance.UpdateScore(-50);
        UIManager.Instance.UpdateLives(-1);
    }
}
