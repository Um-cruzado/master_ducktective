using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Vector2 LookSensitivity;
    [SerializeField]
    private float lookMin = -60, lookMax = 60;
    private float verticalLookRotation;

    private void CameraLook()
    {
        transform.parent.Rotate(Vector3.up * Input.GetAxis("Mouse X") * LookSensitivity.x);
        verticalLookRotation += Input.GetAxis("Mouse Y") * LookSensitivity.y;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, lookMin, lookMax);
        transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    private void Update()
    {
        CameraLook();
    }

    private void Start()
    {
        //Remove later
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
