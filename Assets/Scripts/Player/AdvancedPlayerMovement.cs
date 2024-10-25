using UnityEngine;

public class AdvancedPlayerMovement : MonoBehaviour
{
    public float MaxSpeed { get { return _maxSpeed; } }

    [SerializeField, Range(1f, 20f)] private float _maxRunSpeed = 5f;
    [SerializeField, Range(1f, 20f)] private float _maxSprintSpeed = 10f;
    [SerializeField, Range(1f, 20f)] private float _maxSideStepSpeed = 5f;
    [SerializeField, Range(1f, 20f)] private float _acceleration = 3f;
    [SerializeField, Range(1f, 20f)] private float _deceleration = 5f;

    private float _maxSpeed;
    private float _speedZ = 0;
    private float _speedX = 0;

    Rigidbody _rb;
    Vector3 _velocity;

    [SerializeField] private PlayerAnimationController _animController;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");
        bool sprintInput = Input.GetKey(KeyCode.LeftShift);

        if (xInput != 0f)
            _speedX = Mathf.Lerp(_speedX, xInput * _maxSideStepSpeed, _acceleration * Time.deltaTime);
        else if (_speedX != 0)
            _speedX = Mathf.Lerp(_speedX, xInput * _maxSideStepSpeed, _deceleration * Time.deltaTime);

        if (sprintInput)
            _maxSpeed = _maxSprintSpeed;
        else
            _maxSpeed = _maxRunSpeed;

        if (zInput != 0f)
            _speedZ = Mathf.Lerp(_speedZ, zInput * _maxSpeed, _acceleration * Time.deltaTime);
        else if (_speedZ != 0)
            _speedZ = Mathf.Lerp(_speedZ, zInput * _maxSpeed, _deceleration * Time.deltaTime);

        _animController.SetAnimatorParameters(_speedX / _maxSideStepSpeed, _speedZ / _maxRunSpeed);

        _velocity = new Vector3(xInput, 0, zInput).normalized;
        _velocity.x *= _speedX < 0 ? -_speedX : _speedX;
        _velocity.z *= _speedZ < 0 ? -_speedZ : _speedZ;

        _velocity = transform.TransformDirection(_velocity);

        _velocity.y = _rb.velocity.y;
        _rb.velocity = _velocity;

        //Debug.Log(_rb.velocity.z);
    }
}