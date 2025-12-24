using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   public void PlayGame()
   {
      SceneManager.LoadSceneAsync("Level1");
   }
   
   public void ResetGame()
   {
      Debug.Log("Reset the level to initial state.");
   }
}
