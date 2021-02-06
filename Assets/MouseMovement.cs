using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    // Set mouse sensitivity
    public float lookSensitivity = 0.5f;
    public float movementSpeed = 0.05f;

    // Track mouse positions
    private Vector2 prevPos;
    private Vector2 currPos;

    // Track yaw (left and right rotations)
    private float yaw = 0.0f;

    // Update is called once per frame
    void Update()
    {
        // when left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            prevPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        // when left mouse button is held
        if (Input.GetMouseButton(0))
        {
            // get current mouse position
            currPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            // calculate amount of change
            Vector2 amount = currPos - prevPos;

            // calculate left and right rotations
            yaw += amount.x * lookSensitivity * Time.deltaTime;
            yaw %= 360;

            // move camera
            transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
            transform.position += transform.forward * amount.y * movementSpeed * Time.deltaTime;
        }
    }
}
