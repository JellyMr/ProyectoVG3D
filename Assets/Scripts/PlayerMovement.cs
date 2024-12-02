using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float maximumSpeed = 5f;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private float jumpButtonGracePeriod = 0.2f;

    [SerializeField]
    private Transform cameraTransform;

    [Header("Ground Detection Settings")]
    [SerializeField]
    private float groundDetectionRadius = 0.3f; // Ajustable desde el inspector
    [SerializeField]
    private Vector3 groundDetectionOffset = new Vector3(0, -1f, 0); // Ajustable desde el inspector
    [SerializeField]
    private LayerMask groundLayer; // Define qué capas son consideradas suelo

    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        rb.freezeRotation = true; // Prevenir la rotación automática del Rigidbody
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 2;
        } 

        animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        // Verificar si está en el suelo
        isGrounded = CheckGrounded();
        if (isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        // Lógica de animaciones basada en el estado del jugador
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            animator.SetBool("IsFalling", false);

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                animator.SetBool("IsJumping", true);
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            animator.SetBool("IsGrounded", false);
            isGrounded = false;

            if ((isJumping && rb.velocity.y < 0) || rb.velocity.y < -2)
            {
                animator.SetBool("IsFalling", true);
            }
        }

        // Rotación del jugador hacia la dirección del movimiento
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Movimiento horizontal
        Vector3 velocity = movementDirection * (maximumSpeed * inputMagnitude);
        velocity.y = rb.velocity.y; // Conserva la velocidad vertical
        rb.velocity = velocity;
    }

    private bool CheckGrounded()
    {
        // Calcula la posición de la detección de suelo
        Vector3 detectionPoint = transform.position + groundDetectionOffset;

        // Verifica si el jugador está tocando el suelo
        return Physics.CheckSphere(detectionPoint, groundDetectionRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        if (capsuleCollider != null)
        {
            // Dibuja un gizmo para la detección de suelo
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Vector3 detectionPoint = transform.position + groundDetectionOffset;
            Gizmos.DrawWireSphere(detectionPoint, groundDetectionRadius);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
