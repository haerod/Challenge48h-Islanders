using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public float cameraSpeed = .5f;
    [HideInInspector] public bool canMove = true;

    private Camera cam;
    private Vector3 clickPosition;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (!canMove) return;

        if (Input.GetMouseButtonDown(1))
            clickPosition = Input.mousePosition;

        if(Input.GetMouseButton(1))
        {
            if(Input.mousePosition.x > clickPosition.x)
                transform.RotateAround(Manager.instance.cameraPivot.position, Vector3.up, cameraSpeed * Time.deltaTime * Mathf.Abs(Input.mousePosition.x - clickPosition.x));

            if(Input.mousePosition.x < clickPosition.x)
                transform.RotateAround(Manager.instance.cameraPivot.position, Vector3.up, -cameraSpeed * Time.deltaTime * Mathf.Abs(Input.mousePosition.x - clickPosition.x));
        }
    }
}
