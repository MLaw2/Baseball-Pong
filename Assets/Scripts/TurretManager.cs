using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public static TurretManager SharedInstance;
    [SerializeField] private GameObject turret1;
    [SerializeField] private GameObject turret2;
    [SerializeField] private GameObject turret3;
    private void Awake() {
        SharedInstance = this;
    }
    public void ShootNewBall() {
        int number = UnityEngine.Random.Range(1, 3);
        Debug.Log(number);
        if (number == 1) {
            turret1.GetComponent<TurretBehavior>().ShootBall();
        }
        else if (number == 2) {
            turret2.GetComponent<TurretBehavior>().ShootBall();
        }
        else {
            turret3.GetComponent<TurretBehavior>().ShootBall();
        }
    }
}
