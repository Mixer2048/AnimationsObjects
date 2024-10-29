using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController _animController;
    
    void Update()
    {
        if (Input.GetMouseButton(1))
            _animController.Block(true);
        else
            _animController.Block(false);
    }
}
