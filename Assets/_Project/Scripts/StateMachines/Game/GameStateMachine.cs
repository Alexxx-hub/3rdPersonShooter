namespace _Project.Scripts.StateMachines.Game
{
    public class GameStateMachine
    {
        private GameStateBase _currentState;
        //--------------------------------------------------------------------------------------------------------------
        public GameStateMachine(GameStateBase state)
        {
            _currentState = state;
            _currentState.EnterState();
        }
        //--------------------------------------------------------------------------------------------------------------
        public void SwitchState(GameStateBase state)
        {
            _currentState = state;
            _currentState.EnterState();
        }
        //--------------------------------------------------------------------------------------------------------------
    }
}