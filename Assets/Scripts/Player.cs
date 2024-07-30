using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private float speed = 5.0f;
    private float sprintSpeed = 10.0f;
    private float stamina = 200;
    public int health;
    private bool isGrounded = true;
    private bool shouldMove = true;
    
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform elementalAttackPosition;

    [SerializeField] private GameObject elementalSkill;
    [SerializeField] private GameObject elementalBurst;

    //private PlayableCharacter character;

    void Start() {

    }


    void Update() {
        if (shouldMove) {
            HandleInput();
        }
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

        // MOVIMENTAÇÃO
        // Frente
        if (Input.GetKey(KeyCode.W)) {
            movement += Vector3.forward;
        }
        // Trás
        if (Input.GetKey(KeyCode.S)) {
            movement += Vector3.back;
            animator.SetBool("walkBack", true);
        } else {
            animator.SetBool("walkBack", false);
        }
        // Esquerda
        if (Input.GetKey(KeyCode.A)) {
            movement += Vector3.left;
        }
        // Direita
        if (Input.GetKey(KeyCode.D)) {
            movement += Vector3.right;
        }

        // Atacar
        if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded) {
            animator.SetTrigger("Attack");
        }

        // Pular
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            animator.SetTrigger("Jump");
        }

        // Skill
        if (Input.GetKeyDown(KeyCode.E) && isGrounded) {
            animator.SetTrigger("Skill");
        }
        
        // Burst
        if (Input.GetKeyDown(KeyCode.Q) && isGrounded) {
            animator.SetTrigger("Burst");
        }

        if (movement != Vector3.zero) {
            movement = movement.normalized * speed;
        }

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    public void InstantiateSkill() {
        Instantiate(elementalSkill, elementalAttackPosition.position, elementalAttackPosition.rotation);
    }

    public void InstantiateBurst() {
        Instantiate(elementalBurst, elementalAttackPosition.position, elementalAttackPosition.rotation);
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