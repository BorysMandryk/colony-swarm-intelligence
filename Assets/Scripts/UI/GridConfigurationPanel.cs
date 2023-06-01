using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridConfigurationPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField widthInputField;
    [SerializeField] private TMP_InputField heightInputField;
    [SerializeField] private TMP_InputField cellSizeInputField;
    [SerializeField] private TMP_InputField xOriginInputField;
    [SerializeField] private TMP_InputField yOriginInputField;
    [SerializeField] private Button submitButton;

    private void Start()
    {
        widthInputField.onEndEdit.AddListener(
            (str) => widthInputField.text = ValidatePositiveInput(widthInputField.text));
        heightInputField.onEndEdit.AddListener(
            (str) => heightInputField.text = ValidatePositiveInput(heightInputField.text));
        cellSizeInputField.onEndEdit.AddListener(
            (str) => cellSizeInputField.text = ValidatePositiveInput(cellSizeInputField.text));
        xOriginInputField.onEndEdit.AddListener(
            (str) => xOriginInputField.text = ValidatePositiveInput(xOriginInputField.text));
        yOriginInputField.onEndEdit.AddListener(
            (str) => yOriginInputField.text = ValidatePositiveInput(yOriginInputField.text));
        submitButton.onClick.AddListener(SubmitAbcValues);
    }

    private void Update()
    {
        if (ColonyManager.Instance.Grid == null)
        {
            submitButton.interactable = true;
        }
        else
        {
            submitButton.interactable = false;
        }
    }

    private string ValidatePositiveInput(string inputText)
    {
        float value = float.Parse(inputText);
        value = Mathf.Max(0, value);
        return value.ToString();
    }

    private void SubmitAbcValues()
    {
        int width = int.Parse(widthInputField.text);
        int height = int.Parse(heightInputField.text);
        float cellSize = float.Parse(cellSizeInputField.text);
        float xOrigin = float.Parse(xOriginInputField.text);
        float yOrigin = float.Parse(yOriginInputField.text);
        ColonyManager.Instance.CreateGrid(width, height, cellSize, new Vector2(xOrigin, yOrigin));
    }
}
