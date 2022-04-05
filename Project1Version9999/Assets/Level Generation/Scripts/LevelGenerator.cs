using System;
using System.Collections;
using System.Collections.Generic;
using Game.Utils;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Room[] rooms;
    [SerializeField] private Hallway[] hallways;
    [SerializeField] private int roomsCount;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap shadowsTileMap;
    [SerializeField] private TilemapShadowCaster2D tilemapShadowCaster2D;
    private void Start()
    {
        ResetLevelSettings();
        RecursionGenerate(true);
        GenerateSpecialObjects();
        GenerateEmptyWalls();
        GenerateShadows();
    }

    private Vector2Int currentPos = new Vector2Int(0, 0);

    private ConnectionPoint currentPoint = new ConnectionPoint(new Vector2Int(0,0), new Vector2Int(0,0), ObjectRotation.Right);

    
    [Button("Reset")]
    private void ResetLevelSettings()
    {
        currentPos = new Vector2Int(0, 0);
        currentPoint = new ConnectionPoint(new Vector2Int(0,0), new Vector2Int(0,0), ObjectRotation.Right);
        roomsCurrentAmount = 0;
        currentFieldPos = new Vector2Int(50, 50);
        GenerateEmptyField();
        tilemap.ClearAllTiles();
        shadowsTileMap.ClearAllTiles();
        chestsPositions = new List<Vector2Int>();
        //lockedChestsPositions = new List<Vector2Int>();
        exitPositions = new List<Vector2Int>();
        bonfiresPositions = new List<Vector2Int>();
        leversPositions = new List<Vector2Int>();
        lockedChestsPositions = new List<Vector2Int>();
        wallsPositions = new List<Vector2Int>();
        
        foreach (Transform child in lvlContentParent) {
            DestroyImmediate(child.gameObject);
        }
        
        foreach (Transform child in shadowsTileMap.GetComponent<Transform>()) {
            DestroyImmediate(child.gameObject);
        }
        
        Debug.Log("--Reset--");
    }
    
    private int roomsCurrentAmount = 0;

    [Button("StartRecursionGenerate")]
    private void StartRecursionGenerate()
    {
        RecursionGenerate(true);
        GenerateEmptyWalls();
        GenerateSpecialObjects();
        //GenerateShadows();
        tilemapShadowCaster2D.GenerateShadowCastersRunTime();
        Debug.Log("--Generated--");
    }
    
    private void RecursionGenerate(bool _isMain)
    {
        if(roomsCurrentAmount >= roomsCount)
            return;
        
        //select random suitable room
        Room currentRoom = null;
        bool isOk = false;
        int startPointIndex = 0;
        while (!isOk)
        {
            currentRoom = rooms[UnityEngine.Random.Range(0, rooms.Length)];
            for (var index = 0; index < currentRoom.connectionPoints.Count; index++)
            {
                if (currentRoom.connectionPoints[index].ExitRotation == currentPoint.ExitRotation)
                {
                    startPointIndex = index;
                    isOk = true;
                    break;
                }
            }
        }
        //generate room
        GenerateRoom(currentRoom, startPointIndex);
        roomsCurrentAmount++;
        field[currentFieldPos.x, currentFieldPos.y] = 1;
        //Debug.Log(currentFieldPos);
        
        
        //getting the amount of hallways
        //choose next generating point
        List<ConnectionPoint> nextPoints = new List<ConnectionPoint>();
        for (int j = 0; j < currentRoom.connectionPoints.Count; j++)
        {
            ConnectionPoint p = currentRoom.connectionPoints[j];
            if (j == startPointIndex)
                continue;
            if (field[currentFieldPos.x +1, currentFieldPos.y ] == 1 && p.ExitRotation == ObjectRotation.Right)
                continue;
            if (field[currentFieldPos.x -1, currentFieldPos.y ] == 1 && p.ExitRotation == ObjectRotation.Left)
                continue;
            if (field[currentFieldPos.x, currentFieldPos.y  + 1] == 1 && p.ExitRotation == ObjectRotation.Up)
                continue;
            if (field[currentFieldPos.x, currentFieldPos.y - 1] == 1 && p.ExitRotation == ObjectRotation.Down)
                continue;
            nextPoints.Add(currentRoom.connectionPoints[j]);
        }

        //var emptyPoints = new List<ConnectionPoint>(nextPoints);
        
        if (roomsCurrentAmount * 1.2f < roomsCount && nextPoints.Count >= 2 && _isMain)
        {
            switch (UnityEngine.Random.Range(0,3))
            {
                case 0:
                    ConnectionPoint point = nextPoints[UnityEngine.Random.Range(0, nextPoints.Count)];
                    nextPoints.Remove(point);
                    switch (point.ExitRotation)
                    {
                        case ObjectRotation.Down:
                            currentFieldPos += Vector2Int.down;
                            break;
                        case ObjectRotation.Up:
                            currentFieldPos += Vector2Int.up;
                            break;
                        case ObjectRotation.Left:
                            currentFieldPos += Vector2Int.left;
                            break;
                        case ObjectRotation.Right:
                            currentFieldPos += Vector2Int.right;
                            break;
                    }
                    HallwayGenerationPreparations(point);
                    wallsPositions.Remove(point.LocalPosition + currentPos);
                    RecursionGenerate(true);
                    break;
                
                default:
                    //choose next generating point
                    int index1 = UnityEngine.Random.Range(0, nextPoints.Count);
                    ConnectionPoint point1 = nextPoints[index1];
                    nextPoints.Remove(point1);
                    wallsPositions.Remove(point1.LocalPosition + currentPos);
                    int index2 = UnityEngine.Random.Range(0, nextPoints.Count);;
                    ConnectionPoint point2 = nextPoints[index2];
                    nextPoints.Remove(point2);
                    
                    
                    var currentPosCopy = currentPos;
                    var currentFieldPosCopy = currentFieldPos;
                    
                    switch (point1.ExitRotation)
                    {
                        case ObjectRotation.Down:
                            currentFieldPos += Vector2Int.down;
                            break;
                        case ObjectRotation.Up:
                            currentFieldPos += Vector2Int.up;
                            break;
                        case ObjectRotation.Left:
                            currentFieldPos += Vector2Int.left;
                            break;
                        case ObjectRotation.Right:
                            currentFieldPos += Vector2Int.right;
                            break;
                    }
                    HallwayGenerationPreparations(point1);
                    RecursionGenerate(false);
                    if (roomsCurrentAmount >= roomsCount)
                        break;
                    
                    currentPos = currentPosCopy;
                    currentFieldPos = currentFieldPosCopy;
                    wallsPositions.Remove(point2.LocalPosition + currentPos);
                    switch (point2.ExitRotation)
                    {
                        case ObjectRotation.Down:
                            currentFieldPos += Vector2Int.down;
                            break;
                        case ObjectRotation.Up:
                            currentFieldPos += Vector2Int.up;
                            break;
                        case ObjectRotation.Left:
                            currentFieldPos += Vector2Int.left;
                            break;
                        case ObjectRotation.Right:
                            currentFieldPos += Vector2Int.right;
                            break;
                    }
                    HallwayGenerationPreparations(point2);
                    RecursionGenerate(true);
                    break;
            }
        }
        /*else if (!_isMain && roomsCurrentAmount < roomsCount && nextPoints.Count > 0)
        {
            ConnectionPoint point = nextPoints[UnityEngine.Random.Range(0, nextPoints.Count)];
            switch (point.ExitRotation)
            {
                case ObjectRotation.Down:
                    currentFieldPos += Vector2Int.down;
                    break;
                case ObjectRotation.Up:
                    currentFieldPos += Vector2Int.up;
                    break;
                case ObjectRotation.Left:
                    currentFieldPos += Vector2Int.left;
                    break;
                case ObjectRotation.Right:
                    currentFieldPos += Vector2Int.right;
                    break;
            }
            HallwayGenerationPreparations(point);
        }*/
        else if(roomsCurrentAmount < roomsCount && nextPoints.Count > 0 && _isMain)
        {
 
            //choose next generating point
            ConnectionPoint point = nextPoints[UnityEngine.Random.Range(0, nextPoints.Count)];
            nextPoints.Remove(point);
            switch (point.ExitRotation)
            {
                case ObjectRotation.Down:
                    currentFieldPos += Vector2Int.down;
                    break;
                case ObjectRotation.Up:
                    currentFieldPos += Vector2Int.up;
                    break;
                case ObjectRotation.Left:
                    currentFieldPos += Vector2Int.left;
                    break;
                case ObjectRotation.Right:
                    currentFieldPos += Vector2Int.right;
                    break;
            }
            wallsPositions.Remove(point.LocalPosition + currentPos);
            HallwayGenerationPreparations(point);
            RecursionGenerate(true);
        }
        //Debug.Log(roomsCurrentAmount);

        
    }

    private void HallwayGenerationPreparations(ConnectionPoint _point)
    {
        currentPos += new Vector2Int(_point.LocalPosition.x, _point.LocalPosition.y);
        //Debug.Log(currentPos);
        //choose random suitable hallway
        Hallway currentHallway = null;
        bool isOk = false;
        int startPointIndex = 0;

        while (!isOk)
        {
            currentHallway = hallways[UnityEngine.Random.Range(0, hallways.Length)];
            for (var index = 0; index < currentHallway.connectionPoints.Count; index++)
            {
                if (currentHallway.connectionPoints[index].ExitRotation == _point.ExitRotation)
                {
                    startPointIndex = index;
                    isOk = true;
                    break;
                }
            }
        }
        //generate hallway
        GenerateHallway(currentHallway, startPointIndex);
        currentPos += currentPoint.LocalPosition;
        //Debug.Log(currentPos);
    }
    

    private void GenerateRoom(Room _room, int _startPointIndex)
    {
        ConnectionPoint point = _room.connectionPoints[_startPointIndex];
        var tileArray = _room.tilemapPrefab.GetTilesBlock(_room.roomSize);
        var area = new BoundsInt(
            currentPos.x - point.LocalPosition.x,
            currentPos.y - point.LocalPosition.y,
            0 + _room.roomSize.zMin, 
            _room.roomSize.size.x,
            _room.roomSize.size.y, 
            _room.roomSize.size.z);
        
        tilemap.SetTilesBlock(area, tileArray);
        currentPos -= point.LocalPosition;
    
        foreach (var conPoint in _room.connectionPoints)
        {
            var tilePos = new Vector3Int((conPoint.LocalPosition + currentPos).x,
                (conPoint.LocalPosition + currentPos).y, 0);
            TileBase placingTile = null;
            switch (conPoint.ExitRotation)
            {
                case ObjectRotation.Right:
                    placingTile = tilesDataBase.wallRight;
                    break;
                case ObjectRotation.Left:
                    placingTile = tilesDataBase.wallLeft;
                    break;
                case ObjectRotation.Up:
                    placingTile = tilesDataBase.wallUp;
                    break;
                case ObjectRotation.Down:
                    placingTile = tilesDataBase.wallDown;
                    break;
            }
            
            tilemap.SetTile(tilePos, placingTile);
            wallsPositions.Add(conPoint.LocalPosition + currentPos);
        }

        if (roomsCurrentAmount >= 1) //чтобы в первой комнате начальный тайл закрасился стеной 
        {
            var startTilePos = new Vector3Int((point.LocalPosition + currentPos).x, (point.LocalPosition + currentPos).y, 0);
            tilemap.SetTile(startTilePos,tilesDataBase.ground); //закрашиваем начальный тайл комнаты полом
            wallsPositions.Remove(point.LocalPosition + currentPos);
            Debug.Log(point.LocalPosition + currentPos);
        }
    
        //считываем все специальные тайлы и заменяем их на тайлы пола
        foreach (var placingPoint in _room.placingPoints)
        {
            switch (placingPoint.PlacingThing)
            {
                case PlacingThings.ChestPlace:
                    chestsPositions.Add(placingPoint.LocalPosition+currentPos);
                    break;
                case PlacingThings.MonsterPlace:
                    break;
                case PlacingThings.MonsterDot:
                    break;
                case PlacingThings.LockedChestPlace:
                    lockedChestsPositions.Add(placingPoint.LocalPosition+currentPos);
                    break;
                case PlacingThings.LeverPlace:
                    leversPositions.Add(placingPoint.LocalPosition+currentPos);
                    break;
                case PlacingThings.BonfirePlace:
                    bonfiresPositions.Add(placingPoint.LocalPosition+currentPos);
                    break;
                case PlacingThings.ExitPlace:
                    exitPositions.Add(placingPoint.LocalPosition+currentPos);
                    break;
                case PlacingThings.DoorPlace:
                    break;
                case PlacingThings.LockedDoorPlace:
                    break;
                case PlacingThings.MagicTrapPlace:
                    break;
                case PlacingThings.FallingGroundPlace:
                    break;
                default:
                    continue;
            }

            var tilePos = new Vector3Int((placingPoint.LocalPosition + currentPos).x, (placingPoint.LocalPosition + currentPos).y, 0);
            tilemap.SetTile(tilePos,tilesDataBase.ground);
        }

        foreach (var wallPoint in _room.wallPoints)
        {
            wallsPositions.Add(wallPoint + currentPos);
        }
        

    }

    [SerializeField] private int chestsAmount, bonfiresAmount, lockedChestsAmount;
    [SerializeField] private Transform lvlContentParent;
    [SerializeField] private GameObject chestPrefab, lockedChestPrefab, exitPrefab, leverPrefab;
    
    private List<Vector2Int> chestsPositions = new List<Vector2Int>();
    private List<Vector2Int> lockedChestsPositions = new List<Vector2Int>();
    private List<Vector2Int> leversPositions = new List<Vector2Int>();
    private List<Vector2Int> exitPositions = new List<Vector2Int>();
    private List<Vector2Int> bonfiresPositions = new List<Vector2Int>();

    public List<Vector2Int> wallsPositions = new List<Vector2Int>();

    [SerializeField] private TilesDataBase tilesDataBase;


    [SerializeField] private Vector3 exitTileOffset = new Vector3(0f, 0f, 0f);
    private void GenerateSpecialObjects()
    {
        for (var i = 0; i < chestsAmount; i++)
        {
            var posVector2 = chestsPositions[UnityEngine.Random.Range(0, chestsPositions.Count)];
            var position = tilemap.GetCellCenterWorld(new Vector3Int(posVector2.x, posVector2.y, 0));
            chestsPositions.Remove(posVector2);
            Instantiate(chestPrefab, position, Quaternion.Euler(0,0,0), lvlContentParent);
        }
        for (var i = 0; i < lockedChestsAmount; i++)
        {
            var posVector2 = lockedChestsPositions[UnityEngine.Random.Range(0, lockedChestsPositions.Count)];
            var position = tilemap.GetCellCenterWorld(new Vector3Int(posVector2.x, posVector2.y, 0));
            lockedChestsPositions.Remove(posVector2);
            var lockedChestGameObject = Instantiate(lockedChestPrefab, position, Quaternion.Euler(0,0,0), lvlContentParent);
            var lockedChestComponent = lockedChestGameObject.GetComponent<Chest>();
            
            posVector2 = leversPositions[UnityEngine.Random.Range(0, leversPositions.Count)];
            position = tilemap.GetCellCenterWorld(new Vector3Int(posVector2.x, posVector2.y, 0));
            leversPositions.Remove(posVector2);
            var leverGameObject = Instantiate(leverPrefab, position, Quaternion.Euler(0,0,0), lvlContentParent);
            var leverComponent = leverGameObject.GetComponent<Lever>();
            leverComponent.SetActiveObject(lockedChestComponent);

        }
        var exitPosVector2 = exitPositions[UnityEngine.Random.Range(0, chestsPositions.Count)];
        var exitPosition = tilemap.GetCellCenterWorld(new Vector3Int(exitPosVector2.x, exitPosVector2.y, 0));
        exitPositions.Remove(exitPosVector2);
        
        Instantiate(exitPrefab, exitPosition+exitTileOffset, Quaternion.Euler(0,0,0), lvlContentParent);
    }

    private void GenerateHallway(Hallway _hallway, int _startPointIndex)
    {
        ConnectionPoint point = _hallway.connectionPoints[_startPointIndex];
        var tileArray = _hallway.tilemapPrefab.GetTilesBlock(_hallway.hallwaySize);
        var area = new BoundsInt(
            currentPos.x - point.LocalPosition.x,
            currentPos.y - point.LocalPosition.y,
            0 + _hallway.hallwaySize.zMin, 
            _hallway.hallwaySize.size.x,
            _hallway.hallwaySize.size.y, 
            _hallway.hallwaySize.size.z);
        
        tilemap.SetTilesBlock(area, tileArray);
        
        switch (_startPointIndex)
        {
            case 1:
                currentPoint = _hallway.connectionPoints[0];
                
                break;
            case 0:
                currentPoint = _hallway.connectionPoints[1];
                break;
        }
        currentPos -= point.LocalPosition;
        var tilePos = new Vector3Int((_hallway.connectionPoints[_startPointIndex].LocalPosition + currentPos).x, (_hallway.connectionPoints[_startPointIndex].LocalPosition + currentPos).y, 0);
        tilemap.SetTile(tilePos,tilesDataBase.ground);
        
        foreach (var wallPoint in _hallway.wallPoints)
        {
            wallsPositions.Add(wallPoint + currentPos);
        }
    }

    private void GenerateEmptyWalls()
    {
        foreach (var wallPos in wallsPositions)
        {
            shadowsTileMap.SetTile(new Vector3Int(wallPos.x, wallPos.y, 0), tilesDataBase.emptyTile);
        }
    }

    /*IEnumerator GenerateShadowsIEnumenator()
    {
        yield return new WaitForSeconds(0.5f);
        shadowCaster2DTileMap.Generate();
    }*/
    [Button("GenerateShadows")]
    private void GenerateShadows()
    {
        
        //StartCoroutine(GenerateShadowsIEnumenator());
        tilemapShadowCaster2D.GenerateShadowCastersRunTime();
    }
    

    private int[,] field = new int[99, 99];
    private void GenerateEmptyField()
    {
        for (int i = 0; i < 99; i++)
            for (int j = 0; j < 99; j++)
                field[i, j] = 0;
        
    }
    private Vector2Int currentFieldPos = new Vector2Int(50, 50);

}
