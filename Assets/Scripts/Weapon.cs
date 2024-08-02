using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public abstract string name { get; }
    public abstract int baseATK { get; }
    public abstract BoxCollider boxCollider { get; }

    public virtual void EnableCollision() {
        boxCollider.enabled = true;
    }

    public virtual void DisableCollision() {
        boxCollider.enabled = false;
    }
}