using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public abstract string name { get; protected set; }
    public abstract int baseATK { get; protected set; }
    public abstract BoxCollider boxCollider { get; protected set; }
    [SerializeField] public Player player;

    public virtual void EnableCollision() {
        boxCollider.enabled = true;
    }

    public virtual void DisableCollision() {
        boxCollider.enabled = false;
    }

    public void HandleHit(Collider other) {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy) {
            enemy.GetHit(
                Damage.CalculateDamage(
                    AttackType.Physical,
                    null,
                    player.baseATK,
                    player.critRate,
                    player.critDMG,
                    player.proficiency
                )
            );  
        }
    }
}