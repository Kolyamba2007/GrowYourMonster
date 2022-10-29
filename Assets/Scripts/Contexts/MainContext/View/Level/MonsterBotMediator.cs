namespace Contexts.MainContext
{
    public class MonsterBotMediator : MonsterMediator<MonsterBotView>
    {
        public override void OnRegister()
        {
            base.OnRegister();
            
            View.SetData(GameConfig.GetMonsterConfig.MonsterData);
        }
    }
}
