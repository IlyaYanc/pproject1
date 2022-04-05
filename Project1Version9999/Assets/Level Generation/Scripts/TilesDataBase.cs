using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TilesDataBase", menuName = "LevelGenerator/TilesDataBase")]
public class TilesDataBase : ScriptableObject
{
    public TileBase connectionUp;
    public TileBase connectionDown;
    public TileBase connectionLeft;
    public TileBase connectionRight;
    public TileBase ground;
    public TileBase wallUp;
    public TileBase wallDown;
    public TileBase wallLeft;
    public TileBase wallRight;
    public TileBase monsterPlace;
    public TileBase monsterDot;
    public TileBase chestPlace;
    public TileBase lockedChestPlace;
    public TileBase leverPlace;
    public TileBase bonfirePlace;
    public TileBase exitPlace;
    public TileBase doorPlace;
    public TileBase lockedDoorPlace;
    public TileBase magicTrapPlace;
    public TileBase fallingGroundPlace;
    public TileBase emptyTile;
    public TileBase[] wallTiles;
}
public enum PlacingThings
{
    MonsterPlace,
    MonsterDot,
    ChestPlace,
    LockedChestPlace,
    LeverPlace,
    BonfirePlace,
    ExitPlace,
    DoorPlace,
    LockedDoorPlace,
    MagicTrapPlace,
    FallingGroundPlace
}