using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Update()
    {
        float x = Input.mousePosition.normalized.x;
        float y = Input.mousePosition.normalized.y;

        //Debug.Log("x = " + x + " y = " + y);

        _animator.SetFloat("x_axis", x);
        _animator.SetFloat("y_axis", y);
    }
}
