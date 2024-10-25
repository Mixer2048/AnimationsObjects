using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField, Range(1f, 100f)] private float _jumpForce = 10f;
    [SerializeField] private PlayerAnimationController _animController;

    private Rigidbody _rb;

    public void Jump() => _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);

    private void Start() => _rb = GetComponent<Rigidbody>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _animController.Jump();
    }
}
