using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private AdvancedPlayerMovement _playerMovement;
    [SerializeField] private float _rotationSpeed = 5f;

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _playerMovement.MaxSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _target.rotation, _rotationSpeed * Time.deltaTime);
    }
}