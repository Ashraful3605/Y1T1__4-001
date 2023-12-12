using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour
{
    public float fov;
    public Transform target;
    public float viewAngle;
    public float viewRange;
    public bool isSeen;

    int MoveSpeed = 4;
    int MaxDist = 5;
    int MinDist = 3;

    // Update is called once per frame
    void Update()
    {
        CanSeeTarget();  //runs custom method of checking 
        if (isSeen) //if the player has been seen
        {
            transform.LookAt(target);
            if (Vector3.Distance(transform.position, target.position) >= MinDist)
            {
                while (isSeen)
                {
                    transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                }
                //if (Vector3.Distance(transform.position, target.position) <= MaxDist)
                //{
                    //Here Call any function U want Like Shoot at here or something
                //}

            }
        }
        else // if the player has not been seen
        {
            Debug.Log("Nothing is there");
        }
    }

    void CanSeeTarget()
    {
        Vector3 toTarget = target.position - transform.position;
        if (Vector3.Angle(transform.forward, toTarget) <= viewAngle) //checks to see if the angle between our current facing and the target direction is within our viewing angle
        {
            RaycastHit Hit;
            if (Physics.Raycast(transform.position, toTarget, out RaycastHit hit, viewRange)) // use a raycast to see if there is anything in the way of the enemy (objects)
            {
                if (hit.transform.root == target) // if the thing hit is the target
                {
                    isSeen = true;
                }
            }
            else
            {
                isSeen = false;
            }
        }
    }

    void OnDrawGizmos() //draws a wire sphere over the enemy and a cone style fov object in front of the enemy to act as the detection zone. 
    {
        Gizmos.color = isSeen ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position, fov);
        float totalFOV = 70.0f;
        float rayRange = 2.0f;
        float halfFOV = totalFOV / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
    }
}
