using UnityEngine;

namespace Contexts.MainContext
{
    public class MonsterMediator : ViewMediator<MonsterView>
    {
        [Inject] public IHealthService HealthService { get; set; }
        [Inject] public GameConfig GameConfig { get; set; }
        [Inject] public Controls Controls { get; set; }

        public override void OnRegister()
        {
            View.HitInfrastructureSignal.AddListener(OnInfrastructureHit);
            
            View.SetData(GameConfig.GetMonsterConfig.MonsterData, Controls);
        
            View.StartMove();
            View.StartInfrastructureDetect();
            View.StartAttack();
        }

        public override void OnRemove()
        {
            View.HitInfrastructureSignal.RemoveListener(OnInfrastructureHit);
            
            View.StopAllCoroutines();
        }

        private void OnInfrastructureHit(Collider collider)
        {
            if (collider.TryGetComponent(out InfrastructureView infrastructureView))
            {
                HealthService.SetDamage(infrastructureView.ID, View.MonsterData.AttackDamage, out int remainingHealth);

                if (remainingHealth != 0)
                    Debug.Log(remainingHealth);//unitView.UpdateHealthBar(remainingHealth);
                else
                {
                    HealthService.Remove(infrastructureView.ID);
                    infrastructureView.SetDivideActive(true);
                }
            }
        }
    }
}
