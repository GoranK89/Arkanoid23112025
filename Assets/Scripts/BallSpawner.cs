using System.Numerics;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    private GameObject spawnedBall;

    private void Start()
    {
        SpawnBall();
    }

    public void SpawnBall()
    {
        float currentPlayerPositionX = FindFirstObjectByType<PlayerController>().GetCurrentPlayerPositionX();
        spawnedBall = Instantiate(ballPrefab, new UnityEngine.Vector3(currentPlayerPositionX, -11f, 0), UnityEngine.Quaternion.identity);
    }
}
