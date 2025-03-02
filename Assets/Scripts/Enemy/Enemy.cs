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
    public abstract string enemyName { get; protected set; }
    public abstract float maxHealth { get; protected set; }
    public abstract int level { get; protected set; }
    public abstract int exp { get; protected set; }
    public Animator animator;
    public BoxCollider enemyCollision;
    public BoxCollider attackBox;
    public EnemyAttackBox enemyAttackBox;
    public HealthBar healthBar;
    public List<ElementalAttack> affectedBy { get; protected set; }

    private Coroutine attackCoroutine;
    public float health { get; protected set; }

    protected virtual void Start() {
        if (attackCoroutine == null) {
            attackCoroutine = StartCoroutine(PeriodicAttack(attackCooldownTime));
        }

        affectedBy = new List<ElementalAttack>();

        health = maxHealth;
        
        healthBar.maxHealth = maxHealth;
        healthBar.health = health;
    }

    public virtual void Update() {
        if (health <= 0f) {
            Die();
        }
    }

    public void EnableBoxCollision() {
        enemyCollision.enabled = true;
    }

    public void DisableBoxCollision() {
        enemyCollision.enabled = false;
    }

    public void EnableAttackCollision() {
        enemyCollision.enabled = true;
    }

    public void DisableAttackCollision() {
        enemyCollision.enabled = false;
    }

    public void DestroySelf(GameObject obj) {
        Destroy(obj);
    }
    
    public virtual void Attack() {
        animator.SetTrigger("Punch");
    }

    public virtual void Die() {
        animator.SetTrigger("Die");
        DisableAttackCollision();
        DisableBoxCollision();
    }
    
    public void HandleHit(Collider other) {
        other.GetComponent<Player>().GetHit(
            Damage.CalculateDamage(
                AttackType.Physical,
                false,
                null,
                null,
                baseATK,
                critRate,
                critDMG,
                proficiency
            ), other.GetComponent<Player>().characterElement
        );
    }

    public void GetHit(float damageSuffered, Element element) {
        // Here.
        health -= damageSuffered;
        healthBar.Damage(damageSuffered);
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

    protected virtual void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Weapon")) {
            animator.SetTrigger("Hit");
        }

        if (other.CompareTag("ElementalBurst") || other.CompareTag("ElementalSkill")) {
            animator.SetTrigger("Hit");

            ElementalAttack reactingWithEAttack = other.GetComponent<ElementalAttack>();

            if (affectedBy.Count > 0) {
                foreach (ElementalAttack eAttack in affectedBy) {
                    if (eAttack.element != reactingWithEAttack.element) {
                        reactingWithEAttack.HandleHit(this, true, eAttack.element);
                    }
                }
            } else {
                reactingWithEAttack.HandleHit(this, false, null);
            }

            affectedBy.Add(reactingWithEAttack);
            StartCoroutine(RemoveElementalAttackAfterLifetime(reactingWithEAttack));
        }
    }

    private IEnumerator RemoveElementalAttackAfterLifetime(ElementalAttack eAttack) {
        yield return new WaitForSeconds(6f);

        affectedBy.Remove(eAttack);
    }
}