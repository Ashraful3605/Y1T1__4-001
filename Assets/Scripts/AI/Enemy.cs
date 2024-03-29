using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{
    public float fov;
    public Transform target;
    public float viewAngle;
    public float viewRange;
    public bool isSeen;
    public bool isSeenWR;

    int MaxDist = 7;
    float MinDist = 1f;

    public Transform[] waypoints;
    private int currentWaypointIndex;
    public float speed = 1f;

    public float waitTime = 2f;
    private float waitCounter = 0f;
    private bool waiting = false;

    public int timeSeen;
    public int timeSeen_GO; //changeable integer for the time the player 
                            //can be seen until they reach the gameover screen

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftControl)) //when crouching is decided 
        {
            MaxDist = 5;
            MinDist = 3;
        }
        else
        {
            MaxDist = 7;
            MinDist = 3;
        }

        CanSeeTarget();  //runs custom method of checking 
        if (isSeenWR)
            {
                transform.position += transform.forward * speed * Time.deltaTime; //move towards the player
            }
        if (isSeen) //if the player has been seen
        {
            transform.LookAt(target);
            
            if (Vector3.Distance(transform.position,target.position)>= MinDist)
            {
                isSeenWR = true;
                transform.position += transform.forward * speed * Time.deltaTime; //move towards the player
                    
            }
            if (Vector3.Distance(transform.position, target.position) < 1)
                {
                    SceneManager.LoadScene("Game Over Scene");
                }
            
        }
        else // if the player has not been seen
        {
            timeSeen = 0;
            Debug.Log("Nothing is there");
        }
        
    }


    //Late Update is called once per frame after Update is called
    void LateUpdate()
    {
        if (!isSeen)
        {
            if (waiting) //if the enemy is waiting
            {
                waitCounter += Time.deltaTime; //increment the time to wait by the time.deltatime
                if (waitCounter < waitTime) //if this new value is less than the wait time
                    return; 
                waiting = false; //The enemy is no longer waiting
            }

            Transform wp = waypoints[currentWaypointIndex]; //put this new transform in the current way point array at the index of the current waypoint
            if (Vector3.Distance(transform.position, wp.position) < 0.01f) //If the distance of the enemy to the waypoint is greater than 0.01
            {
                transform.position = wp.position; //The current position becomes the position of the waypoint 
                waitCounter = 0f; //wait counter is set to 0
                waiting = true; //the enemy is now waiting 

                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; //increment current waypoint index but make sure its smaller than the total length of this array
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, wp.position, speed * Time.deltaTime); //else move towards the position of the last waypoint
                transform.LookAt(wp.position); //look at the waypoint position
            }
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