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
		public PunchAnimationPreset presetPunchRotation;
		public PunchAnimationPreset presetPunchPosition;

		[Inject] private ShopPoint shop;
		private Point candyPoint;

		private void Start()
		{
			candyPoint = new Point(Points);
		}

		public override void OnTick()
		{
			AnimationShortCuts.PunchRotationAnimation(transform, presetPunchRotation);
			//AnimationShortCuts.PunchPositionAnimation(transform, presetPunchPosition);
		}

		private void OnTriggerEnter(Collider other)
		{
			shop.AddPoints(candyPoint);
			Destroy(this.gameObject, 0.5f);
		}
	}
}
