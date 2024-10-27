using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    Animator animator;
    bool onGround = true;
    int injuredLayerIndex;

    void Start()
    {
        animator = GetComponent<Animator>();
        injuredLayerIndex = animator.GetLayerIndex("injured");
    }

    public void setAnimatorParameters(float x, float z)
    {
        if (onGround == true)
        {
            animator.SetFloat("Speed_X", x);
            animator.SetFloat("Speed_Z", z);
        }
    }

    public void damageTaken(float hpRatio)
    {
        animator.SetTrigger("onHit");
        animator.SetLayerWeight(injuredLayerIndex, 1 - hpRatio);
    }
}
