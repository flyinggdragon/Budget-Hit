using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimallalJadeGreatsword : Weapon {
    public override string name { get; } = "Primallal Jade Greatsword";
    public override int baseATK { get; } = 100;
    [SerializeField] private BoxCollider _boxCollider;
    public override BoxCollider boxCollider {
        get { return _boxCollider; }
    }

    public override void EnableCollision() {
        base.EnableCollision();
    }

    public override void DisableCollision() {
        base.DisableCollision();
    }
}