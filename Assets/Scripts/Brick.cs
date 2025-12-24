using UnityEngine;
using System;

public class Brick : MonoBehaviour
{
    public static Action<int> onBrickHit;
    
    private BrickSO brickData;
    
    public void InitializeBrickData(BrickSO data)
    {
        brickData = data;
    }
    
   private void OnCollisionEnter2D(Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Ball"))
       {
           onBrickHit?.Invoke(brickData.points);
           SoundFXManager.Instance.PlayRandomSoundFXClip(brickData.hitSound, transform, 1f);
           Destroy(gameObject);
       }
   }
}
