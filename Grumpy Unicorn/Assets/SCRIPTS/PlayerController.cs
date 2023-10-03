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

        float currentSpeed = characterController.velocity.magnitude;
        animator.SetFloat("MoveSpeed", currentSpeed);  // Setting the speed value in the animator.

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
    direction.y = 0;

    // Sprawdzenie, czy różnica między bieżącą pozycją a docelową jest wystarczająco duża
    if (direction.magnitude < 0.1f)
    {
        isMoving = false;
        targetPosition = transform.position;  // Zresetuj targetPosition
        characterController.Move(Vector3.zero);  // Zeruj prędkość ruchu
        animator.SetFloat("MoveSpeed", 0);       // Bezpośrednie ustawienie wartości prędkości na 0 w Animatorze
        return;
    }

    // Obracanie postaci tylko jeśli się porusza
    Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

    Vector3 moveDirection = direction.normalized * moveSpeed * Time.deltaTime;
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
