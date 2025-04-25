using System;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{

    private Target enemy;
    private GameObject enemyObject;

    private void Start()
    {   
        // enemyObject = 
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        if (player != null)
        {
            
        }
    }
}
