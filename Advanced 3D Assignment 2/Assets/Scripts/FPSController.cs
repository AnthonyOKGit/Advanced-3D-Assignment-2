using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float playerSpeed = 6.0f;
    public float runningSpeed = 12.0f;
    public float jumpHeight = 8.0f;
    public float gravity = 10.0f;

    public Vector3 spawnPoint;
    public int health = 3;
    public TextMeshProUGUI healthText;
    public int XP = 0;
    public TextMeshProUGUI XPText;

    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Health and XP
        healthText.text = "x" + health;
        XPText.text = ": " + XP;

        // Running
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : playerSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : playerSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpHeight;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Camera rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Item pickup
        if (Input.GetKeyDown(KeyCode.E) && QuestSystem.Instance.questIsActive)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 3))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    QuestSystem.Instance.CheckItem(hit.collider.gameObject.name);

                    XP += 100;

                    Destroy(hit.collider.gameObject);
                }
            }
        }

    }

    // OnTriggerEnter with "Portal" tag go to the next level
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            Debug.Log("Portal entered");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (other.CompareTag("Enemy"))
        {
            health--;
            // Go back to spawn point
            characterController.enabled = false;
            transform.position = spawnPoint;
            characterController.enabled = true;
            // If health is 0, restart the level
            if (health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (other.CompareTag("Death"))
        {
            Debug.Log("DeathZone entered");
            // Go back to spawn point
            characterController.enabled = false;
            transform.position = spawnPoint;
            characterController.enabled = true;
        }
    }
}
