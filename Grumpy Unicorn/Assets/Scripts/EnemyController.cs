using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;
    public float slowedSpeed = 1.0f;
    private float currentSpeed;
    public float distance = 5.0f;
    public bool isSlowed = false;
    public bool isStopped = false;

    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    private Vector3 _destination;

    private void Start()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();
        if (_rigidbody == null)
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();
        }

        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        _startPosition = transform.position;
        _destination = _startPosition + new Vector3(distance, 0, 0);

        currentSpeed = speed;
    }

    private void Update()
    {
        if (isStopped)
        {
            currentSpeed = 0f;
        }
        else
        {
            currentSpeed = isSlowed ? slowedSpeed : speed;
        }
        MoveTowardsDestination();
    }

    private void MoveTowardsDestination()
    {
        Vector3 direction = (_destination - transform.position).normalized;
        float step = currentSpeed * Time.deltaTime;
        Vector3 movement = direction * step;
        transform.position = transform.position + movement;

        float distanceTolerance = 0.1f;
        if (Vector3.Distance(transform.position, _destination) < distanceTolerance)
        {
            _destination = _destination == _startPosition ? _startPosition + new Vector3(distance, 0, 0) : _startPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null && !playerController.isInvincible)
            {
                RestartGame();
            }
        }
    }

    private void RestartGame()
    {
        GameSaveManager.Instance.ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
