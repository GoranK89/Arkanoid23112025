using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
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
                GameObject brickPrefab = Instantiate(brickData.prefab, transform);

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
