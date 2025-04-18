using System;
using System.Collections;
using AI.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class Timer : ITimer
    {
        public event Action OnStarted;

        public event Action OnTimeChanged;

        public event Action OnCanceled;

        public event Action OnFinished;

        public event Action OnReset;

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-10)]
        [PropertySpace(8)]
        public bool IsPlaying { get; private set; }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-9)]
        [ProgressBar(0, 1)]
        public float Progress
        {
            get { return this.currentTime / this.duration; }
            set { this.SetProgress(value); }
        }

        public float Duration
        {
            get { return this.duration; }
            set { this.duration = value; }
        }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-8)]
        public float CurrentTime
        {
            get { return this.currentTime; }
            set { this.currentTime = Mathf.Clamp(value, 0, this.duration); }
        }

        [Space]
        [SerializeField]
        private float duration;

        private float currentTime;

        private Coroutine coroutine;

        public Timer()
        {
        }

        public Timer(float duration)
        {
            this.duration = duration;
            this.currentTime = 0;
        }

        public void Play()
        {
            if (this.IsPlaying)
            {
                return;
            }

            this.IsPlaying = true;
            this.OnStarted?.Invoke();
            this.coroutine = MonoHelper.Instance.StartCoroutine(this.TimerRoutine());
        }

        public void Stop()
        {
            if (this.coroutine != null)
            {
                MonoHelper.Instance.StopCoroutine(this.coroutine);
            }

            if (this.IsPlaying)
            {
                this.IsPlaying = false;
                this.OnCanceled?.Invoke();
            }
        }

        public void ResetTime()
        {
            this.currentTime = 0;
            this.OnReset?.Invoke();
        }

        private void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            this.currentTime = this.duration * progress;
            this.OnTimeChanged?.Invoke();
        }

        private IEnumerator TimerRoutine()
        {
            while (this.currentTime < this.duration)
            {
                yield return null;
                this.currentTime += Time.deltaTime;
                this.OnTimeChanged?.Invoke();
            }

            this.IsPlaying = false;
            this.OnFinished?.Invoke();
        }
    }
}