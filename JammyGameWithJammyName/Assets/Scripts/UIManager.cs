using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image _heroHealthSlider;
    [SerializeField]
    private TMP_Text _heroHealthText;
    [SerializeField]
    private Image _bossHealthSlider;
    [SerializeField]
    private TMP_Text _bossHealthText;
    [SerializeField]
    private Image _bossSuspicionSlider;
    [SerializeField]
    private TMP_Text _bossSuspicionText;
    [SerializeField]
    private Image _bossBloodPressureSlider;
    [SerializeField]
    private TMP_Text _bossBloodPressureText;
    [SerializeField]
    private GameObject _gameOverScreen;
    [SerializeField]
    private GameObject _winScreen;
    [SerializeField]
    private GameObject _pauseMenu;

    private static UIManager _instance;

    public static UIManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public void SetHeroHealth(int current, int max)
    {
        _heroHealthSlider.fillAmount = (float)current / (float)max;
        _heroHealthText.text = $"{current}/{max} hp";
    }

    public void SetBossHealth(int current, int max)
    {
        _bossHealthSlider.fillAmount = (float)current / (float)max;
        _bossHealthText.text = $"{current}/{max} hp";
    }

    public void SetBossSuspicion(int current, int max)
    {
        _bossSuspicionSlider.fillAmount = (float)current / (float)max;
        _bossSuspicionText.text = $"{_bossSuspicionSlider.fillAmount * 100}%";
    }

    public void SetBossBloodPressure(int current, int max)
    {
        _bossBloodPressureSlider.fillAmount = (float)current / (float)max;
        _bossBloodPressureText.text = $"{current} bpm";
    }

    public void GameOver(bool gameWon)
    {
        if (gameWon)
        {
            _winScreen.SetActive(true);
        }
        else
        {
            _gameOverScreen.SetActive(true);
        }
    }
}
