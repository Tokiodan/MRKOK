using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpSplash : MonoBehaviour
{
    [Header("Splash Screen Settings")]
    [SerializeField] private TextMeshProUGUI levelText;     
    [SerializeField] private CanvasGroup splashCanvasGroup; 
    [SerializeField] private float splashDisplayDuration = 2f; 

    [Header("Rewards Screen Settings")]
    [SerializeField] private CanvasGroup rewardsCanvasGroup;
    [SerializeField] private Button button1;                 
    [SerializeField] private Button button2;                 
    [SerializeField] private Button button3;                 

    private ExperienceManager experienceManager;

    void Start()
    {
        
        experienceManager = FindObjectOfType<ExperienceManager>();

        
        if (experienceManager != null)
        {
            experienceManager.OnLevelUp += ShowLevelUpSplash;
        }

        
        HideSplashScreen();
        HideRewardsScreen();

      
        button1.onClick.AddListener(OnButtonClicked);
        button2.onClick.AddListener(OnButtonClicked);
        button3.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
        // unbubs van de event
        if (experienceManager != null)
        {
            experienceManager.OnLevelUp -= ShowLevelUpSplash;
        }

        // Unsubs buttons 
        button1.onClick.RemoveListener(OnButtonClicked);
        button2.onClick.RemoveListener(OnButtonClicked);
        button3.onClick.RemoveListener(OnButtonClicked);
    }

    // Methoed die triggered wanneer de player leveled
    private void ShowLevelUpSplash(int newLevel)
    {
        levelText.text = $"Level Up!\nYou are now Level {newLevel}!";
        gameObject.SetActive(true);  // Zet de splash screen aan
        StartCoroutine(FadeInAndOutSplashScreen());
    }

    private IEnumerator FadeInAndOutSplashScreen()
    {
        // Fade in
        float fadeDuration = 0.5f;
        float elapsed = 0f;
        splashCanvasGroup.alpha = 0f;

        while (elapsed < fadeDuration)
        {
            splashCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        splashCanvasGroup.alpha = 1f;

        // Wachten op splash screen
        yield return new WaitForSeconds(splashDisplayDuration);

        // Fade out
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            splashCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        splashCanvasGroup.alpha = 0f;

        // Hide the splash screen
        HideSplashScreen();

       
        ShowRewardsScreen();
    }

    // verstopt de splash screen
    private void HideSplashScreen()
    {
        splashCanvasGroup.alpha = 0f;
        splashCanvasGroup.interactable = false;
        splashCanvasGroup.blocksRaycasts = false;
        gameObject.SetActive(false);
    }

    // laat t rewards screen zien
    private void ShowRewardsScreen()
    {
        rewardsCanvasGroup.alpha = 1f;
        rewardsCanvasGroup.interactable = true;
        rewardsCanvasGroup.blocksRaycasts = true;

        // unlocked de cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // stopt player movement
        Time.timeScale = 0f; // Pauseert het spel.
    }

    // Hide t rewards scherm wnr er op een button is geklikt
    private void HideRewardsScreen()
    {
        rewardsCanvasGroup.alpha = 0f;
        rewardsCanvasGroup.interactable = false;
        rewardsCanvasGroup.blocksRaycasts = false;

        // Cursor word verstopt en weer in t scherm gelocked
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

      
        Time.timeScale = 1f; //game gaat verder hierdoor
    }

  
    private void OnButtonClicked()
    {
        Debug.Log("Reward Button Clicked");
        HideRewardsScreen();
    }
}
