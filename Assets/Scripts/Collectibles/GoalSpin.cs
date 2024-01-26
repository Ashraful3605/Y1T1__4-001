using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSpin : MonoBehaviour
{
    public float rotationSpeed = 50f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
    }
}
