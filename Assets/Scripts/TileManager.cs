using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField] private AudioClip tileHitSound;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector3 hitPosition = contact.point;
                Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);
                TileBase hitTile = tilemap.GetTile(cellPosition);

                // sometimes tile collision is not detected, this is a temporary fix
                // move the scoring logic to GameManager later
                if (hitTile == null)
                    continue;

                if (hitTile.name == "TileYellow")
                {
                    UIManager.Instance.UpdateScore(10);
                }
                else if (hitTile.name == "TileGreen")
                {
                    UIManager.Instance.UpdateScore(20);
                }
                else if (hitTile.name == "TileBlue")
                {
                    UIManager.Instance.UpdateScore(30);
                }

                tilemap.SetTile(cellPosition, null);
            }
        }
    }
}
