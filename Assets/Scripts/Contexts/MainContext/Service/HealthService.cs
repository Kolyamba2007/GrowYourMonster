using System.Linq;

namespace Contexts.MainContext
{
    public class HealthService : IHealthService
    {
        [Inject] public IHealthState HealthState { get; set; }

        public void AddElement(IHealth healthData, out ushort id)
        {
            id = GetID();
            
            HealthState.Health.Add(id, healthData.Health);
        }

        public void Remove(ushort id)
        {
            if (HealthState.Health.TryGetValue(id, out int _))
                HealthState.Health.Remove(id);
        }

        public void SetDamage(ushort id, int damage)
        {
            if (HealthState.Health.TryGetValue(id, out int health))
                health = HealthState.Health[id] -= damage;

            if (health < 0) HealthState.Health[id] = 0;
        }

        public void SetDamage(ushort id, int damage, out int remainingHealth)
        {
            if (HealthState.Health.TryGetValue(id, out int health))
                health = HealthState.Health[id] -= damage;

            remainingHealth = health > 0 ? health : 0;
        }

        private ushort GetID()
        {
            var id = HealthState.Health.Keys;

            if (id.Count != 0)
                return (ushort) (id.Max() + 1);
            else
                return 0;
        }
    }
}
