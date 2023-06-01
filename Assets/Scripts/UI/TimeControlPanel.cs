using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeControlPanel : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button increaseSpeedButton;
    [SerializeField] private Button decreaseSpeedButton;

    [SerializeField] private TMP_Text stateText;

    private float speed;

    public void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        pauseButton.onClick.AddListener(OnPauseButtonClick);
        increaseSpeedButton.onClick.AddListener(OnIncreaseButtonClick);
        decreaseSpeedButton.onClick.AddListener(OnDecreaseButtonClick);
        startButton.IsActive();
        speed = Time.timeScale;
        OnPauseButtonClick();
    }

    private void OnStartButtonClick()
    {
        Time.timeScale = speed;
    }

    private void OnPauseButtonClick()
    {
        Time.timeScale = 0f;
    }

    private void OnIncreaseButtonClick()
    {
        speed += 1f;
        Time.timeScale = speed;
    }

    private void OnDecreaseButtonClick()
    {
        speed -= 1f;
        speed = Mathf.Max(speed, 1f);
        Time.timeScale = speed;
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            stateText.text = $"Speed: {speed}x";
        }
        else
        {
            stateText.text = $"Paused";
        }
    }
}
