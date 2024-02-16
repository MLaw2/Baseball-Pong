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
    void Start()
    {
        rbody = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        move_vector = new UnityEngine.Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        UnityEngine.Vector3.Normalize(move_vector);
        if (Input.GetButtonDown("Jump")) {
            rbody.AddForce(UnityEngine.Vector3.up * jump_strength);
        }
        // if(Math.Abs(rbody.velocity.x) + Math.Abs(rbody.velocity.z) <= max_speed){
        // rbody.AddForce(move_vector * move_speed, );
        // }
        transform.Translate(move_vector * move_speed * Time.deltaTime);
    }
}
