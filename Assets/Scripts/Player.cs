using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private float speed = 5.0f;
    private float sprintSpeed = 10.0f;
    private float stamina = 200;
    private float jumpHeight = 2.0f;
    public int health;
    private bool isGrounded = true;
    
    [SerializeField] private Rigidbody rb;

    //private PlayableCharacter character;

    void Start() {

    }


    void Update() {
        HandleInput();
    }

    private void HandleInput() {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0.0f) {
            speed = sprintSpeed;
            stamina -= 1.0f;
        }
        else {
            speed = 5.0f;

            if (stamina < 200.0f) {
                stamina += 0.4f;
            }
        }

        if (Input.GetKey(KeyCode.W)) {
            movement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S)) {
            movement += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A)) {
            movement += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D)) {
            movement += Vector3.right;
        }

        if (movement != Vector3.zero) {
            movement = movement.normalized * speed;
        }

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }
}
