using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float minSize = 5;
    [SerializeField] private float maxSize = 60;
    [SerializeField] private float sensitivity = 30;
    [SerializeField] private float zoomSpeed = 10;

    private Camera camera;

    private float targetZoom;

    private bool hidden = false;
    private Canvas canvas;

    private void Awake()
    {
        camera = Camera.main;
        canvas = FindObjectOfType<Canvas>();
    }

    private void Start()
    {
        targetZoom = camera.orthographicSize;
    }

    void Update()
    {
        Move();
        Zoom();

        if (Input.GetKeyDown(KeyCode.H))
        {
            hidden = !hidden;
            canvas.enabled = !hidden;
        }
    }

    private void Move()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        vertical *= movementSpeed * Time.unscaledDeltaTime;
        horizontal *= movementSpeed * Time.unscaledDeltaTime;
        transform.Translate(horizontal, vertical, 0);
    }

    private void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scroll * sensitivity;
        targetZoom = Mathf.Clamp(targetZoom, minSize, maxSize);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, zoomSpeed * Time.unscaledDeltaTime);
    }
}
