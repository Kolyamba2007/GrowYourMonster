namespace Contexts.MainContext
{
    public interface IMonsterService
    {
        void AddMonster(out ushort id);
        void Remove(ushort id);
        void RaiseScore(ushort id, int point);
        int GetScore(ushort id);
        void ChangeSpeed(ushort id, float speed);
    }
}
