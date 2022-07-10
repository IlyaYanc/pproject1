using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "GroundTile", menuName = "GroundTile")]
public class GroundTile : Tile
{
    public enum GroundType
    {
        Ground,
        Wall
    }

    [SerializeField] private GroundType m_groundType;

    public GroundType Type => m_groundType;
}
