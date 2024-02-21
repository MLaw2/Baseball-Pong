using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class TurretBehavior : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private void Update() {
        transform.LookAt(player.transform.position);
    }
    public void ShootBall() {
        GameObject bullet = BallManager.SharedInstance.GetPooledObject();
        if (bullet != null) {
            bullet.transform.SetPositionAndRotation(transform.position, transform.rotation);
            bullet.SetActive(true);
        }
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward);
    }
}
