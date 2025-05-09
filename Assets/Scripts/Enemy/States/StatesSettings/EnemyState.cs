namespace Enemy.States
{
    public abstract class EnemyState: IEnemyState
	{
        public bool isFinish;
        protected IEnemy enemy;

        public EnemyState()
        {
        }
        public EnemyState(IEnemy enemyController)
        {
            enemy = enemyController;
        }

        /// <summary>
        /// call each frame
        /// </summary>
        public virtual void Update() { }
        /// <summary>
        /// enter to the state
        /// </summary>
        public virtual void Enter() 
        {
            isFinish = false;
        }
        /// <summary>
        /// exit from the state
        /// </summary>
        public virtual void Exit()
        {
            isFinish=true;
        }
    }
}
