using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
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

    [SerializeField] private Slider power_bar;
    [SerializeField] private float charge_rate;


    private bool left_mouse_down;
    private bool right_mouse_down;
    private float left_mouse_up;
    private float right_mouse_up;
    private bool armed;
    // for passing direction of the ball hit
    enum Direction { Left, Right, Up }

    // For tuning dual direction input test
    [SerializeField] private float time_buffer;

    void Start()
    {
        origin = player_cam.GetComponent<Transform>();
        if (power_bar == null) {
            Debug.Log("Still Got Problems");
        }
    }

    // Update is called once per frame
    void Update()
    {
        move_vector = new UnityEngine.Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));   // section for calculating the movement vector for the transform
        UnityEngine.Vector3.Normalize(move_vector);
        transform.Translate(move_speed * Time.deltaTime * move_vector);

        left_mouse_down = Input.GetMouseButton(0);
        right_mouse_down = Input.GetMouseButton(1);
        //left_mouse_down = Input.GetKey(KeyCode.N);
        //right_mouse_down = Input.GetKey(KeyCode.M);

        // get ui working
        up_arrow.SetActive(left_mouse_down && right_mouse_down);
        left_arrow.SetActive(left_mouse_down && !right_mouse_down);
        right_arrow.SetActive(right_mouse_down && !left_mouse_down);

        if (left_mouse_down || right_mouse_down) {
            armed = true;
            power_bar.value += 1 * charge_rate;
        }

        // if you release the mouse, measure the time it was released (for recording dual input)
        if (Input.GetMouseButtonUp(0)) {
        //if (Input.GetKeyUp(KeyCode.N)) {
            left_mouse_up = Time.time;
        }
        if (Input.GetMouseButtonUp(1)) {
        //if (Input.GetKeyUp(KeyCode.M)) {
            right_mouse_up = Time.time;
        }

        if (!left_mouse_down && !right_mouse_down && armed) {  // Section for calculating hitting the ball
            if (Mathf.Abs(left_mouse_up - right_mouse_up) < time_buffer) {
                Swing(Direction.Up);
            }
            else if (left_mouse_up > right_mouse_up) {
                Swing(Direction.Left);
            }
            //else if (right_mouse_up < left_mouse_up) {
            //    Swing(Direction.Right);
            //}
            else {
                Swing(Direction.Right);
                //Debug.Log("Either nothing is supposed to happen or we lose");
                //Debug.Log(left_mouse_up + " : " + right_mouse_up);
            }
            armed = false;
            power_bar.value = 0;
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
                    hit_vector = origin.TransformDirection(Vector3.forward / 2 + Vector3.right + Vector3.up);
                }
                else if (dir == Direction.Left) {
                    Debug.Log("LEFT");
                    hit_vector = origin.TransformDirection(Vector3.forward / 2 + Vector3.left + Vector3.up);
                }
                else { // dir is Up
                    Debug.Log("UP");
                    hit_vector = origin.TransformDirection(Vector3.up);
                }
                hit_vector.Normalize();

                target.rigidbody.AddForce((power_bar.value / 100) * swing_force * hit_vector, ForceMode.VelocityChange);
            }
        }
        else { // missed
            Debug.Log("Did not Hit");
        }
    }
}
