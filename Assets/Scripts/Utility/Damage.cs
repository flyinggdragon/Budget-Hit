using System;

public static class Damage {
    public static int CalculateDamage(AttackType attackType, Element? element, int baseATK, float critRate, float critDMG, int proficiency) {
        int partialDamage = 0;

        int baseDMG = CalculateBaseDamage(baseATK);
        
        switch(attackType) {
            case AttackType.Physical:
                partialDamage = CalculatePhysicalDamage(baseDMG);
                break;
            
            // Não está pronto.
            case AttackType.ElementalSkill:
                partialDamage = CalculateElementalDamage(baseDMG);
                break;
            
            case AttackType.Burst:
                partialDamage = CalculateElementalDamage(baseDMG) * 2;
                break;
        }

        if (Critical(critRate)) {
            return CalculateCriticalDamage(critDMG, partialDamage);
        } else {
            return partialDamage;
        }
    }

    public static int CalculateBaseDamage(int baseATK) {
        Random r = new Random();

        return baseATK + r.Next(1, 20);
    }

    public static int CalculatePhysicalDamage(int baseDMG) {
        return baseDMG * 10;
    }

    public static int CalculateElementalDamage(int baseDMG) {
        return baseDMG * 15;
    }

    public static int CalculateElementalReactionDamage(
            int baseDMG,
            Element attackElement,
            Element reactingWith,
            int proficiency
        ) {
        int elementalDamage = CalculateElementalDamage(baseDMG);
        float proficiencyBonus = CalculateProficiencyBonus(proficiency);

        float subPartialDamage = elementalDamage * proficiencyBonus;
        float multiplier = attackElement == Element.Scotos ? 2.0f : 1.5f;

        return (int)Math.Ceiling(subPartialDamage * multiplier);
    }

    public static float CalculateProficiencyBonus(int proficiency) {
        return 1 + (proficiency / 1000);
    }

    public static bool Critical(float critRate) {
        Random r = new Random();

        return (r.Next(0, 100)) > critRate;
    }

    public static int CalculateCriticalDamage(float critDMG, int partialDamage) {
        return (int)Math.Ceiling(partialDamage * (1 + (critDMG / 100)));
    }
}