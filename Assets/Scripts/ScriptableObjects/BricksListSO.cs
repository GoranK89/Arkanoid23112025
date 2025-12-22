using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BricksListSO", menuName = "Scriptable Objects/BricksListSO")]
public class BricksListSO : ScriptableObject
{
    public List<BrickSO> bricksList;
}
