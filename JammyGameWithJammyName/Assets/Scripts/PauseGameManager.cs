using UnityEngine;

public class PauseGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;

    private static PauseGameManager _instance;

    public static PauseGameManager Instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public void SetGamePaused(bool pauseGame)
    {
        Time.timeScale = pauseGame ? 0f : 1f;
        _pauseMenu.SetActive(pauseGame);
        GameStateManager.SetGameState(pauseGame ? GameState.GamePaused : GameState.GameRunning);
    }
}
