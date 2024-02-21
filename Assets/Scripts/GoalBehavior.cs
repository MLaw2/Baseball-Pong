using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")){
            other.gameObject.SetActive(false);
            TurretManager.SharedInstance.ShootNewBall();
            //Destroy(other.gameObject);
        }
    }
}
