using UnityEngine;
using UnityEngine.InputSystem;

public class XROriginRightHandMovement : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionProperty rightControllerMove;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;   

    [Header("References")]
    [SerializeField] private Transform rightHandTransform;

    private CharacterController characterController;

    private void Awake()
    {
        // Get or add CharacterController component
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            characterController = gameObject.AddComponent<CharacterController>();
            // Set default CharacterController values
            characterController.height = 5f;
            characterController.radius = 0.3f;
            characterController.center = new Vector3(0, 0.9f, 0);
        }

        // Validate right hand transform
        if (rightHandTransform == null)
        {
            Debug.LogWarning("XROriginRightHandMovement: rightHandTransform is not assigned!");
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (rightControllerMove.action == null || rightHandTransform == null)
        {
            return;
        }

        Vector2 rightInput = rightControllerMove.action.ReadValue<Vector2>();

        // Calculate movement direction based on right hand orientation
        Vector3 forward = rightHandTransform.forward;
        Vector3 right = rightHandTransform.right;

        // Normalize directions
        forward.Normalize();
        right.Normalize();

        // Combine forward/back and left/right movement
        Vector3 moveDirection = forward * rightInput.y + right * rightInput.x;

        // Apply movement
        if (characterController != null && characterController.enabled)
        {
            // Move the character
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime); 
        }
    }

    private void OnEnable()
    {
        if (rightControllerMove.action != null)
            rightControllerMove.action.Enable();
    }

    private void OnDisable()
    {
        if (rightControllerMove.action != null)
            rightControllerMove.action.Disable();
    }
}