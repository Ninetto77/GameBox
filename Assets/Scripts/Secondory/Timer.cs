using System;
using System.Collections;
using UnityEngine;

namespace CommonTimer
{
	public class Timer
	{
		private float time;
		private float remainingTime;

		private IEnumerator countdown;

		private MonoBehaviour context;

		public event Action<float> HasBeenUpdate;
		public event Action TimeIsOver;

		public Timer(MonoBehaviour context)
		{
			this.context = context;
		}

		public void SetTimer(float targetTime)
		{
			this.time = targetTime;
			this.remainingTime = this.time;
		}

		public void StartCountingTime()
		{
			countdown = CountDown();
			context.StartCoroutine(countdown);
		}

		public void StopCountingTime()
		{
			if (countdown != null)
				context.StopCoroutine(countdown);
		}

		private IEnumerator CountDown()
		{
			while (remainingTime >= 0)
			{
				remainingTime -= Time.deltaTime;
				HasBeenUpdate?.Invoke(remainingTime / time);
				yield return null;
			}
			TimeIsOver?.Invoke();
		}
	}
}
