using UnityEngine;

namespace Enemy.States
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(Rigidbody))]
	public class Vampire : EnemyController, IAttack
	{
		[Header("���������")]
		[Tooltip("�������� ��������� �������")]
		public float healValue = 10;

		#region �����
		public void Attack()
		{
			GetHeal();
			player.ApplyDamage(Damage);
		}
		#endregion

		#region ���������
		private void GetHeal()
		{
			health.TakeTreat(healValue);
		}
		#endregion
	}
}