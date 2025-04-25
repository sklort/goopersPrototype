using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public bool canJump;
    public int jumpCount = 1;
    private int currentJumpCount = 0;

    private Vector3 playerOrigin;
    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Jump
        if (Input.GetKey(KeyCode.Space) && currentJumpCount < jumpCount)
        {
            Jump();
        }

        playerOrigin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (Physics.Raycast(playerOrigin, Vector3.down, 1.2f))
        {
            currentJumpCount = 0;
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }

    public void Jump()
    {
        playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        currentJumpCount++;
    }
    
}
