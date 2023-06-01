using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbcConfigurationPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField employedInputField;
    [SerializeField] private TMP_InputField onlookerInputField;
    [SerializeField] private TMP_InputField maxTrialsInputField;
    [SerializeField] private TMP_Dropdown optimizationProblemDropdown;
    [SerializeField] private Button submitButton;


    private void Start()
    {
        employedInputField.onEndEdit.AddListener(
            (str) => employedInputField.text = ValidatePositiveInput(employedInputField.text));
        onlookerInputField.onEndEdit.AddListener(
            (str) => onlookerInputField.text = ValidatePositiveInput(onlookerInputField.text));
        maxTrialsInputField.onEndEdit.AddListener(
            (str) => maxTrialsInputField.text = ValidatePositiveInput(maxTrialsInputField.text));
        submitButton.onClick.AddListener(SubmitAbcValues);
    }

    private void Update()
    {
        if (ColonyManager.Instance.Grid == null || ColonyManager.Instance.Abc != null)
        {
            submitButton.interactable = false;
        }
        else
        {
            submitButton.interactable = true;
        }
    }

    private string ValidatePositiveInput(string inputText)
    {
        int value = int.Parse(inputText);
        value = Mathf.Max(0, value);
        return value.ToString();
    }

    private void SubmitAbcValues()
    {
        int employedNumber = int.Parse(employedInputField.text);
        int onlookerNumber = int.Parse(onlookerInputField.text);
        int maxTrials = int.Parse(maxTrialsInputField.text);
        int optimizationProblem = optimizationProblemDropdown.value;
        ColonyManager.Instance.InitializeAbc(employedNumber, onlookerNumber, maxTrials, optimizationProblem);
    }
}
