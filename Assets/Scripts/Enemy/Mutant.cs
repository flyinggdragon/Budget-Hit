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
    public override string enemyName { get; protected set; } = "Mutant";
    public override float maxHealth { get; protected set; } = 20000f;
    public override int level { get; protected set; } = 10;
    public override int exp { get; protected set; } = 0;
}