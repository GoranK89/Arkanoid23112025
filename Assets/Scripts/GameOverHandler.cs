using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    
   private void Update()
    {
        if (GameManager.Instance.gameIsOver)
        {
            gameOverUI.gameObject.SetActive(true);
        }
    }
   
   public void RestartGame()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
