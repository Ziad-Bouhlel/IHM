using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float sprintSpeedMultiplier;
    public float rotationSpeed;
    public float jumpSpeed;
    public float jumpButtonGracePeriod;

    [SerializeField]
    private float jumpHorizontalSpeed;

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;
    private bool isGrounded;

    [SerializeField]
    private Transform cameraTransform;

    //public Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        //originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        // Apply sprinting speed multiplier if sprinting is active
        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= sprintSpeedMultiplier;
            animator.SetBool("IsSprinting", true);
        }
        else if (Input.GetMouseButtonUp(2) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= sprintSpeedMultiplier;
            animator.SetBool("IsSprinting", false);
        }

        inputMagnitude *= speed;

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            animator.SetBool("IsFalling", false);
            
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                animator.SetBool("IsJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
            animator.SetBool("IsGrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("IsFalling", true);
            }
        }

        Vector3 velocity = movementDirection * inputMagnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (isGrounded == false)
        {
            velocity = movementDirection * inputMagnitude * jumpHorizontalSpeed;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);
        }

    }

    private void OnAnimatorMove()
    {
        if (isGrounded)
        {
            Vector3 velocity = animator.deltaPosition;
            velocity.y = ySpeed * Time.deltaTime;

            characterController.Move(velocity);
        }
        else if (!isJumping) // Appliquer la gravité seulement lorsque le personnage est en l'air et n'est pas en train de sauter
        {
            ySpeed += Physics.gravity.y *6* Time.deltaTime;
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

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "water")
        {
            Debug.Log("Le joueur est tombé dans l'eau");
            // Réinitialiser la position du personnage au point de respawn
            transform.position = originalPos;
            // Réinitialiser la vitesse de chute
        }
    }*/
}
