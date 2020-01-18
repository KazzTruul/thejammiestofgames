using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Slider _heroHealthSlider;
    [SerializeField]
    private TMP_Text _heroHealthText;
    [SerializeField]
    private Slider _bossHealthSlider;
    [SerializeField]
    private TMP_Text _bossHealthText;
    [SerializeField]
    private Slider _bossSuspicionSlider;
    [SerializeField]
    private TMP_Text _bossSuspicionText;
    [SerializeField]
    private Slider _bossBloodPressureSlider;
    [SerializeField]
    private TMP_Text _bossBloodPressureText;
    [SerializeField]
    private GameObject _gameOverScreen;
    [SerializeField]
    private GameObject _winScreen;

    private static UIManager _instance;

    public static UIManager Instance => _instance;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public void SetHeroHealth(int current, int max)
    {
        _heroHealthSlider.value = current;
        _heroHealthSlider.maxValue = max;
        _heroHealthText.text = $"{current}/{max} HP";
    }

    public void SetBossHealth(int current, int max)
    {
        _bossHealthSlider.value = current;
        _bossHealthSlider.maxValue = max;
        _bossHealthText.text = $"{current}/{max} HP";
    }

    public void SetBossSuspicion(int current, int max)
    {
        _bossSuspicionSlider.value = current;
        _bossSuspicionSlider.maxValue = max;
        _bossSuspicionText.text = $"{current}/{max} Suspicion";
    }

    public void SetBossBloodPressure(int current, int max)
    {
        _bossBloodPressureSlider.value = current;
        _bossBloodPressureSlider.maxValue = max;
        _bossBloodPressureText.text = $"{current} BPM";
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