using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    // Set mouse sensitivity
    private float lookSensitivity = 0.5f;
    private float movementSpeed = 0.05f;
    private float flyingMouseSensitivity = 50.0f;
    private float flyingMoveSpeed = 50.0f;

    // Track mouse positions
    private Vector2 prevPos;
    private Vector2 currPos;
    private Vector3 amount;

    // Track yaw (left and right rotations)
    private float yaw;
    private float pitch;
    private Vector3 transformPosition;

    // GameManager to indicate when game is over
    private GameManager gameManager;

    // Flying
    private bool flying;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        yaw = transform.eulerAngles.y;
        transformPosition = transform.position;
        flying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            flying = !flying;
        }

        if (flying)
        {
            // Disable gravity
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;

            // Mouse movement
            yaw += Input.GetAxis("Mouse X") * flyingMouseSensitivity * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * flyingMouseSensitivity * Time.deltaTime;

            // Constrain yaw and pitch
            yaw %= 360.0f;
            pitch = Mathf.Clamp(pitch, -90.0f, 90.0f);

            // Move camera
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            // Keyboard controls
            if (Input.GetKey("w"))
                transform.position += transform.forward * flyingMoveSpeed * Time.deltaTime;
            if (Input.GetKey("s"))
                transform.position -= transform.forward * flyingMoveSpeed * Time.deltaTime;
            if (Input.GetKey("a"))
                transform.position -= transform.right * flyingMoveSpeed * Time.deltaTime;
            if (Input.GetKey("c"))
                transform.position += transform.right * flyingMoveSpeed * Time.deltaTime;
        }
        else
        {
            // Enable gravity
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;

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
    }

    void OnTriggerEnter(Collider other)
    {
        gameManager.SendMessage("GameOver");
    }
}
