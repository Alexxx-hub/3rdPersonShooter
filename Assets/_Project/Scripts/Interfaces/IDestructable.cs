namespace _Project.Scripts.Interfaces
{
    public interface IDestructable
    {
        public float MaxHealth { get; }
        public float CurrentHealth { get; set; }
        
        public void TakeDamage(float damage);
        public void Die();
    }
}