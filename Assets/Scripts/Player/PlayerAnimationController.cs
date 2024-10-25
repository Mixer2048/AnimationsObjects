using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationController : MonoBehaviour
{
    public UnityEvent OnJumped;
    public UnityEvent OnLanded;

    private Animator _animator;

    private bool _isGrounded;

    public void SetAnimatorParameters(float x, float z)
    {
        _animator.SetFloat("Speed_X", x);
        _animator.SetFloat("Speed_Z", z);
    }

    public void SetGroundState(bool state) 
    {
        _isGrounded = state;
        _animator.SetBool("OnGround", state);
    }

    public void Jump()
    {
        if (_isGrounded)
            _animator.SetTrigger("Jump");
    }

    public void JumpStart()
    {
        OnJumped?.Invoke();
    }

    public void JumpEnd()
    {
        OnLanded?.Invoke();
    }

    public PlayerState GetCurrentState()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            return PlayerState.Falling;

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Landing"))
            return PlayerState.Landing;
        
        return PlayerState.Moving;
    }

    private void Start() => _animator = GetComponent<Animator>();
}
