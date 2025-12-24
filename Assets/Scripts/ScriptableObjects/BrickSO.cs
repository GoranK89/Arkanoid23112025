using UnityEngine;

[CreateAssetMenu(fileName = "BrickSO", menuName = "Scriptable Objects/BrickSO")]
public class BrickSO : ScriptableObject
{
    public string brickName;
    public int health;
    public int points;
    public GameObject prefab;
    public AudioClip[] hitSound;
}
