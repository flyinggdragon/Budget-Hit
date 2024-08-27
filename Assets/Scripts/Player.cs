using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {
    public int baseATK { get; private set; } = 50;
    public float critRate { get; private set; } = 50.0f;
    public float critDMG { get; private set; } = 100.0f;
    public int proficiency { get; private set; } = 100;
    private float speed = 5.0f;
    private float sprintSpeed = 10.0f;
    public int health { get; private set; } = 1000;
    private float stamina = 200;
    private bool isGrounded = true;
    private bool shouldMove = true;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public Transform elementalAttackPosition;

    [SerializeField] private GameObject elementalSkill;
    [SerializeField] private GameObject elementalBurst;
    private Element characterElement = Element.Electro;

    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform cameraTransform;

    void Start() {
        DisableWeaponCollision();
    }

    void Update() {
        if (shouldMove) {
            HandleInput();
        }

        Debug.Log("Vida do Player: " + health);

        if (health <= 0) {
            animator.SetTrigger("Die");
        }
    }

    private void HandleInput() {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0.0f) {
            speed = sprintSpeed;
            stamina -= 1.0f;
        } else {
            speed = 5.0f;

            if (stamina < 200.0f) {
                stamina += 0.4f;
            }
        }

        // MOVIMENTAÇÃO
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        if (Input.GetKey(KeyCode.W)) {
            movement += forward;
        }
        if (Input.GetKey(KeyCode.S)) {
            movement -= forward;
            animator.SetBool("walkBack", true);
        } else {
            animator.SetBool("walkBack", false);
        }
        if (Input.GetKey(KeyCode.A)) {
            movement -= right;
        }
        if (Input.GetKey(KeyCode.D)) {
            movement += right;
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

        if (movement != Vector3.zero) {
            transform.forward = movement; // Faz o personagem olhar na direção do movimento
        }
    }

    public void GetHit(int damageSuffered) {
        health -= damageSuffered;
    }

    public void DestroySelf() {
        Destroy(transform.GetChild(0).gameObject);
        Destroy(transform.GetChild(1).gameObject);
        Destroy(elementalAttackPosition.gameObject);
    }

    public void InstantiateSkill() {
        Instantiate(elementalSkill, elementalAttackPosition.position, elementalAttackPosition.rotation);
    }

    public void InstantiateBurst() {
        Instantiate(elementalBurst, elementalAttackPosition.position, elementalAttackPosition.rotation);
    }

    public void EnableWeaponCollision() {
        weapon.EnableCollision();
    }

    public void DisableWeaponCollision() {
        weapon.DisableCollision();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("EnemyAttack")) {
            animator.SetTrigger("Hit");
        }
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