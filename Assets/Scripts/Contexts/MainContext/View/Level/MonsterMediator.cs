using strange.extensions.mediation.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class MonsterMediator<T> : Mediator
    {
        [Inject] public T View { get; set; }
        [Inject] public IMonsterService MonsterService { get; set; }
        [Inject] public GameConfig GameConfig { get; set; }
        [Inject] public HandleTakingDamageSignal HandleTakingDamageSignal { get; set; }
        [Inject] public DestroySignal DestroySignal { get; set; }
        [Inject] public StartGameSignal StartGameSignal { get; set; }

        private MonsterView _monsterView;

        public override void OnRegister()
        {
            _monsterView = (View as MonsterView)!;
            
            _monsterView.HitInfrastructureSignal.AddListener(OnInfrastructureHit);
            _monsterView.HitEnemySignal.AddListener(OnEnemyHit);
            StartGameSignal.AddListener(_monsterView.SetProgressBarActive);
            StartGameSignal.AddListener(_monsterView.StartMove);
            
            MonsterService.AddMonster(out ushort id);
            _monsterView.SetID(id);
        }

        public override void OnRemove()
        {
            _monsterView.HitInfrastructureSignal.RemoveListener(OnInfrastructureHit);
            _monsterView.HitEnemySignal.RemoveListener(OnEnemyHit);
            StartGameSignal.RemoveListener(_monsterView.SetProgressBarActive);
            StartGameSignal.RemoveListener(_monsterView.StartMove);
            
            MonsterService.Remove(_monsterView.ID);
        }

        private void OnInfrastructureHit(Collider collider)
        {
            if (collider.TryGetComponent(out InfrastructureView infrastructureView))
                HandleTakingDamageSignal.Dispatch((View as MonsterView)!, infrastructureView);
        }

        private void OnEnemyHit(Collider collider)
        {
            if (collider.TryGetComponent(out MonsterView enemyView))
            {
                var enemyScore = MonsterService.GetScore(enemyView.ID);
                
                if (MonsterService.GetScore(_monsterView.ID) > enemyScore)
                {
                    MonsterService.RaiseScore(_monsterView.ID, enemyScore);
                    _monsterView.UpdateScore(MonsterService.GetScore(_monsterView.ID));
                    enemyView.DestroyView();
                }
            }
        }
    }
}
