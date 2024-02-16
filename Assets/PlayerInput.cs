using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Code from Physics Week
    private Rigidbody rbody;
    [SerializeField] private float move_speed;
    // [SerializeField] private float max_speed;
    [SerializeField] private float jump_strength;
    private UnityEngine.Vector3 move_vector;
    // Start is called before the first frame update

    // Hit the ball with a raycast. Maybe do other casts for better flexibility (aim assist???)
    private RaycastHit target;
    [SerializeField] private GameObject player_cam;
    private Transform origin;
    [SerializeField] private float swing_distance;
    [SerializeField] private float swing_force;


    void Start()
    {
        rbody = GetComponentInChildren<Rigidbody>();
        origin = player_cam.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        move_vector = new UnityEngine.Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        UnityEngine.Vector3.Normalize(move_vector);
        transform.Translate(move_vector * move_speed * Time.deltaTime);
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(origin.position, origin.TransformDirection(Vector3.forward), out target, swing_distance)) {
                Debug.Log(target.transform.gameObject);
                if (target.transform.CompareTag("Ball")) {
                    Debug.DrawRay(origin.position, origin.TransformDirection(Vector3.forward) * target.distance, Color.yellow);
                    Debug.Log("Did Hit");

                    target.rigidbody.AddForce(origin.TransformDirection(Vector3.forward) * swing_force, ForceMode.VelocityChange);
                }
            }
            else {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
    }
}
