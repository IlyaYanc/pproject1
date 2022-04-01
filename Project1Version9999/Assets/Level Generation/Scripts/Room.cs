using System;using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "RoomPrefab", menuName = "LevelGenerator/RoomPrefab")]
public class Room : ScriptableObject
{
    public Tilemap tilemapPrefab;
    public BoundsInt roomSize;
    public TilesDataBase tilesDataBase;
    public List<ConnectionPoint> connectionPoints;
    public List<PlacingPoint> placingPoints;
    

    [Button("Bake")]
    public void Bake()
    {
        connectionPoints.Clear();
        placingPoints.Clear();
        var tileArray = tilemapPrefab.GetTilesBlock(roomSize);
        for (var index = 0; index < tileArray.Length; index++)
        {
            var tile = tileArray[index];
            int x, y;
            // позиции относительно левого нижнего угла комнаты
            x = index % roomSize.size.x; 
            y = index / roomSize.size.x;
            //позиции относительно центра tilemap'ы
            int x1 = x + roomSize.xMin;
            int y1 = y + roomSize.yMin;
            //switch почему-то отказался работать :((
            //connections
            if (tile == tilesDataBase.connectionDown)
                connectionPoints.Add(new ConnectionPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), ObjectRotation.Down));
            else if (tile == tilesDataBase.connectionUp)
                connectionPoints.Add(new ConnectionPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), ObjectRotation.Up));
            else if (tile == tilesDataBase.connectionLeft)
                connectionPoints.Add(new ConnectionPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), ObjectRotation.Left));
            else if (tile == tilesDataBase.connectionRight)
                connectionPoints.Add(new ConnectionPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), ObjectRotation.Right));
            //other special points
            else if (tile == tilesDataBase.chestPlace)
                placingPoints.Add(new PlacingPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), PlacingThings.ChestPlace));
            else if (tile == tilesDataBase.lockedChestPlace)
                placingPoints.Add(new PlacingPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), PlacingThings.LockedChestPlace));
            else if (tile == tilesDataBase.doorPlace)
                placingPoints.Add(new PlacingPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), PlacingThings.DoorPlace));
            else if (tile == tilesDataBase.lockedDoorPlace)
                placingPoints.Add(new PlacingPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), PlacingThings.LockedDoorPlace));
            else if (tile == tilesDataBase.monsterPlace)
                placingPoints.Add(new PlacingPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), PlacingThings.MonsterPlace));
            else if (tile == tilesDataBase.monsterDot)
                placingPoints.Add(new PlacingPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), PlacingThings.MonsterDot));
            else if (tile == tilesDataBase.exitPlace)
                placingPoints.Add(new PlacingPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), PlacingThings.ExitPlace));
            else if (tile == tilesDataBase.bonfirePlace)
                placingPoints.Add(new PlacingPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), PlacingThings.BonfirePlace));
            else if (tile == tilesDataBase.leverPlace)
                placingPoints.Add(new PlacingPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), PlacingThings.LeverPlace));
            else if (tile == tilesDataBase.magicTrapPlace)
                placingPoints.Add(new PlacingPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), PlacingThings.MagicTrapPlace));
            else if (tile == tilesDataBase.fallingGroundPlace)
                placingPoints.Add(new PlacingPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), PlacingThings.FallingGroundPlace));
            
            else
                continue;
            
            Debug.Log(tile);
        }
    }
    
}

public enum ObjectRotation
{
    Right,
    Left,
    Up,
    Down
}
[Serializable]
public struct ConnectionPoint
{
    public Vector2Int Position;
    public Vector2Int LocalPosition;
    public ObjectRotation ExitRotation;

    public ConnectionPoint(Vector2Int _position, Vector2Int _localPosition, ObjectRotation _exitRotation)
    {
        Position = _position;
        LocalPosition = _localPosition;
        ExitRotation = _exitRotation;
    }
}

[Serializable]
public struct PlacingPoint
{
    public Vector2Int Position;
    public Vector2Int LocalPosition;
    public PlacingThings PlacingThing;
    public PlacingPoint(Vector2Int _position, Vector2Int _localPosition, PlacingThings _placingThing)
    {
        Position = _position;
        LocalPosition = _localPosition;
        PlacingThing = _placingThing;
    }
}


