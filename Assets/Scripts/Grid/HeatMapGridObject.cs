using System;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapGridObject : IGridObject, IVisualizable
{
    private const int MIN = 0;
    private const int MAX = 100;

    private int x;
    private int y;
    private int value;

    public float Value => value;

    public event Action OnGridObjectValueChanged;

    public HeatMapGridObject(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        value = Mathf.Clamp(value + addValue, MIN, MAX);
        OnGridObjectValueChanged?.Invoke();
    }

    public float GetValueNormalized()
    {
        return (float)value / (MIN + MAX);
    }

    public override string ToString()
    {
        return value.ToString();
    }
}