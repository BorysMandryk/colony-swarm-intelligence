using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HeatMapGenericVisual : GridVisualizer<HeatMapGridObject>
{
    [Header("Gradient")]
    [SerializeField] private ColorTimePair[] colorTimePairs;

    protected override void SetTexture(Texture2D texture)
    {
        this.texture = texture;
        if (texture == null)
        {
            GradientBuilder gb = new GradientBuilder();
            foreach (ColorTimePair colorTime in colorTimePairs)
            {
                gb.AddColorTimePair(colorTime.color, colorTime.time);
            }
            Gradient gradient = gb.Build();

            texture = UtilsClass.CreateGradientTexture(100, 1, gradient);
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;
        }
        meshRenderer.material.mainTexture = texture;
    }
}
