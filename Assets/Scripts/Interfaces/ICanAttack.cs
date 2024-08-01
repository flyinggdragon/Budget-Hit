public interface ICanAttack {
    int baseATK { get; }
    float critRate { get; }
    float critDMG { get; }
    float energyRecharge { get; }
    int proficiency { get; }
    void Attack();
}