using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ExperienceManager : MonoBehaviour
{
    [Header("Experience")]
    [SerializeField] private AnimationCurve experienceCurve;

    public static int currentLevel { private set; get; } = 1;
    private int totalExperience = 0;
    private int previousLevelsExperience, nextLevelsExperience;

    [Header("Interface")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private Image experienceFill;



    public event Action<int> OnLevelUp;

    void Start()
    {
        UpdateLevel();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            AddExperience(5);
        }
    }

    public void AddExperience(int amount)
    {
        totalExperience += amount;
        UpdateInterface();
        CheckForLevelUp();
    }

    private void CheckForLevelUp()
    {

        if (totalExperience >= nextLevelsExperience)
        {
            currentLevel++;
            OnLevelUp?.Invoke(currentLevel);
            UpdateLevel();
        }
    }

    private void UpdateLevel()
    {
        previousLevelsExperience = (int)experienceCurve.Evaluate(currentLevel - 1);
        nextLevelsExperience = (int)experienceCurve.Evaluate(currentLevel);
        UpdateInterface();
    }

    private void UpdateInterface()
    {
        int start = totalExperience - previousLevelsExperience;
        int end = nextLevelsExperience - previousLevelsExperience;

        levelText.text = currentLevel.ToString();
        experienceText.text = start + " exp / " + end + " exp";
        experienceFill.fillAmount = (float)start / (float)end;
    }
}