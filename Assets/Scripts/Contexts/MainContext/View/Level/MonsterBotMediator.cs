using UnityEngine.AI;

namespace Contexts.MainContext
{
    public class MonsterBotMediator : MonsterMediator<MonsterBotView>
    {
        public override void OnRegister()
        {
            base.OnRegister();
            
            View.UpdateSpeedSignal.AddListener(OnSpeedUpdate);
            StartGameSignal.AddListener(View.UpdateSpeed);
            StartGameSignal.AddListener(View.EnablePointer);
            SpawnedNewMonsterSignal.AddListener(View.UpdateData);
            
            View.SetData(GameConfig.GetMonsterConfig.MonsterData);
            MonsterService.ChangeSpeed(View.ID, View.MonsterData.StartSpeed);
        }
        
        public override void OnRemove()
        {
            base.OnRemove();
            
            View.UpdateSpeedSignal.RemoveListener(OnSpeedUpdate);
            StartGameSignal.RemoveListener(View.UpdateSpeed);
            StartGameSignal.RemoveListener(View.EnablePointer);
            SpawnedNewMonsterSignal.AddListener(View.UpdateData);
        }

        private void OnSpeedUpdate(NavMeshAgent navMeshAgent)
        {
            navMeshAgent.speed = MonsterState.Speed[View.ID];
        }
    }
}
