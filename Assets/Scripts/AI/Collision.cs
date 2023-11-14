using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    void Update()
    {
        LayerMask wall = LayerMask.GetMask("Wall");

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 2.8f, wall))
        {
            Debug.Log("Hit wall");
            Debug.DrawRay(transform.position, transform.forward * 3, Color.green);

        }
        else
        {
            Debug.Log("Hit Nothing");
            Debug.DrawRay(transform.position, transform.forward * 3, Color.red);
        }
    }
}
