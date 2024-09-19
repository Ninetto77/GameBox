namespace Player.States
{
	public static class FactoryState
	{
		public static PlayerState GetStatePlayer(StatesEnum state, PlayerBrain player)
		{
			switch (state)
			{
				case StatesEnum.idle:
					return new IdleState(player);
				case StatesEnum.shoot:
					return new ShootState(player);
				case StatesEnum.gather:
					return new GatherState(player);
				default:
					return new IdleState(player);
			}
		}
	}
}