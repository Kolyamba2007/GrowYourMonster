using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class MainContext : MVCSContext
    {
        public MainContext(MonoBehaviour view) : base(view)
        {
        }

        public MainContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();

            injectionBinder
                .Unbind<ICommandBinder>();

            injectionBinder
                .Bind<ICommandBinder>()
                .To<SignalCommandBinder>()
                .ToSingleton();
        }

        protected override void mapBindings()
        {
            injectionBinder
                .Bind<GameConfig>()
                .To(GameConfig.Load())
                .ToSingleton();
        
            injectionBinder
                .Bind<Controls>()
                .To(new Controls())
                .ToSingleton();
        
            BindSignals();
            BindModels();
            BindViews();
            BindCommands();
            BindServices();
        }
    
        private void BindViews()
        {
            mediationBinder
                .Bind<JoystickView>()
                .To<JoystickMediator>();
            
            mediationBinder
                .Bind<ControlledMonsterView>()
                .To<ControlledMonsterMediator>();
            
            mediationBinder
                .Bind<MonsterBotView>()
                .To<MonsterBotMediator>();
            
            mediationBinder
                .Bind<InfrastructureView>()
                .To<InfrastructureMediator>();
            
            mediationBinder
                .Bind<SnapScrollingView>()
                .To<SnapScrollingMediator>();
            
            mediationBinder
                .Bind<CameraView>()
                .To<CameraMediator>();
            
            mediationBinder
                .Bind<PlayButtonView>()
                .To<PlayButtonMediator>();
            
            mediationBinder
                .Bind<PauseButtonView>()
                .To<PauseButtonMediator>();
            
            mediationBinder
                .Bind<PausePanelView>()
                .To<PausePanelMediator>();
        }
    
        private void BindSignals()
        {
            injectionBinder
                .Bind<StartGameSignal>()
                .ToSingleton();
        
            injectionBinder
                .Bind<PauseGameSignal>()
                .ToSingleton();
        
            injectionBinder
                .Bind<ContinueGameSignal>()
                .ToSingleton();
        
            injectionBinder
                .Bind<EndGameSignal>()
                .ToSingleton();
            
            injectionBinder
                .Bind<HandleTakingDamageSignal>()
                .ToSingleton();
            
            injectionBinder
                .Bind<ChangeTransformSignal>()
                .ToSingleton();
            
            injectionBinder
                .Bind<DestroySignal>()
                .ToSingleton();
            
            injectionBinder
                .Bind<StartCameraTransitionSignal>()
                .ToSingleton();
            
            injectionBinder
                .Bind<SetTimeScaleSignal>()
                .ToSingleton();
            
            injectionBinder
                .Bind<SpawnedNewMonsterSignal>()
                .ToSingleton();
        }
    
        private void BindCommands()
        {
            commandBinder
                .Bind<HandleTakingDamageSignal>()
                .To<HandleTakingDamageCommand>();
            
            commandBinder
                .Bind<SetTimeScaleSignal>()
                .To<SetTimeScaleCommand>();
            
            commandBinder
                .Bind<ReloadSceneSignal>()
                .To<ReloadSceneCommand>();
        }

        private void BindModels()
        {
            injectionBinder
                .Bind<IHealthState>()
                .To<HealthState>()
                .ToSingleton();
            
            injectionBinder
                .Bind<IMonsterState>()
                .To<MonsterState>()
                .ToSingleton();
        }

        private void BindServices()
        {
            injectionBinder
                .Bind<IHealthService>()
                .To<HealthService>()
                .ToSingleton();
            
            injectionBinder
                .Bind<IMonsterService>()
                .To<MonsterService>()
                .ToSingleton();
        }
    }
}
