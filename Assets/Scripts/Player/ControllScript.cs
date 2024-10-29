using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllScript : MonoBehaviour
{
    public HPScript hp;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            hp.hpChange(-10);
    }
}
