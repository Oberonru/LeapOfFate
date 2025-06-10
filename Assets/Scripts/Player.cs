using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraSpeed = 1;
    [SerializeField] private float offsetY = 1.5f;
    [SerializeField] private GameObject stopPanel;
    private bool isGrounded;
    private Vector3 velocity;

    void Update()
    {
        if (!stopPanel.activeSelf)
        {
            isGrounded = controller.isGrounded;
            Vector3 target = new Vector3(playerTransform.position.x, playerTransform.position.y + offsetY, cameraTransform.position.z);
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, target, cameraSpeed * Time.deltaTime);
            float move = Input.GetAxis("Horizontal");
            if (move > 0)
            {
                playerTransform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (move < 0) 
            {
                playerTransform.rotation = Quaternion.Euler(0, -90, 0);
            }
                    
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            controller.Move(speed * Time.deltaTime * Vector3.right * Input.GetAxis("Horizontal"));
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        
    }
}
