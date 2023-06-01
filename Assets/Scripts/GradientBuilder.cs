using System.Collections.Generic;
using UnityEngine;

public class GradientBuilder
{
    private List<GradientColorKey> colorKeys = new List<GradientColorKey>();
    private List<GradientAlphaKey> alphaKeys = new List<GradientAlphaKey>();

    public GradientBuilder()
    {

    }

    public GradientBuilder AddColorTimePair(Color color, float time)
    {
        colorKeys.Add(new GradientColorKey(color, time));
        alphaKeys.Add(new GradientAlphaKey(1.0f, time));
        return this;
    }


    public GradientBuilder Clear()
    {
        colorKeys.Clear();
        alphaKeys.Clear();
        return this;
    }

    public Gradient Build()
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(colorKeys.ToArray(), alphaKeys.ToArray());
        return gradient;
    }
}