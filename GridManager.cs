using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static int width = 10;
    public static int height = 20;
    public static Transform[,] grid = new Transform[width, height];


    public static Vector2 Round(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }


    public static bool InsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }


    public static bool IsValidPosition(Transform piece)
    {
        foreach (Transform block in piece)
        {
            Vector2 v = Round(block.position);
            if (!InsideBorder(v)) return false;
            if ((int)v.y < height)
            {
                if (grid[(int)v.x, (int)v.y] != null && grid[(int)v.x, (int)v.y].parent != piece)
                    return false;
            }
        }
        return true;
    }
    public static void StorePiece(Transform piece)
{
    foreach (Transform block in piece)
    {
        Vector2 v = Round(block.position);
        if ((int)v.y < height)
            grid[(int)v.x, (int)v.y] = block;
    }
}


    public static void DeleteRow(int y)
{
    for (int x = 0; x < width; x++)
    {
        if (grid[x, y] != null)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
}


public static void DecreaseRow(int y)
{
    for (int x = 0; x < width; x++)
    {
        if (grid[x, y] != null)
        {
            grid[x, y - 1] = grid[x, y];
            grid[x, y] = null;
            grid[x, y - 1].position += new Vector3(0, -1, 0);
        }
    }
}


public static void DecreaseRowsAbove(int y)
{
    for (int i = y; i < height; i++)
        DecreaseRow(i);
}


public static bool IsRowFull(int y)
{
    for (int x = 0; x < width; x++)
    {
        if (grid[x, y] == null) return false;
    }
    return true;
}


public static int DeleteFullRows()
{
    int lines = 0;
    for (int y = 0; y < height; ++y)
    {
        if (IsRowFull(y))
        {
            DeleteRow(y);
            DecreaseRowsAbove(y + 1);
            y--;
            lines++;
        }
    }
    return lines;
}
}