using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridController : MonoBehaviour
{
    [SerializeField] private HeatMapGenericVisual heatMapVisual;
    [SerializeField] private BrushControlPanel brushPanel;
    [SerializeField] private float timeToNextClick = 0.5f;

    public Grid<HeatMapGridObject> heatMapGrid;

    private float timer;

    void Start()
    {
        ColonyManager.Instance.OnGridCreated += () =>
        {
            heatMapGrid = ColonyManager.Instance.Grid;
            heatMapVisual.SetGrid(heatMapGrid);
        };

        timer = timeToNextClick;
    }

    void Update()
    {
        if (heatMapGrid == null)
        {
            return;
        }

        Vector3 worldPosition = UtilsClass.GetMouseWorldPosition();

        if (timer <= 0 && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0))
            {
                AddValueCircle(worldPosition, brushPanel.Value, brushPanel.Radius);
            }
            else if (Input.GetMouseButton(1))
            {
                AddValueCircle(worldPosition, -brushPanel.Value, brushPanel.Radius);
            }
            timer = timeToNextClick;
        }
        else
        {
            timer -= Time.unscaledDeltaTime;
        }
    }

    public void AddValueCircle(Vector3 worldPosition, int value, int radius)
    {
        float cellValue = (float)value / radius;
        (int cx, int cy) = heatMapGrid.GetXY(worldPosition);

        int left = cx - radius;
        int right = cx + radius;
        int bottom = cy - radius;
        int top = cy + radius;

        for (int x = left; x <= right; x++)
        {
            for (int y = bottom; y <= top; y++)
            {
                int dx = cx - x;
                int dy = cy - y;
                int distance = dx * dx + dy * dy;
                if (distance < radius * radius)
                {
                    int currentCellValue = Mathf.RoundToInt(value - Mathf.Sqrt(distance) * cellValue);
                    heatMapGrid.GetGridObject(x, y)?.AddValue(currentCellValue);
                }
            }
        }
    }
}
