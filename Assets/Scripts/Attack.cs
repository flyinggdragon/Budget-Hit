/*public class Attack() {
    private AttackType attackType;
    private Element? element;
    private int damage;

    public Attack(AttackType type, Element attackElement, int baseATK, float critRate, float critDMG, int proficiency) {
        attackType = type;
        element = attackElement;

        damage = Damage.CalculateDamage(
            AttackType attackType,
            Element element
            int baseATK, 
            float critRate, 
            float critDMG, 
            int proficiency
        );
    }
}



- type: AttackType
- element: Element
+ damage: int


+ Attack(Attack type, Element element, int baseATK, float critRate, float critDMG, int proficiency)
+ CalculateDamage(int baseATK, float critRate, float critDMG, int proficiency)

*/