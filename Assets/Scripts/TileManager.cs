using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    private Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object that collided is the ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Get the contact point
            ContactPoint2D contact = collision.contacts[0];
            Vector3 hitPosition = contact.point;

            // Convert world position to cell position
            Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);

            // Remove the tile at the cell position
            tilemap.SetTile(cellPosition, null);
        }
    }
}
