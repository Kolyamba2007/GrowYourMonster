using UnityEngine;

namespace Contexts.MainContext
{
    public class MonsterMediator : ViewMediator<MonsterView>
    {
        [Inject] public IMonsterService MonsterService { get; set; }
        [Inject] public GameConfig GameConfig { get; set; }
        [Inject] public Controls Controls { get; set; }
        [Inject] public HandleTakingDamageSignal HandleTakingDamageSignal { get; set; }

        public override void OnRegister()
        {
            View.HitInfrastructureSignal.AddListener(OnInfrastructureHit);
            
            View.SetData(GameConfig.GetMonsterConfig.MonsterData, Controls);
            
            MonsterService.AddMonster(out ushort id);
            View.SetID(id);
        
            View.StartMove();
        }

        public override void OnRemove()
        {
            View.HitInfrastructureSignal.RemoveListener(OnInfrastructureHit);
            
            MonsterService.Remove(View.ID);
            
            View.StopAllCoroutines();
        }

        private void OnInfrastructureHit(Collider collider)
        {
            if (collider.TryGetComponent(out InfrastructureView infrastructureView))
                HandleTakingDamageSignal.Dispatch(View, infrastructureView);
        }
    }
}
