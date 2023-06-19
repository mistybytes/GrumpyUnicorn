using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public Camera mainCamera;
    public float speed = 5.0f;
    public float jumpHeight = 5.0f;
    public float gravity = -30f;
    public float groundCheckDistance = 0.1f;
    public float minMoveDistance = 0.1f;
    public int carrotsCollected = 0;

    private Vector3 targetPosition;
    private bool isMoving;

    private CharacterController characterController;
    private bool isGrounded;
    private Vector3 velocity;

    public int CarrotsCollected
    {
        get { return carrotsCollected; }
        set { carrotsCollected = value; }
    }

    public TextMeshProUGUI carrotText;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        if (GameSaveManager.Instance == null)
        {
            Debug.LogError("GameSaveManager.Instance is null");
        }
        else
        {
            LoadGame();
        }
        if (carrotText != null)
        {
            UpdateCarrotText();
        }
        else
        {
            Debug.LogError("carrotText is null");
        }
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            HandleTouchInput();
        }
        else
        {
            HandleMouseInput();
        }

        if (isMoving)
        {
            Move();
        }
    }

    private void HandleMouseInput()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0 && EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            return;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SetTargetPosition();
        }
    }

    private void SetTargetPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            targetPosition = hit.point;
            isMoving = true;
        }
    }

    private void Move()
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0; // Zablokuj ruch wzd³u¿ osi Y

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

        Vector3 moveDirection = direction.normalized * moveSpeed * Time.deltaTime;
        characterController.Move(moveDirection);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }

    public void AddCarrots(int amount)
    {
        carrotsCollected += amount;
        UpdateCarrotText();
    }

    public void UpdateCarrotText()
    {
        if (carrotText != null)
        {
            carrotText.text = "Carrots: " + carrotsCollected;
        }
    }

    public void LoadGame()
    {
        if (GameSaveManager.Instance != null)
        {
            GameSaveManager.Instance.LoadGame(this);
            UpdateCarrotText();
        }
        else
        {
            Debug.LogError("GameSaveManager.Instance is null");
        }
    }

    public void SaveGame()
    {
        if (GameSaveManager.Instance != null)
        {
            GameSaveManager.Instance.SaveGame(this);
        }
        else
        {
            Debug.LogError("GameSaveManager.Instance is null");
        }
    }

    public void GameOver()
    {
        GameSaveManager.Instance.ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
