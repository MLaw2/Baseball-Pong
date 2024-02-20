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

    // Ball hitting segment
    [SerializeField] private GameObject left_arrow;
    [SerializeField] private GameObject right_arrow;
    [SerializeField] private GameObject up_arrow;
    [SerializeField] private GameObject charge_meter;
    [SerializeField] private float charge_rate;
    private bool left_mouse_down;
    private bool right_mouse_down;
    private bool armed;
    private float charge_percent;

    // i can use a coroutine to have a pool of turret ball shooters
    // from there, only spawn one ball out of all of them
    // coroutine to pause the shooting until a goal is made or the ball dies (ball dies is easier and more practical)


    void Start()
    {
        rbody = GetComponentInChildren<Rigidbody>();
        origin = player_cam.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        move_vector = new UnityEngine.Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));   // section for calculating the movement vector for the transform
        UnityEngine.Vector3.Normalize(move_vector);
        transform.Translate(move_vector * move_speed * Time.deltaTime);

        left_mouse_down = Input.GetMouseButton(0);
        right_mouse_down = Input.GetMouseButton(1);
        if (left_mouse_down || right_mouse_down) {
            armed = true;
            // TODO add limit to charge_percent
            charge_percent += 1 * charge_rate;
        }

        if (!left_mouse_down && !right_mouse_down && armed) {  // Section for calculating hitting the ball
            swing();
            armed = false;
        }
    }

    // TODO IMPLEMENT DIRECTIONAL HITS
    void swing() {
        if (Physics.Raycast(origin.position, origin.TransformDirection(Vector3.forward), out target, swing_distance)) {
            Debug.Log(target.transform.gameObject);
            if (target.transform.CompareTag("Ball")) {
                Debug.DrawRay(origin.position, origin.TransformDirection(Vector3.forward) * target.distance, Color.yellow);
                Debug.Log("Did Hit");

                target.rigidbody.AddForce(origin.TransformDirection(Vector3.forward) * swing_force * (charge_percent / 100), ForceMode.VelocityChange);
            }
        }
        else { // missed
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
