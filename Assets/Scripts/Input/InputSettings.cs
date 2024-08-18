using UnityEngine;

[CreateAssetMenu(fileName ="Input", menuName = "InputSettings")]
public class InputSettings : ScriptableObject
{
	public KeyCode InventoryScreen;
	public KeyCode CraftScreen;
	public KeyCode UnequiptScreen;
	public KeyCode ShowCursor;
}
