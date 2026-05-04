using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    [Header("Lever Settings")]

    [SerializeField] private UnityEvent _onLeverPull;
    private Animator _animation;
    private bool _isPlayerInRange;

    private void Start()
    {
        _animation = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_isPlayerInRange && Input.GetMouseButtonDown(0))
        {
            _animation.SetTrigger("Pull");
            _onLeverPull?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerInRange = false;
        }
    }
}
