using System;

namespace ID.Runtime.Utilities
{
    public class Timer
    {
        protected float duration;
        protected float remainingTime;
        protected bool isRunning;

        public float RemainingTime => remainingTime;
        public float Duration
        {
            get => duration;
            set => duration = value;
        }

        public bool IsRunning => isRunning;
        
        public Action<Timer> OnStart;
        public Action<Timer> OnUpdate;
        public Action<Timer> OnEnd;

        public Timer(float duration, Action<Timer> onStart = null, Action<Timer> onEnd = null, Action<Timer> onUpdate = null)
        {
            this.duration = duration;
            OnStart = onStart;
            OnUpdate = onUpdate;
            OnEnd = onEnd;
        }

        public virtual void Start()
        {
            isRunning = true;
            remainingTime = 0;
            OnStart?.Invoke(this);
        }

        public void TogglePause()
        {
            isRunning = !isRunning;
            if (isRunning)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }

        public void Pause()
        {
            isRunning = true;
        }

        public void Unpause()
        {
            isRunning = false;
        }

        public virtual void Update(float deltaTime)
        {
            if (isRunning == false) return;
            
            remainingTime += deltaTime;
            
            if (remainingTime >= duration)
            {
                End();
            }
            OnUpdate?.Invoke(this);
        }

        public virtual void End()
        {
            remainingTime = duration;
            isRunning = false;
            OnEnd?.Invoke(this);
        }
    } 
}

