using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NaughtyAttributes;
using System.Linq;

[CreateAssetMenu(fileName = "HallwayPrefab", menuName = "LevelGenerator/HallwayPrefab")]
public class Hallway : ScriptableObject
{
    public Tilemap tilemapPrefab;
    public BoundsInt hallwaySize;
    public TilesDataBase tilesDataBase;
    /*public ObjectRotation rotation;
    public ConnectionPoint startPoint;
    public ConnectionPoint endPoint;*/
    public List<ConnectionPoint> connectionPoints;
    public List<Vector2Int> wallPoints;

    [Button("Bake")]
    public void Bake()
    {
        connectionPoints.Clear();
        var tileArray = tilemapPrefab.GetTilesBlock(hallwaySize);
        for (var index = 0; index < tileArray.Length; index++)
        {
            var tile = tileArray[index];
            int x, y;
            // позиции относительно левого нижнего угла комнаты
            x = index % hallwaySize.size.x; 
            y = index / hallwaySize.size.x;
            //Debug.Log(tile + "; x = " + x + "; y = " + y);
            //позиции относительно центра tilemap'ы
            int x1 = x + hallwaySize.xMin;
            int y1 = y + hallwaySize.yMin;
            
            //Debug.Log( "; x1 = " + x + "; y1 = " + y);
            if (tile == tilesDataBase.connectionDown)
                connectionPoints.Add(new ConnectionPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), ObjectRotation.Down));
            else if (tile == tilesDataBase.connectionUp)
                connectionPoints.Add(new ConnectionPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), ObjectRotation.Up));
            else if (tile == tilesDataBase.connectionLeft)
                connectionPoints.Add(new ConnectionPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), ObjectRotation.Left));
            else if (tile == tilesDataBase.connectionRight)
                connectionPoints.Add(new ConnectionPoint(new Vector2Int(x1, y1),new Vector2Int(x, y), ObjectRotation.Right));
            
            else if (tilesDataBase.wallTiles.Any(_wallTile => tile == _wallTile))
                wallPoints.Add(new Vector2Int(x, y));
            
            else
                continue;
            
            Debug.Log(tile);
        }
    }
}
