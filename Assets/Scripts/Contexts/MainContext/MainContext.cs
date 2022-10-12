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
                .Bind<MonsterView>()
                .To<MonsterMediator>();
            
            mediationBinder
                .Bind<InfrastructureView>()
                .To<InfrastructureMediator>();
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
        }
    
        private void BindCommands()
        {
            
        }

        private void BindModels()
        {
            injectionBinder
                .Bind<IHealthState>()
                .To<HealthState>()
                .ToSingleton();
        }

        private void BindServices()
        {
            injectionBinder
                .Bind<IHealthService>()
                .To<HealthService>()
                .ToSingleton();
        }
    }
}
