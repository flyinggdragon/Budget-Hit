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
    [SerializeField] private Animator _animator;
    public override Animator animator {
        get { return _animator; }
        protected set { _animator = value; }
    }
    public override string name { get; protected set; } = "Mutant";
    public override int level { get; protected set; } = 0;
    public override int exp { get; protected set; } = 0;
    public override int health { get; protected set; } = 100;
    public override int maxHP { get; protected set; } = 100;

    private Coroutine attackCoroutine;

    void Start() {
        if (attackCoroutine == null) {
            attackCoroutine = StartCoroutine(base.PeriodicAttack(attackCooldownTime));
        }
    }

    void Update() {
        Debug.Log("Vida do Mutante: " + health);
        if (health < 0) {
            Die();
        }
    }

    public override void GetHit() {}

    public override void Die() {
        _animator.SetTrigger("Die");
    }

    void OnDestroy() {
        StopAttackTimer();
    }

    public override void Attack() {
        _animator.SetTrigger("Punch");
    }

    private new void StopAttackTimer() {
        if (attackCoroutine != null) {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Weapon")) {
            health -= 10;
        }
    }
}