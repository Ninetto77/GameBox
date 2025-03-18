using Attack.Overlap;

namespace Enemy
{
	public interface IWeaponVisitor
	{
		void Visit(MelleWeapon weapon);
	}
}
