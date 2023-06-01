using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrushControlPanel : MonoBehaviour
{
    [Header("Size")]
    [SerializeField] private Slider radiusSlider;
    [SerializeField] private TMP_Text radiusText;

    [Header("Value")]
    [SerializeField] private Slider valueSlider;
    [SerializeField] private TMP_Text valueText;

    private Grid<HeatMapGridObject> grid; 

    public int Radius => (int)radiusSlider.value;
    public int Value => (int)valueSlider.value;

    private void Update()
    {
        radiusText.text = $"Brush radius: {Radius}";
        valueText.text = $"Brush value: {Value}";
    }
}
