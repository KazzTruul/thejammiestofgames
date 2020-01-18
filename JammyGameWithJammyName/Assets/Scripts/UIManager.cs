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

    private UIManager _instance;

    public UIManager Instance => _instance;

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
    }
}