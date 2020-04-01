using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    public float mouseSensitivity = 250f;
    float xRotation = 0f;

    public Transform body;
    public GameObject player;
    PlayerMovement playerMovement;

    public float maxTilt = 30f;
    public float leanSpeed = 5f;
    public float leanBackSpeed = 6f;
    float tilt = 0;



    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        float z = 0;

        if (playerMovement.isWallrunningRight && !playerMovement.isGrounded) {
            z = LeanRight();
        }else if (playerMovement.isWallrunningLeft && !playerMovement.isGrounded) {
            z = LeanLeft();
        } else {
            z = LeanBack();
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, z);
        body.Rotate(Vector3.up * mouseX);
    }

    private float LeanLeft()
    {
        float currAngle = transform.rotation.eulerAngles.z;

        float targetAngle = maxTilt - 360;

        if (currAngle > 180.0) {
            targetAngle = 360.0f - maxTilt;
        }

        float angle = Mathf.Lerp(currAngle, targetAngle, leanSpeed * Time.deltaTime);

        return angle;
    }

    private float LeanRight()
    {
        float currAngle = transform.rotation.eulerAngles.z;

        float targetAngle = maxTilt;

        if (currAngle > 180.0) {
            currAngle = 360 - currAngle;
        }

        float angle = Mathf.Lerp(currAngle, targetAngle, leanSpeed * Time.deltaTime);

        return angle;
    }

    private float LeanBack() {
        float currAngle = transform.rotation.eulerAngles.z;

        float targetAngle = 0f;

        if(currAngle > 180) {
            targetAngle = 360;
        }

        float angle = Mathf.Lerp(currAngle, targetAngle, leanSpeed * Time.deltaTime);

        return angle;
    }
}
