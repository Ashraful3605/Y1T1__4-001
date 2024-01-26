using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public bool isCollected = false;
    private void OnCollisionEnter(UnityEngine.Collision collision)
    { 

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            isCollected = true;
            Debug.Log("Coin has been collected");
        }

    }
}

    


