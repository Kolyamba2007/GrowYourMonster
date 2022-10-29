namespace Contexts.MainContext
{
    public class SnapScrollingMediator : ViewMediator<SnapScrollingView>
    {
        [Inject] public DestroySignal DestroySignal { get; set; }
        
        public override void OnRegister()
        {
            View.DestroyPreviousSignal.AddListener(DestroySignal.Dispatch);
        }

        public override void OnRemove()
        {
            View.DestroyPreviousSignal.RemoveAllListeners();
        }
    }
}