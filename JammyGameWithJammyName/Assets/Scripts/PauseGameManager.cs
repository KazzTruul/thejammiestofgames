using UnityEngine;

public class PauseGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;

    private static PauseGameManager _instance;

    public static PauseGameManager Instance;

    private bool _gamePaused;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;

        GameStateManager.SetGameState(GameState.GameRunning);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && (GameStateManager.CurrentGameState == GameState.GameRunning || GameStateManager.CurrentGameState == GameState.GamePaused))
        {
            SetGamePaused(!_gamePaused);
        }
    }

    public void SetGamePaused(bool pauseGame)
    {
        _gamePaused = pauseGame;
        Time.timeScale = pauseGame ? 0f : 1f;
        _pauseMenu.SetActive(pauseGame);
        GameStateManager.SetGameState(pauseGame ? GameState.GamePaused : GameState.GameRunning);
        Cursor.lockState = pauseGame ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = pauseGame;
    }
}
