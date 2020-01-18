public static class GameStateManager
{
    private static GameState _currentGameState = GameState.Menu;

    public static GameState CurrentGameState => _currentGameState;

    public static void SetGameState(GameState newGameState)
    {
        if (newGameState == _currentGameState)
        {
            return;
        }

        _currentGameState = newGameState;
    }
}