using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IAttack, ICharacter, IDamageable {
    public abstract int baseATK { get; protected set; }
    public abstract float critRate { get; protected set; }
    public abstract float critDMG { get; protected set; }
    public abstract float energyRecharge { get; protected set; }
    public abstract int proficiency { get; protected set; }
    public abstract float attackCooldownTime { get; protected set; }
    public abstract Animator animator { get; protected set; }
    public abstract void Attack();
    public abstract string name { get; protected set; }
    public abstract int level { get; protected set; }
    public abstract int exp { get; protected set; }
    public abstract int health { get; protected set; }
    public abstract int maxHP { get; protected set; }
    public abstract void Die();
    public abstract void GetHit();

    public void StartAttackTimer() {
        StartCoroutine(PeriodicAttack(attackCooldownTime));
    }

    public void StopAttackTimer() {
        StopCoroutine(PeriodicAttack(attackCooldownTime));
    }

    public IEnumerator PeriodicAttack(float time) {
        while (true) {
            Attack();
            yield return new WaitForSeconds(time);
        }
    }
}