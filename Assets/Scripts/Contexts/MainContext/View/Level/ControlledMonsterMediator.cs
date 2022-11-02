using UnityEngine;

namespace Contexts.MainContext
{
    public class ControlledMonsterMediator : MonsterMediator<ControlledMonsterView>
    {
        [Inject] public Controls Controls { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            
            View.MoveToSignal.AddListener(OnMove);
            DestroySignal.AddListener(View.DestroyView);
            
            View.SetData(GameConfig.GetMonsterConfig.MonsterData, Controls);
            MonsterService.ChangeSpeed(View.ID, View.MonsterData.StartSpeed);
        }
        
        public override void OnRemove()
        {
            base.OnRemove();
            
            View.MoveToSignal.RemoveListener(OnMove);
            DestroySignal.RemoveListener(View.DestroyView);
        }

        private void OnMove(Vector3 direction)
        {
            View.MoveTo(direction, MonsterState.Speed[View.ID]);
            Debug.Log(MonsterState.Speed[View.ID]);
        }
    }
}
