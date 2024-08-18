using UnityEngine;

public class StateMashine : MonoBehaviour
{
    [HideInInspector]
    public enum StateType
    {
        game,
        inventory,
        craft
    }

    [Header("Windows")]
    [SerializeField] private Window _gameScreen;
    [SerializeField] private Window _inventoryScreen;
    [SerializeField] private Window _craftScreen;


    private Window _currentScreen;

    private void Start()
    {
        _currentScreen = _gameScreen;
    }

    public void ChangeStates(StateType state)
    {
        if (_currentScreen == null)
        {
            return;
        }
        _currentScreen.Close_Instantly();

        switch (state)
            {
            case StateType.game:
                _gameScreen.Open_Instantly();
                _currentScreen = _gameScreen;
                break;
			case StateType.inventory:
				_inventoryScreen.Open_Instantly();
				_currentScreen = _inventoryScreen;
				break;
			case StateType.craft:
				_craftScreen.Open_Instantly();
				_currentScreen = _craftScreen;
				break;
		}
    }
}
