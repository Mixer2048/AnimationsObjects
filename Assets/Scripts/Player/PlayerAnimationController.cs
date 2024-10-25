using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] Animator _animator;

    public void SetAnimatorParameters(float x, float z)
    {
        _animator.SetFloat("Speed_X", x);
        _animator.SetFloat("Speed_Z", z);
    }

    private void Start() => _animator = _player.GetComponent<Animator>();
}
