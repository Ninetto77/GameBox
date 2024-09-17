public interface IEnemyState
{
	/// <summary>
	/// call each frame
	/// </summary>
	public virtual void Update() { }
	/// <summary>
	/// enter to the state
	/// </summary>
	public virtual void Enter() { }

	/// <summary>
	/// exit from the state
	/// </summary>
	public virtual void Exit() { }

}
