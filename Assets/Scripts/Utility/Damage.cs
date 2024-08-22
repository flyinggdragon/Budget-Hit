using System;

public static class Damage {
    public static int CalculateDamage(
            AttackType attackType,
            bool isReaction,
            Element? element,
            Element? reactingWith,
            int baseATK,
            float critRate,
            float critDMG,
            int proficiency
        ) {
        int partialDamage = 0;

        int baseDMG = CalculateBaseDamage(baseATK);

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

    public static int CalculateBaseDamage(int baseATK) {
        Random rng = new Random();
        return baseATK + rng.Next(1, 20);
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
        return 1 + (proficiency / 1000f);
    }

    public static bool Critical(float critRate) {
        Random rng = new Random();
        int critRoll = rng.Next(0, 100);
        return critRoll < critRate;
    }

    public static int CalculateCriticalDamage(float critDMG, int partialDamage) {
        critDMG = partialDamage * (1 + (critDMG / 100));
        return (int)Math.Ceiling(critDMG);
    }
}