using UnityEngine;
namespace Enemy.States
{
    public class IdleState : EnemyState
    {
		public IdleState(IEnemy enemyController) : base(enemyController) { }

		public override void Update()
		{ }

		public override void Enter()
		{
			base.Enter();
		}

		public override void Exit()
		{
			base.Exit();
		}

		//    private float timeToRevert;
		//    private float curTimeToRevert;

		//    public IdleState(EnemyController enemyController) : base(enemyController)
		//    {
		//        timeToRevert = enemy.TimeToRevert;
		//    }

		//    public override void Update()
		//    {
		//       curTimeToRevert += Time.deltaTime;
		//        if (curTimeToRevert > timeToRevert)
		//        {
		//            isFinish = true;
		//        }
		//    }

		//    public override void Enter()
		//    {
		//        base.Enter();
		//        curTimeToRevert = 0;
		//    }

		//    public override void Exit() { }
		//}

	}
}
