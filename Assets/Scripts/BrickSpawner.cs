using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] brickPrefabs;

    // Grid settings
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 16;
    [SerializeField] private float spacingX = 3f;
    [SerializeField] private float spacingY = 1f;


    private void Start()
    {
        SpawnBricks();
    }

    private void SpawnBricks()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                GameObject brickPrefab = Instantiate(brickPrefabs[Random.Range(0, brickPrefabs.Length)], transform);

                float posX = col * spacingX;
                float posY = row * -spacingY;

                brickPrefab.transform.position = new Vector2(posX, posY);
            }
        }

        // Center the grid
        float gridW = columns * spacingX;
        float gridH = rows * spacingY;
        transform.position = new Vector2(-gridW / 2 + spacingX / 2, gridH / 2 - spacingY / 2);
    }
}
