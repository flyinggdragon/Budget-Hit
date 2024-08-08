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
    public abstract string name { get; protected set; }
    public abstract int level { get; protected set; }
    public abstract int exp { get; protected set; }
    public abstract int health { get; protected set; }
    public abstract int maxHP { get; protected set; }
    public abstract BoxCollider boxCollider { get; }
    public abstract BoxCollider attackCollider { get; }

    public virtual void EnableBoxCollision() {
        boxCollider.enabled = true;
    }

    public virtual void DisableBoxCollision() {
        boxCollider.enabled = false;
    }

    public void EnableAttackCollision() {
        attackCollider.enabled = true;
    }

    public void DisableAttackCollision() {
        attackCollider.enabled = false;
    }

    public void DestroySelf(GameObject obj) {
        Destroy(obj);
    }
    
    public abstract void Attack();
    public abstract void Die();
    public virtual void GetHit(int damageSuffered) {
        health -= damageSuffered;
    }

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