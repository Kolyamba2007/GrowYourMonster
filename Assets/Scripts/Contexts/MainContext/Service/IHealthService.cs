namespace Contexts.MainContext
{
    public interface IHealthService
    {
        void AddElement(IHealth healthData, out ushort id);

        void Remove(ushort id);

        void SetDamage(ushort id, int damage);

        void SetDamage(ushort id, int damage, out int remainingHealth);
    }
}
