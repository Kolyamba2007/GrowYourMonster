using strange.extensions.command.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class HandleTakingDamageCommand : Command
    {
        [Inject] public IHealthService HealthService { get; set; }
        [Inject] public IMonsterService MonsterService { get; set; }
        [Inject] public MonsterView MonsterView { get; set; }
        [Inject] public InfrastructureView InfrastructureView { get; set; }

        public override void Execute()
        {
            HealthService.SetDamage(InfrastructureView.ID,
                MonsterService.GetScore(MonsterView.ID) + MonsterView.MonsterData.AttackDamage,
                out int remainingHealth);

            if (remainingHealth != 0)
            {
                InfrastructureView.Shake();
                InfrastructureView.EnableHealthBar();

                var healthRatio = (float) remainingHealth / InfrastructureView.InfrastructureData.Health;
                InfrastructureView.UpdateHealthBar(healthRatio);
            }
            else
            {
                HealthService.Remove(InfrastructureView.ID);

                MonsterView.FinishAttack(InfrastructureView.GetComponent<Collider>());
                InfrastructureView.EnableDivide();

                MonsterService.RaiseScore(MonsterView.ID, InfrastructureView.InfrastructureData.Points);

                var score = MonsterService.GetScore(MonsterView.ID);
                MonsterView.UpdateScore(score);

                var monsterData = MonsterView.MonsterData;
                var scoreRatio = (float) score / monsterData.GrowthPointLimit;
                if (score <= monsterData.GrowthPointLimit)
                {
                    float scale = 1 + score / (monsterData.GrowthPointLimit / (monsterData.MaxScale - 1));

                    MonsterView.GrowUp(scale, 100 - scoreRatio * 100, scoreRatio);

                    var speed = (MonsterView.MonsterData.EndSpeed - MonsterView.MonsterData.StartSpeed) * scoreRatio +
                                MonsterView.MonsterData.StartSpeed;

                    MonsterService.ChangeSpeed(MonsterView.ID, speed);
                }
                else if (MonsterView.transform.localScale.x < monsterData.MaxScale)
                {
                    MonsterView.GrowUp(monsterData.MaxScale, 0, 1);
                    MonsterService.ChangeSpeed(MonsterView.ID, MonsterView.MonsterData.EndSpeed);
                }
            }
        }
    }
}
