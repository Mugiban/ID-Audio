using System;

namespace ID.Runtime.Utilities
{
    public class InvertedTimer : Timer
    {
        public InvertedTimer(float duration, Action<Timer> onStart = null, Action<Timer> onUpdate = null, Action<Timer> onEnd = null) : base(
            duration, onStart, onUpdate, onEnd)
        {
            
        }
        public override void Start()
        {
            isRunning = true;
            remainingTime = duration;
            OnStart?.Invoke(this);
        }
        public override void Update(float deltaTime)
        {
            if (isRunning == false) return;
            
            remainingTime -= deltaTime;
            
            if (remainingTime <= 0)
            {
                End();
            }
            OnUpdate?.Invoke(this);
        }
        
        public override void End()
        {
            remainingTime = 0;
            isRunning = false;
            OnEnd?.Invoke(this);
        }
    } 
}