using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Copied from Physics Weekly CheckIn
    [SerializeField] private float sensitivity;
    [SerializeField] private GameObject viewport;
    [SerializeField] private GameObject playerBody;

    void Start() {
        Cursor.visible = false;
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // TODO
    // Figure out how the hell to not have player body get disjointed from player

    // Update is called once per frame
    void Update() {
        // assisted by ChatGPT
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the camera based on mouse input
        transform.Rotate(mouseX * sensitivity * Vector3.up);
        viewport.transform.Rotate(mouseY * sensitivity * Vector3.left);

        // Rotate player rigidbody as well
        playerBody.transform.Rotate(mouseX * sensitivity * Vector3.up);

        // Optional: Limit the vertical rotation to avoid flipping the camera
        float currentXRotation = viewport.transform.rotation.eulerAngles.x;
        if (currentXRotation > 80 && currentXRotation < 180) {
            currentXRotation = 80;
        }
        else if (currentXRotation > 180 && currentXRotation < 280) {
            currentXRotation = 280;
        }
        viewport.transform.rotation = Quaternion.Euler(currentXRotation, transform.rotation.eulerAngles.y, 0);
    }
}
