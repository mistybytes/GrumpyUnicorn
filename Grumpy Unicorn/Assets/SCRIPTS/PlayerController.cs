using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public Camera mainCamera;
    public float jumpHeight = 5.0f;
    public float gravity = -30f;
    public float groundCheckDistance = 0.1f;
    public int carrotsCollected = 0;
    public bool isInvincible = false;

    public GameObject[] playerSkins;
    private int currentSkinIndex = 0;

    public Joystick moveJoystick;

    private Vector3 targetPosition;
    private bool isMoving;

    private CharacterController characterController;
    private Animator animator;

    public int CarrotsCollected
    {
        get { return carrotsCollected; }
        set { carrotsCollected = value; }
    }

    public TextMeshProUGUI carrotText;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

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

        SetPlayerSkin(currentSkinIndex);
    }

    Vector3 joystickCenter;

   private void Update()
{
    if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
    {
        HandleJoystickMovement();
    }
    else
    {
        HandleMouseJoystickMovement();
    }

    float currentSpeed = characterController.velocity.magnitude;
    animator.SetFloat("MoveSpeed", currentSpeed);  

    if (isMoving)
    {
        Move();
    }
}

private void HandleJoystickMovement()
{
    Vector3 direction = new Vector3(moveJoystick.Horizontal, 0, moveJoystick.Vertical).normalized;
    if (direction.magnitude >= 0.1f)
    {
        targetPosition = transform.position + direction * moveSpeed;
        isMoving = true;
    }
    else
    {
        isMoving = false;
    }
}

private void HandleMouseJoystickMovement()
{
    if (Input.GetMouseButtonDown(0))
    {
        joystickCenter = Input.mousePosition;
    }

    if (Input.GetMouseButton(0))
    {
        Vector3 direction = (Input.mousePosition - joystickCenter).normalized;
        if (direction.magnitude >= 0.1f)
        {
            targetPosition = transform.position + new Vector3(direction.x, 0, direction.y) * moveSpeed;
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}

    private void Move()
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;

        if (direction.magnitude < 0.1f)
        {
            isMoving = false;
            targetPosition = transform.position;  
            characterController.Move(Vector3.zero);  
            animator.SetFloat("MoveSpeed", 0);       
            return;
        }

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        Vector3 moveDirection = new Vector3(direction.x, 0, direction.z).normalized * moveSpeed * Time.deltaTime;
        characterController.Move(moveDirection);
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

    public void ChangePlayerSkin()
    {
        currentSkinIndex++;

        if (currentSkinIndex >= playerSkins.Length)
        {
            currentSkinIndex = 0;
        }

        SetPlayerSkin(currentSkinIndex);
    }

    private void SetPlayerSkin(int skinIndex)
    {
        for (int i = 0; i < playerSkins.Length; i++)
        {
            playerSkins[i].SetActive(i == skinIndex);
        }
    }

    public void StartInvulnerability(float duration)
    {
        if (!isInvincible)
        {
            StartCoroutine(InvulnerabilityTimer(duration));
        }
    }

    private IEnumerator InvulnerabilityTimer(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }
}
