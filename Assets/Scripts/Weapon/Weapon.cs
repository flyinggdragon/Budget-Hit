using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public abstract string weaponName { get; protected set; }
    public abstract int baseATK { get; protected set; }
    [SerializeField] public BoxCollider boxCollider;
    [SerializeField] public Player player;

    protected virtual void Start() { }

    public void EnableCollision() {
        boxCollider.enabled = true;
    }

    public void DisableCollision() {
        boxCollider.enabled = false;
    }

    public void HandleHit(Collider other) {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy) {
            float damage = Damage.CalculateDamage(
                AttackType.Physical,
                false,
                null,
                null,
                player.baseATK,
                player.critRate,
                player.critDMG,
                player.proficiency
            );
        
            enemy.GetHit(damage, Element.Physical);
        }
    }

    protected virtual void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            HandleHit(other);
        }
    }
}