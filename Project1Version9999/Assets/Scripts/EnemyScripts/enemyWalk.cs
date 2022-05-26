using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class enemyWalk : EnemyCanSee
{

    public Tilemap map;

    public Tile ground;

    private Dictionary<Vector3, int> used = new Dictionary<Vector3, int>();
    private Dictionary<Vector3, Vector3> pred = new Dictionary<Vector3, Vector3>();
    private Queue<Vector3> q = new Queue<Vector3>();
    public Vector3 SavePosition(Vector3 current, Vector3 target, float escapeDist)
    {
        Vector3 up = GoToWall(target, escapeDist, Vector3.up);
        Vector3 right = GoToWall(target, escapeDist, Vector3.right);
        Vector3 down = GoToWall(target, escapeDist, -1 * Vector3.up);
        Vector3 left = GoToWall(target, escapeDist, -1 * Vector3.right);
        Vector3[] positions = new Vector3[3];
        Vector3 dir = current - target;
        float angDir = Mathf.Round(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg / 90) * 90;
        if (angDir == 90)
        {
            positions[0] = up;
            positions[1] = right;
            positions[2] = left;
        }
        else if (angDir == 0)
        {
            positions[0] = right;
            positions[1] = up;
            positions[2] = down;
        }
        else if (angDir == -90)
        {
            positions[0] = down;
            positions[1] = left;
            positions[2] = right;
        }
        else
        {
            positions[0] = left;
            positions[1] = down;
            positions[2] = up;
        }
        if (true)
        {
            if (Vector3.Distance(target, positions[0]) >= escapeDist)
                return positions[0];
            else if (Vector3.Distance(target, positions[1]) >= escapeDist)
                return positions[1];
            else if (Vector3.Distance(target, positions[2]) >= escapeDist)
                return positions[2];
            if (Vector3.Distance(target, positions[0]) >= 0.9)
                return positions[0];
        }
        return current;
    }
    private Vector3 GoToWall(Vector3 start, float escapeDist, Vector3 direction)
    {
        Vector3 answer = start;
        for (; ; )
        {
            answer += direction;
            if (Vector3.Distance(start, answer) >= escapeDist || map.GetTile(map.WorldToCell(answer + direction)) != ground)
                break;
        }
        return answer;
    }
    public Vector3 ShortWay(Vector3 start, Vector3 target)
    {
        if (start == target)
            return start;
        used.Clear();
        pred.Clear();
        used.Add(map.WorldToCell(start), 0);
        pred.Add(map.WorldToCell(start), map.WorldToCell(start));
        q.Enqueue(map.WorldToCell(start));
        while (!(q.Count == 0))
        {
            Vector3 u = q.Dequeue();
            int tileSize = (int)map.GetComponentInParent<Grid>().cellSize.x;
            Vector3 dir = player.transform.position - transform.position;
            float angDir = Mathf.Round( Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg/90)*90;
            if (angDir==0)
            {
                Vector3Int pos;
                pos = new Vector3Int((int)(u.x - tileSize), (int)u.y, 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
                pos = new Vector3Int((int)u.x, (int)(u.y + tileSize), 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
                pos = new Vector3Int((int)u.x, (int)(u.y - tileSize), 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
                pos = new Vector3Int((int)(u.x + tileSize), (int)u.y, 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
            }
            else if (angDir==90)
            {
                Vector3Int pos;
                pos = new Vector3Int((int)u.x, (int)(u.y - tileSize), 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
                pos = new Vector3Int((int)(u.x + tileSize), (int)u.y, 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
                pos = new Vector3Int((int)(u.x - tileSize), (int)u.y, 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
                pos = new Vector3Int((int)u.x, (int)(u.y + tileSize), 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
            }
            else if (angDir == -90)
            {
                Vector3Int pos;
                pos = new Vector3Int((int)u.x, (int)(u.y + tileSize), 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
                pos = new Vector3Int((int)(u.x + tileSize), (int)u.y, 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
                pos = new Vector3Int((int)(u.x - tileSize), (int)u.y, 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
                pos = new Vector3Int((int)u.x, (int)(u.y - tileSize), 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
            }
            else
            {
                Vector3Int pos;
                pos = new Vector3Int((int)(u.x + tileSize), (int)u.y, 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
                pos = new Vector3Int((int)u.x, (int)(u.y + tileSize), 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
                pos = new Vector3Int((int)u.x, (int)(u.y - tileSize), 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
                pos = new Vector3Int((int)(u.x - tileSize), (int)u.y, 0);
                if (map.GetTile(pos) == ground && !used.ContainsKey(pos))
                {
                    q.Enqueue(pos);
                    used.Add(pos, used[u] + 1);
                    pred.Add(pos, u);
                }
            }
        }
        if (pred.ContainsKey(map.WorldToCell(target)))
        {
            Vector3 pos = findPred(map.WorldToCell(target));
            return new Vector3(pos.x + map.transform.position.x + 0.5f, pos.y + map.transform.position.y + 0.5f, 0);
        }
        else
            return start;
    }
    public Vector3 InPlayer(Vector3 pos, Vector3 dir)
    {
        Vector3 _dir = new Vector3(Mathf.Round(dir.x), Mathf.Round(dir.y), 0);
        Vector3[] positions = new Vector3[4];
        if (dir == Vector3.up)
        {
            positions[0] = Vector3.up+pos;
            positions[1] = Vector3.right + pos;
            positions[2] = -1 * Vector3.right + pos;
            positions[3] = -1 * Vector3.up + pos;
        }
        else if (dir == -1 * Vector3.up)
        {
            positions[0] = -1 * Vector3.up + pos;
            positions[1] = -1 * Vector3.right + pos;
            positions[2] = Vector3.right + pos;
            positions[3] = Vector3.up + pos;
        }
        else if (dir == -1 * Vector3.right)
        {
            positions[0] = -1 * Vector3.right + pos;
            positions[1] = -1 * Vector3.up + pos;
            positions[2] = Vector3.up + pos;
            positions[3] = Vector3.right + pos;
        }
        else
        {
            positions[0] = Vector3.right + pos;
            positions[1] = Vector3.up + pos;
            positions[2] = -1 * Vector3.up + pos;
            positions[3] = -1 * Vector3.right + pos;
        }
        
        if (map.GetTile(map.WorldToCell(positions[0])) == ground)
            return positions[0];
        if (map.GetTile(map.WorldToCell(positions[1])) == ground)
            return positions[1];
        if (map.GetTile(map.WorldToCell(positions[2])) == ground)
            return positions[2];
        if (map.GetTile(map.WorldToCell(positions[3])) == ground)
            return positions[3];
        return pos;
    }
    private Vector3 findPred(Vector3 u)
    {
        if (pred[pred[u]] == pred[u])
        {
            return u;
        }
        else
        {
            return findPred(pred[u]);
        }
    }
}
