using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : Enemy {
    public override int baseATK { get; protected set; } = 25;
    public override float critRate { get; protected set; } = 0.5f;
    public override float critDMG { get; protected set; } = 100.0f;
    public override float energyRecharge { get; protected set; } = 25.0f;
    public override int proficiency { get; protected set; } = 50;
    public override float attackCooldownTime { get; protected set; } = 6.0f;
    public override string name { get; protected set; } = "Mutant";
    public override int level { get; protected set; } = 0;
    public override int exp { get; protected set; } = 0;
    public override int health { get; protected set; } = 100;
    public override int maxHP { get; protected set; } = 100;

    private Coroutine attackCoroutine;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider _attackCollider;
    public override BoxCollider attackCollider {
        get { return _attackCollider; }
    }

    public override void EnableAttackCollision() {
        base.EnableAttackCollision();
    }

    public override void DisableAttackCollision() {
        base.DisableAttackCollision();
    }

    void Start() {
        if (attackCoroutine == null) {
            attackCoroutine = StartCoroutine(base.PeriodicAttack(attackCooldownTime));
        }
    }

    void Update() {
        Debug.Log("Vida do Mutante: " + health);
        if (health <= 0) {
            Die();
        }
    }

    public override void GetHit() {}

    public override void Die() {
        animator.SetTrigger("Die");
    }

    void OnDestroy() {
        StopAttackTimer();
    }

    public override void Attack() {
        animator.SetTrigger("Punch");
    }

    private new void StopAttackTimer() {
        if (attackCoroutine != null) {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    public void TakeDamage(int value) {
        health -= value;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Weapon")) {
            health -= 10;
            animator.SetTrigger("Hit");
        }

        if (other.CompareTag("ElementalBurst") || other.CompareTag("ElementalSkill")) {
            health -= 25;
            animator.SetTrigger("Hit");

            GameObject collider = other.gameObject;
            collider.GetComponent<BoxCollider>().enabled = false;
        }
    }
}