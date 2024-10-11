using Cache;
using Code.Global.Animations;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Points
{
	public class Candy : MonoCache
	{
		public int Points;
		public PunchAnimationPreset presetPunch;

		[Inject] private ShopPoint shop;
		private Point candyPoint;

		private void Start()
		{
			candyPoint = new Point(Points);
		}

		public override void OnTick()
		{
			AnimationShortCuts.PunchRotationAnimation(transform, presetPunch);
			//AnimationShortCuts.PunchPositionAnimation(transform, presetPunch).SetLoops(-1);
		}

		private void OnTriggerEnter(Collider other)
		{
			shop.AddPoints(candyPoint);
			Destroy(this.gameObject, 0.5f);
		}
	}
}
