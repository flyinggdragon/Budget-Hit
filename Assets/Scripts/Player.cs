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
    [SerializeField] Transform cam;

    void Start() {
        DisableWeaponCollision();
    }

    void Update() {
        if (shouldMove) {
            HandleMovement();
        }

        if (health <= 0) {
            animator.SetTrigger("Die");
        }
    }

    private void HandleMovement() {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0.0f) {
            speed = sprintSpeed;
            stamina -= 1.0f;
        } else {
            speed = 5.0f;
            if (stamina < 200.0f) {
                stamina += 0.4f;
            }
        }

        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = cam.forward * vInput + cam.right * hInput;
        moveDirection.y = 0;

        if (moveDirection != Vector3.zero) {
            rb.velocity = moveDirection.normalized * speed + Vector3.up * rb.velocity.y;
            transform.forward = moveDirection;
        } else {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        HandleActions();
    }

    private void HandleActions() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded) {
            animator.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            animator.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.E) && isGrounded) {
            animator.SetTrigger("Skill");
        }

        if (Input.GetKeyDown(KeyCode.Q) && isGrounded) {
            animator.SetTrigger("Burst");
        }
    }

    public void GetHit(int damageSuffered) {
        health -= damageSuffered;
    }

    public void DestroySelf() {
        Destroy(gameObject);
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