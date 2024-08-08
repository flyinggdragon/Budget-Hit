using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalAttack : MonoBehaviour {
    [SerializeField] public Element element;
    [SerializeField] public bool isBurst;
    [SerializeField] public BoxCollider boxCollider;
    private Player player;

    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void HandleHit(Collider other) {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy) {
            if (!isBurst) {
                enemy.GetHit(
                    Damage.CalculateDamage(
                        AttackType.ElementalSkill,
                        element,
                        player.baseATK,
                        player.critRate,
                        player.critDMG,
                        player.proficiency
                    )
                ); 
            } else {
                enemy.GetHit(
                    Damage.CalculateDamage(
                        AttackType.Burst,
                        element,
                        player.baseATK,
                        player.critRate,
                        player.critDMG,
                        player.proficiency
                    )
                );
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            HandleHit(other);
        }
    }
}