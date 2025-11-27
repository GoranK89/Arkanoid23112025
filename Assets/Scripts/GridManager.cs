using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Camera mainCamera;
    private Grid grid;

    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private int gridRows = 12;
    [SerializeField] private int gridColumns = 13;

    void Start()
    {
        grid = GetComponent<Grid>();

        AdjustGridSize();
        PlaceBlock(0, 0); // Example: Place a block at the top-left corner
    }

    private void AdjustGridSize()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calculate the cell size based on the camera size and grid dimensions
        float cellWidth = cameraWidth / gridColumns;
        float cellHeight = cameraHeight / gridRows;

        grid.cellSize = new Vector3(cellWidth, cellHeight, 0);

    }

    public void PlaceBlock(int row, int column)
    {
        Vector3Int cellPosition = new Vector3Int(column, row, 0);
        Vector3 worldPosition = grid.CellToWorld(cellPosition) + grid.cellSize / 2;

        Instantiate(blockPrefab, worldPosition, Quaternion.identity);
    }
}
