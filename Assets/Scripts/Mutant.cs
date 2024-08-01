using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : Enemy {
    public override string enemyName { get; protected set; } = "Mutant";
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

    private Coroutine attackCoroutine;

    void Start() {
        if (attackCoroutine == null) {
            attackCoroutine = StartCoroutine(base.PeriodicAttack(attackCooldownTime));
        }
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
}