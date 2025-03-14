using Cache;
using Code.Global.Animations;
using DG.Tweening;
using Sounds;
using UnityEngine;
using Zenject;

namespace Points
{
	public class Candy : MonoCache
	{
		public int Points;

		[Header("Animations")]
		public Transform AnimTransform;
		public Transform MoveTransform;
		public PunchAnimationPreset presetPunchRotation;
		public ShakeAnimationPreset presetShakePosition;

		[Header("Effects")]
		[SerializeField] private ParticleSystem particleOnDestroy;

		[Inject] private PointsLevel shop;
		[Inject] private AudioManager audioManager;


		private const string candyAudio = GlobalStringsVars.CANDY_SOUND_NAME;
		private Point candyPoint;
		Sequence seq;

		private void Start()
		{
			candyPoint = new Point(Points);

			AnimationShortCuts.ShakePositionAnimation(MoveTransform, presetShakePosition).SetLoops(-1, LoopType.Yoyo);
		}

		protected override void OnTick()
		{
			AnimationShortCuts.PunchRotationAnimation(AnimTransform, presetPunchRotation);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag != "Player")
				return;

			audioManager.PlaySound(candyAudio);

			if (particleOnDestroy != null)
			{
				var hitEffect = Instantiate(particleOnDestroy, transform.position, Quaternion.identity);
			}

			shop.AddPoints(candyPoint);
			shop.AddCandies();
			Destroy(this.gameObject);
		}
	}
}
