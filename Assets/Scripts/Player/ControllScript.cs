using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllScript : MonoBehaviour
{
    public HPScript hp;
    public AnimatorScript anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            hp.hpChange(-10);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        anim.setAnimatorParameters(x, z);
    }
}
