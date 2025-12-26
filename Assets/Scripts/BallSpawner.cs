using System.Numerics;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance { get; private set; }
    
    [SerializeField] private GameObject ballPrefab;
    private GameObject spawnedBall;

    private void Awake()
    {
        Instance = this;
    }
    
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
