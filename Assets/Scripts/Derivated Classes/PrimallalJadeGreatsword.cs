using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimallalJadeGreatsword : Weapon {
    public override string name { get; protected set; } = "Primallal Jade Greatsword";
    public override int baseATK { get; protected set; } = 100;
    [SerializeField] private BoxCollider _boxCollider;
    public override BoxCollider boxCollider {
        get { return _boxCollider; }
        protected set { _boxCollider = value; }
    }

    public override void EnableCollision() {
        base.EnableCollision();
    }

    public override void DisableCollision() {
        base.DisableCollision();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            base.HandleHit(other);
        }
    }
}