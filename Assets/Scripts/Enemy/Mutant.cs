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
    public override float maxHealth { get; protected set; } = 20000f;
    public override float health { get; protected set; } = 20000f;
    public override List<ElementalAttack> affectedBy { get; protected set; }

    private Coroutine attackCoroutine;
    [SerializeField] private Animator _animator;
    public override Animator animator {
        get { return _animator; }
    }
    [SerializeField] private BoxCollider _boxCollider;
    public override BoxCollider boxCollider {
        get { return _boxCollider; }
    }
    [SerializeField] private BoxCollider _attackCollider;
    public override BoxCollider attackCollider {
        get { return _attackCollider; }
    }

    [SerializeField] private EnemyAttackBox _attackBox;
    public override EnemyAttackBox attackBox {
        get { return _attackBox; }
    }

    [SerializeField] private HealthBar _healthBar;
    public override HealthBar healthBar {
        get { return _healthBar; }
    }

    public void DestroySelf() {
        base.DestroySelf(gameObject);
    }

    void Start() {
        if (attackCoroutine == null) {
            attackCoroutine = StartCoroutine(base.PeriodicAttack(attackCooldownTime));
        }

        affectedBy = new List<ElementalAttack>();
        
        healthBar.maxHealth = maxHealth;
        healthBar.health = health;
    }

    void Update() {
        Debug.Log("Vida do Mutante: " + health);
        if (health <= 0f) {
            Die();
        }
    }

    public override void GetHit(float damageSuffered) {
        base.GetHit(damageSuffered);
        healthBar.Damage(damageSuffered);
    }

    public override void Die() {
        _animator.SetTrigger("Die");
        base.DisableAttackCollision();
        base.DisableBoxCollision();
    }

    void OnDestroy() {
        StopAttackTimer();
    }

    public override void Attack() {
        _animator.SetTrigger("Punch");
    }

    public override void HandleHit(Collider other) {
        base.HandleHit(other);
    }

    private new void StopAttackTimer() {
        if (attackCoroutine != null) {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Weapon")) {
            _animator.SetTrigger("Hit");
        }

        if (other.CompareTag("ElementalBurst") || other.CompareTag("ElementalSkill")) {
            _animator.SetTrigger("Hit");

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