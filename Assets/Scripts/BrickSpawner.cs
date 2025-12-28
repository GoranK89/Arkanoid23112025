using UnityEngine;
using System;

public class BrickSpawner : MonoBehaviour
{
    public static Action<int> onBricksSpawned;
    
    private BricksListSO bricksListSO;

    // Grid settings
    [SerializeField] private float spacingX = 3f;
    [SerializeField] private float spacingY = 1f;


    private void Start()
    {
		bricksListSO = Resources.Load<BricksListSO>("Lists/BricksListSO");
        SpawnBricks(5, 16);
    }

    private void SpawnBricks(int rows, int columns)
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
				BrickSO brickData = bricksListSO.bricksList[col % bricksListSO.bricksList.Count];
                GameObject brickInstance = Instantiate(brickData.prefab, transform);
                brickInstance.GetComponent<Brick>().InitializeBrickData(brickData); // For the Brick script to know its data

                float posX = col * spacingX;
                float posY = row * -spacingY;

                brickInstance.transform.position = new Vector2(posX, posY);
            }
        }
        // Fire event with amount of spawned bricks
        onBricksSpawned?.Invoke(transform.childCount);

        // Center the grid
        float gridW = columns * spacingX;
        float gridH = rows * spacingY;
        transform.position = new Vector2(-gridW / 2 + spacingX / 2, gridH / 0.9f - spacingY / 2);
    }
}
