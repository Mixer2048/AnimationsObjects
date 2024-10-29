using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationController : MonoBehaviour
{
    public UnityEvent OnJumped;
    public UnityEvent OnLanded;

    private Animator _animator;

    private bool _isGrounded;

    int injuredLayerIndex;

    public void SetAnimatorParameters(float x, float z)
    {
        if (_isGrounded == true)
        {
            _animator.SetFloat("Speed_X", x);
            _animator.SetFloat("Speed_Z", z);
        }
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

    public void damageTaken(float hpRatio)
    {
        _animator.SetTrigger("onHit");
        _animator.SetLayerWeight(injuredLayerIndex, 1 - hpRatio);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        injuredLayerIndex = _animator.GetLayerIndex("injured");
    }   
}
