using UnityEngine;

public class AdvancedPlayerMovement : MonoBehaviour
{
    public float MaxSpeed { get { return _maxForwardSpeed; } }

    [SerializeField, Range(1f, 20f)] private float _runSpeed = 5f;
    [SerializeField, Range(1f, 20f)] private float _sprintSpeed = 10f;
    [SerializeField, Range(1f, 20f)] private float _sideStepSpeed = 5f;
    [SerializeField, Range(1f, 20f)] private float _acceleration = 3f;
    [SerializeField, Range(1f, 20f)] private float _deceleration = 5f;

    private float _maxForwardSpeed;
    private float _maxSideSpeed;
    private float _speedZ = 0;
    private float _speedX = 0;

    Rigidbody _rb;
    Vector3 _velocity;

    [SerializeField] private PlayerAnimationController _animController;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _maxForwardSpeed = _runSpeed;
        _maxSideSpeed = _sideStepSpeed;
    }

    private void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
            _maxForwardSpeed = _sprintSpeed;
        else
            _maxForwardSpeed = _runSpeed;

        GetAnimationState();

        if (xInput != 0f)
            _speedX = Mathf.Lerp(_speedX, xInput * _maxSideSpeed, _acceleration * Time.deltaTime);
        else if (_speedX != 0)
            _speedX = Mathf.Lerp(_speedX, xInput * _maxSideSpeed, _deceleration * Time.deltaTime);

        if (zInput != 0f)
            _speedZ = Mathf.Lerp(_speedZ, zInput * _maxForwardSpeed, _acceleration * Time.deltaTime);
        else if (_speedZ != 0)
            _speedZ = Mathf.Lerp(_speedZ, zInput * _maxForwardSpeed, _deceleration * Time.deltaTime);

        _animController.SetAnimatorParameters(_speedX / _sideStepSpeed, _speedZ / _runSpeed);
        
        _velocity = new Vector3(xInput, 0, zInput).normalized;
        _velocity.x *= _speedX < 0 ? -_speedX : _speedX;
        _velocity.z *= _speedZ < 0 ? -_speedZ : _speedZ;

        _velocity = transform.TransformDirection(_velocity);

        _velocity.y = _rb.velocity.y;
        _rb.velocity = _velocity;
    }

    private void GetAnimationState()
    {
        PlayerState state = _animController.GetCurrentState();
        Debug.Log(state);

        switch (state)
        {
            case PlayerState.Falling:
                _maxForwardSpeed = _runSpeed / 2;
                _maxSideSpeed = _sideStepSpeed / 2;
                break;
            case PlayerState.Landing:
                _maxForwardSpeed = 0;
                _maxSideSpeed = 0;
                _speedX = 0;
                _speedZ = 0;
                break;
            default:
                _maxSideSpeed = _sideStepSpeed;
                return;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
            _animController.SetGroundState(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
            _animController.SetGroundState(false);
    }
}