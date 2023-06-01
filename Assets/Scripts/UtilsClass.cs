using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class UtilsClass
{
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        return worldPosition;
    }

    // ����� � ������� ����� �������� �������
    public static Texture2D Create2ColorTexture(Color color1, Color color2)
    {
        int width = 2;
        int height = 1;
        Texture2D texture = new Texture2D(width, height);

        texture.SetPixel(0, 0, color1);
        texture.SetPixel(1, 0, color2);

        texture.Apply();
        return texture;
    }

    public static Texture2D CreateGradientTexture(int width, int height, Gradient gradient, Color zeroValueColor)
    {
        Texture2D texture = new Texture2D(width, height);

        texture.SetPixel(0, 0, zeroValueColor);
        for (int x = 1; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float t = (float)x / width;
                Color color = gradient.Evaluate(t);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    public static Texture2D CreateGradientTexture(int width, int height, Gradient gradient)
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float t = (float)x / width;
                Color color = gradient.Evaluate(t);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    //public static Texture2D CreateGradientTexture(int width, int height, Color minValueColor, Color maxValueColor)
    //{
    //    Gradient gradient = CreateGradient(minValueColor, maxValueColor);
    //    Texture2D texture = new Texture2D(width, height);

    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            float t = (float)x / width;
    //            Color color = gradient.Evaluate(t);
    //            texture.SetPixel(x, y, color);
    //        }
    //    }
    //    texture.Apply();
    //    return texture;
    //}

    //public static Texture2D CreateGradientTexture(int width, int height, Color minValueColor, Color maxValueColor, Color zeroValueColor)
    //{
    //    // ��� ����� ���������� �������� �������� �������,
    //    // ��� ��� ����� ����� ��������� �� ��������, �� null
    //    // � � ����� ������� ��������, �� �������� �� null ����� ??
    //    Gradient gradient = CreateGradient(minValueColor, maxValueColor);
    //    Texture2D texture = new Texture2D(width, height);

    //    // ̳������� �������� ���� �� ����� ��, ��� ���������� � �������
    //    texture.SetPixel(0, 0, zeroValueColor);
    //    for (int x = 1; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            float t = (float)x / width;
    //            Color color = gradient.Evaluate(t);
    //            texture.SetPixel(x, y, color);
    //        }
    //    }
    //    texture.Apply();
    //    return texture;
    //}

    public static void SaveTextureToPNG(string filename, Texture2D texture)
    {
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/" + filename, bytes);
    }
}
