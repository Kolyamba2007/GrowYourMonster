namespace Contexts.MainContext
{
    public class ControlledMonsterMediator : MonsterMediator<ControlledMonsterView>
    {
        [Inject] public Controls Controls { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            
            DestroySignal.AddListener(View.DestroyView);
            
            View.SetData(GameConfig.GetMonsterConfig.MonsterData, Controls);
        }
        
        public override void OnRemove()
        {
            base.OnRemove();
            
            DestroySignal.RemoveListener(View.DestroyView);
            
            View.SetData(GameConfig.GetMonsterConfig.MonsterData, Controls);
        }
    }
}
