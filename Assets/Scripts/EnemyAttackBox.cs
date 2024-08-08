using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : MonoBehaviour {
    [SerializeField] public Enemy parentEnemy;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            parentEnemy.HandleHit(other);
        }
    }
}