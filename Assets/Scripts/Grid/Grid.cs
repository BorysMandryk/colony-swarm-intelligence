using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grid<TGridObject> where TGridObject : IGridObject
{
    public event EventHandler<GridValueChangedEventArgs> OnGridValueChanged;
    public class GridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    protected int width;
    protected int height;
    protected float cellSize;
    private Vector3 originPosition;

    protected TGridObject[,] gridArray;

    public int Width => width;
    public int Height => height;
    public float CellSize => cellSize;
    public Vector3 OriginPosition => originPosition;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TGridObject gridObject = createGridObject(x, y);
                int xt = x;   
                int yt = y;   

                gridObject.OnGridObjectValueChanged += () =>
                {
                    OnGridValueChanged?.Invoke(this, new GridValueChangedEventArgs() { x = xt, y = yt });
                };
                gridArray[x, y] = gridObject;
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public (int x, int y) GetXY(Vector3 worldPosition)
    {
        int x, y;
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        return (x, y);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        (x, y) = GetXY(worldPosition);
        return GetGridObject(x, y);
    }
}
