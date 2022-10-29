using UnityEngine;

namespace Contexts.MainContext
{
    public class MonsterMediator : ViewMediator<MonsterView>
    {
        [Inject] public IMonsterService MonsterService { get; set; }
        [Inject] public GameConfig GameConfig { get; set; }
        [Inject] public Controls Controls { get; set; }
        [Inject] public HandleTakingDamageSignal HandleTakingDamageSignal { get; set; }
        [Inject] public DestroySignal DestroySignal { get; set; }
        [Inject] public StartGameSignal StartGameSignal { get; set; }

        public override void OnRegister()
        {
            View.HitInfrastructureSignal.AddListener(OnInfrastructureHit);
            DestroySignal.AddListener(View.DestroyView);
            StartGameSignal.AddListener(View.SetProgressBarActive);
            
            View.SetData(GameConfig.GetMonsterConfig.MonsterData, Controls);
            
            MonsterService.AddMonster(out ushort id);
            View.SetID(id);
        
            View.StartMove();
        }

        public override void OnRemove()
        {
            View.HitInfrastructureSignal.RemoveListener(OnInfrastructureHit);
            DestroySignal.RemoveListener(View.DestroyView);
            StartGameSignal.RemoveListener(View.SetProgressBarActive);
            
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
