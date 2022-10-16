namespace Contexts.MainContext
{
    public class InfrastructureMediator : ViewMediator<InfrastructureView>
    {
        [Inject] public IHealthService HealthService { get; set; }
        [Inject] public GameConfig GameConfig { get; set; }
        
        public override void OnRegister()
        {
            View.SetData(GameConfig.GetInfrastructureConfig.InfrastructureData[View.Type]);
            
            HealthService.AddElement(View.InfrastructureData, out ushort id);
            View.SetID(id);
        }

        public override void OnRemove()
        {
            
        }
    }
}
