using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float angle = 1;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, angle * 0.25f);
    }
}
