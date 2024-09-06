public interface ICharacter {
    string name { get; }
    int level { get; }
    int exp { get; }
    float maxHealth { get; }
    float health { get; }
    void Die();
}