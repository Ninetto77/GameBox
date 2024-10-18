using Enemy.States;
using UnityEngine;

namespace Enemy.States
{
    public class StateMachine
    {
        public EnemyState CurrentState { get; private set; }

        public void Init(EnemyState newState)
        {
            CurrentState = newState;
            CurrentState.Enter();
        }
        public void ChangeState(EnemyState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            //Debug.Log($"Current state is {CurrentState}");
            CurrentState.Enter();
        }
    }
}

