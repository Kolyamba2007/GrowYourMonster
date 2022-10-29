namespace Contexts.MainContext
{
    public class CameraMediator : ViewMediator<CameraView>
    {
        [Inject] public StartGameSignal StartGameSignal { get; set; }
        [Inject] public StartCameraTransitionSignal StartCameraTransitionSignal { get; set; }
        [Inject] public DestroySignal DestroySignal { get; set; }
        
        public override void OnRegister()
        {
            DestroySignal.AddListener(View.DestroyView);
            StartCameraTransitionSignal.AddListener(View.StartTransition);
            View.ReachedFinalPositionSignal.AddListener(StartGameSignal.Dispatch);
        }

        public override void OnRemove()
        {
            DestroySignal.RemoveListener(View.DestroyView);
            StartCameraTransitionSignal.RemoveListener(View.StartTransition);
            View.ReachedFinalPositionSignal.RemoveListener(StartGameSignal.Dispatch);
        }
    }
}