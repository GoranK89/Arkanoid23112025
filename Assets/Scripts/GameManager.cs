using UnityEngine;

public class GameManager : MonoBehaviour
{
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
