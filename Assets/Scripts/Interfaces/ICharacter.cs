public interface ICharacter {
    string name { get; }
    int level { get; }
    int exp { get; }
    int health { get; }
    int maxHP { get; }
    void Die();
}