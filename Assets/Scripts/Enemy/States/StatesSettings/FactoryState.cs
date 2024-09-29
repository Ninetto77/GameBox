namespace Enemy.States
{
    public static class FactoryState
    {
        public static EnemyState GetStateEnemy(StatesEnum state, IEnemy enemy)
        {
            switch (state)
            {
                case StatesEnum.idle:
                    return new IdleState(enemy);
                case StatesEnum.scream:
                    return new ScreamState(enemy);
                case StatesEnum.run:
                    return new RunState(enemy);
                case StatesEnum.attack:
                    return new AttackState(enemy);
                case StatesEnum.damage:
                    return new DamageState(enemy);
                case StatesEnum.death:
                    return new DeathState(enemy);
                default:
                    return new IdleState(enemy);
            }
        }
    }
}