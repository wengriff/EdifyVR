using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbToKinematic : MonoBehaviour
{
    private Rigidbody rb;
    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }
    public void SwitchToKinematic()
    {
        rb.isKinematic = true;
    }

    public void SwitchToCollide()
    {
        rb.isKinematic = false;
    }
}
