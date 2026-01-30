using UnityEngine;
using UnityEngine.InputSystem;
using KBCore.Refs;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    private InputAction move;
    private InputAction look;

    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float gravity = -30.0f;
    private Vector3 velocity;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private float mouseSensY = 5f;

    [SerializeField, Self] private CharacterController controller;
    [SerializeField, Child] private Camera cam;

    private void OnValidate()
    {
        this.ValidateRefs();
    }

    void Start()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        look = InputSystem.actions.FindAction("Player/Look");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 readmove = move.ReadValue<Vector2>();
        Vector2 readlook = look.ReadValue<Vector2>();

        // Player Movement
        Vector3 movement = transform.right * readmove.x + 
            transform.forward * readmove.y;

        velocity.y += gravity * Time.deltaTime;
        movement *= maxSpeed * Time.deltaTime;
        movement += velocity;

        controller.Move(movement);

        // Player Rotation
        transform.Rotate(Vector3.up, readlook.x * rotationSpeed * Time.deltaTime);

        // Rotate The Camera
        mouseSensY = mouseSensY * readlook.y;
        mouseSensY = Mathf.Clamp(mouseSensY, -90f, 90f);
        cam.gameObject.transform.localRotation = Quaternion.Euler(mouseSensY, 0, 0);
    }
}
