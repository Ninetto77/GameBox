using Cache;
using Code.Global.Animations;
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
		public PunchAnimationPreset presetPunchRotation;
		public PunchAnimationPreset presetPunchPosition;

		[Header("Effects")]
		[SerializeField] private ParticleSystem particleOnDestroy;

		[Inject] private PointsLevel shop;
		[Inject] private AudioManager audioManager;


		private const string candyAudio = GlobalStringsVars.CANDY_SOUND_NAME;
		private Point candyPoint;

		private void Start()
		{
			candyPoint = new Point(Points);
		}

		protected override void OnTick()
		{
			AnimationShortCuts.PunchRotationAnimation(AnimTransform, presetPunchRotation);
			//AnimationShortCuts.PunchPositionAnimation(transform, presetPunchPosition);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag != "Player")
				return;

			shop.AddPoints(candyPoint);
			shop.AddCandies();

			audioManager.PlaySound(candyAudio);

			if (particleOnDestroy != null)
			{
				var hitEffect = Instantiate(particleOnDestroy, transform.position, Quaternion.identity);
			}
			Destroy(this.gameObject);
		}
	}
}
