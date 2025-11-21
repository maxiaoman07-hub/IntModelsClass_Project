using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(CharacterController))]
public class SimpleFishMovement : MonoBehaviour
{
    public float speed = 1.5f;
    public float verticalSpeed = 1f;

    CharacterController controller;
    Transform xrCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // XR camera is the MainCamera inside the XR Origin
        xrCamera = Camera.main.transform;
    }

    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");  // left/right turn/swim
        float vert = Input.GetAxis("Vertical");      // forward/back swim

        // Headset forward movement (fish swims where you look)
        Vector3 forward = xrCamera.forward;
        forward.y = 0; // keep forward motion horizontal by default
        forward.Normalize();

        Vector3 right = xrCamera.right;
        right.y = 0;
        right.Normalize();

        // Movement
        Vector3 move = (forward * vert + right * horiz) * speed;

        // Up/down swimming (use Q/E or triggers if mapped)
        float updown = 0f;
        if (Input.GetKey(KeyCode.Q)) updown = 1f;
        if (Input.GetKey(KeyCode.E)) updown = -1f;

        move += Vector3.up * updown * verticalSpeed;

        controller.Move(move * Time.deltaTime);
    }
}
