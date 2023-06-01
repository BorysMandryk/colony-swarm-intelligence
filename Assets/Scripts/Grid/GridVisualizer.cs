using System;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

public abstract class GridVisualizer<TGridObject> 
    : MonoBehaviour
    where TGridObject : IGridObject, IVisualizable
{
    [System.Serializable]
    public class ColorTimePair
    {
        public Color color;
        public float time;
    }

    protected Grid<TGridObject> grid;
    protected Mesh mesh;
    protected MeshRenderer meshRenderer;

    [SerializeField] protected Texture2D texture;

    private TextMeshPro[,] textArray;

    public void SetGrid(Grid<TGridObject> grid)
    {
        this.grid = grid;
        UpdateGridVisual();
        CreateGridText();
        grid.OnGridValueChanged += Grid_OnGridValueChanged;
    }

    private void Awake()
    {
        Debug.Log("GridVisualizer Awake()");
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        meshRenderer = GetComponent<MeshRenderer>();

        SetTexture(texture);
    }

    private void Grid_OnGridValueChanged(object sender, Grid<TGridObject>.GridValueChangedEventArgs e)
    {
        UpdateCellVisual(e.x, e.y);
        UpdateCellText(e.x, e.y);
    }

    private void CreateGridText()
    {
        textArray = new TextMeshPro[grid.Width, grid.Height];

        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                textArray[x, y] = CreateWorldText(grid.GetGridObject(x, y).ToString(), 
                    grid.GetWorldPosition(x, y) + new Vector3(grid.CellSize, grid.CellSize) * .5f);
            }
        }
    }

    private void UpdateCellText(int x, int y)
    {
        textArray[x, y].text = grid.GetGridObject(x, y).ToString();
    }

    private TextMeshPro CreateWorldText(string text, Vector3 position)
    {
        GameObject go = new GameObject("World_Text", typeof(TextMeshPro));
        go.transform.SetParent(this.transform);
        go.transform.position = position;
        TextMeshPro textMesh = go.GetComponent<TextMeshPro>();
        textMesh.alignment = TextAlignmentOptions.CenterGeoAligned;
        textMesh.text = text;
        textMesh.fontSize = 20;
        return textMesh;
    }

    private void DestroyWorldTextObjects()
    {
        for (int x = 0; x < textArray.GetLength(0); x++)
        {
            for (int y = 0; y < textArray.GetLength(1); y++)
            {
                Destroy(textArray[x, y]);
            }
        }
    }
    public void ResetVisual()
    {
        mesh.Clear();
        DestroyWorldTextObjects();
    }

    protected void UpdateCellVisual(int x, int y)
    {
        Vector2[] uv = mesh.uv;
        int index = x * grid.Height + y;

        int vIndex = index * 4;

        SetCellUV(x, y, uv, vIndex);

        mesh.uv = uv;
    }

    protected virtual void SetCellUV(int x, int y, Vector2[] uv, int vIndex)
    {
        TGridObject gridValue = grid.GetGridObject(x, y);
        float gridValueNormalized = gridValue.GetValueNormalized();

        Vector2 gridValueUV = new Vector2(gridValueNormalized, 0);
        uv[vIndex] = new Vector2(gridValueUV.x, gridValueUV.y);
        uv[vIndex + 1] = new Vector2(gridValueUV.x, gridValueUV.y);
        uv[vIndex + 2] = new Vector2(gridValueUV.x, gridValueUV.y);
        uv[vIndex + 3] = new Vector2(gridValueUV.x, gridValueUV.y);
    }

    protected void UpdateGridVisual()
    {
        mesh.Clear();

        int gridSize = grid.Width * grid.Height;
        Vector3[] verticies = new Vector3[4 * gridSize];
        Vector2[] uv = new Vector2[4 * gridSize];
        int[] triangles = new int[6 * gridSize];

        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                int index = x * grid.Height + y;

                Vector3 quadSize = new Vector3(1, 1) * grid.CellSize;
                Vector3 position = grid.GetWorldPosition(x, y);

                int vIndex = index * 4;
                verticies[vIndex] = new Vector3(position.x, position.y);
                verticies[vIndex + 1] = new Vector3(position.x, position.y + quadSize.y);
                verticies[vIndex + 2] = new Vector3(position.x + quadSize.x, position.y + quadSize.y);
                verticies[vIndex + 3] = new Vector3(position.x + quadSize.x, position.y);

                SetCellUV(x, y, uv, vIndex);

                int tIndex = index * 6;
                triangles[tIndex] = vIndex;
                triangles[tIndex + 1] = vIndex + 1;
                triangles[tIndex + 2] = vIndex + 2;

                triangles[tIndex + 3] = vIndex;
                triangles[tIndex + 4] = vIndex + 2;
                triangles[tIndex + 5] = vIndex + 3;
            }
        }

        mesh.vertices = verticies;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    protected abstract void SetTexture(Texture2D texture);
}