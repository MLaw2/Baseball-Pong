using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    // for passing direction of the ball hit
    enum Direction { Left, Right, Up }

    // For tuning dual direction input test
    [SerializeField] private float time_buffer;

    // Code from Physics Week
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
    private float left_mouse_up;
    private float right_mouse_up;
    private bool armed;
    private float charge_percent;

    // i can use a coroutine to have a pool of turret ball shooters
    // from there, only spawn one ball out of all of them
    // coroutine to pause the shooting until a goal is made or the ball dies (ball dies is easier and more practical)


    void Start()
    {
        origin = player_cam.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        move_vector = new UnityEngine.Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));   // section for calculating the movement vector for the transform
        UnityEngine.Vector3.Normalize(move_vector);
        transform.Translate(move_speed * Time.deltaTime * move_vector);

        left_mouse_down = Input.GetMouseButton(0);
        right_mouse_down = Input.GetMouseButton(1);
        if (left_mouse_down || right_mouse_down) {
            armed = true;
            // TODO add limit to charge_percent
            charge_percent += 1 * charge_rate;
        }

        // if you release the mouse, measure the time it was released (for recording dual input)
        if (Input.GetMouseButtonUp(0)) {
            left_mouse_up = Time.time;
        }
        if (Input.GetMouseButtonUp(1)) {
            right_mouse_up = Time.time;
        }

        if (!left_mouse_down && !right_mouse_down && armed) {  // Section for calculating hitting the ball
            if (Mathf.Abs(left_mouse_up - right_mouse_up) < 0.01) {
                Swing(Direction.Up);
            }
            else if (left_mouse_up > right_mouse_up) {
                Swing(Direction.Left);
            }
            else if (right_mouse_up < left_mouse_up) {
                Swing(Direction.Right);
            }
            else {
                Debug.Log("Either nothing is supposed to happen or we lose");
            }
            armed = false;
            charge_percent = 0;
        }
    }

    // TODO IMPLEMENT DIRECTIONAL HITS
    void Swing(Direction dir) {
        if (Physics.Raycast(origin.position, origin.TransformDirection(Vector3.forward), out target, swing_distance)) {
            if (target.transform.CompareTag("Ball")) {
                Vector3 hit_vector;

                // Calculate the new vector for the ball
                if (dir == Direction.Right) {
                    Debug.Log("RIGHT");
                    hit_vector = origin.TransformDirection(Vector3.forward + Vector3.right);
                }
                else if (dir == Direction.Left) {
                    Debug.Log("LEFT");
                    hit_vector = origin.TransformDirection(Vector3.forward + Vector3.left);
                }
                else { // dir is Up
                    Debug.Log("UP");
                    hit_vector = origin.TransformDirection(Vector3.forward + Vector3.up);
                }

                target.rigidbody.AddForce((charge_percent / 100) * swing_force * hit_vector, ForceMode.VelocityChange);
            }
        }
        else { // missed
            Debug.Log("Did not Hit");
        }
    }
}
