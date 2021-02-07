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
    private Vector3 amount;

    // Track yaw (left and right rotations)
    private float yaw;
    private Vector3 transformPosition;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        yaw = transform.eulerAngles.y;
        transformPosition = transform.position;
    }

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
            amount = currPos - prevPos;

            // calculate left and right rotations
            yaw += amount.x * lookSensitivity * Time.deltaTime;
            yaw %= 360;

            // move camera
            transform.position += transform.forward * amount.y * movementSpeed * Time.deltaTime;
            transformPosition = transform.position;
        }

        // rotate camera
        transform.position = transformPosition;
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        gameManager.SendMessage("GameOver");
    }
}
