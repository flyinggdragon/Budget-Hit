using System;

public static class Damage {
    public static float CalculateDamage(
            AttackType attackType,
            bool isReaction,
            Element? element,
            Element? reactingWith,
            int baseATK,
            float critRate,
            float critDMG,
            int proficiency
        ) {
        float partialDamage = 0f;

        float baseDMG = CalculateBaseDamage(baseATK);

        switch(attackType) {
            case AttackType.Physical:
                partialDamage = CalculatePhysicalDamage(baseDMG);
                break;
            
            case AttackType.ElementalSkill:
                if (isReaction && element.HasValue && reactingWith.HasValue) {
                    partialDamage = CalculateElementalReactionDamage(
                        baseDMG, element.Value, reactingWith.Value, proficiency
                    );
                } else {
                    partialDamage = CalculateElementalDamage(baseDMG);
                }
                break;
            
            case AttackType.Burst:
                if (isReaction && element.HasValue && reactingWith.HasValue) {
                    partialDamage = CalculateElementalReactionDamage(
                        baseDMG, element.Value, reactingWith.Value, proficiency
                    ) * 2;
                } else {
                    partialDamage = CalculateElementalDamage(baseDMG) * 2;
                }
                break;
        }

        if (Critical(critRate)) {
            return CalculateCriticalDamage(critDMG, partialDamage);
        } else {
            return partialDamage;
        }
    }

    public static float CalculateBaseDamage(int baseATK) {
        Random rng = new Random();
        return baseATK + rng.Next(1, 20);
    }

    public static float CalculatePhysicalDamage(float baseDMG) {
        return baseDMG * 10f;
    }

    public static float CalculateElementalDamage(float baseDMG) {
        return baseDMG * 15f;
    }

    // Corrigido para aceitar float em vez de int no parâmetro baseDMG
    public static float CalculateElementalReactionDamage(
            float baseDMG, // Alterado para float
            Element attackElement,
            Element reactingWith,
            int proficiency
        ) {
        float elementalDamage = CalculateElementalDamage(baseDMG);
        float proficiencyBonus = CalculateProficiencyBonus(proficiency);

        float subPartialDamage = elementalDamage * proficiencyBonus;
        float multiplier = attackElement == Element.Scotos ? 2.0f : 1.5f;

        return subPartialDamage * multiplier;
    }

    public static float CalculateProficiencyBonus(int proficiency) {
        return 1 + (proficiency / 1000f);
    }

    public static bool Critical(float critRate) {
        Random rng = new Random();
        int critRoll = rng.Next(0, 100);
        return critRoll < critRate;
    }

    // Corrigido para aceitar float no parâmetro partialDamage
    public static float CalculateCriticalDamage(float critDMG, float partialDamage) { // Alterado para float
        critDMG = partialDamage * (1 + (critDMG / 100));
        return (float)Math.Ceiling(critDMG);
    }
}