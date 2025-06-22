using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class LevelSelector : MonoBehaviour
{
    public MapDataManager mapDataManager;  
    public Button[] levelButtons;

    public Transform homeButtonTransform;     
    public Transform selectLevelBannerTransform;

    public AudioClip winSound;
    private AudioSource audioSource;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && winSound != null)
        {
            audioSource.PlayOneShot(winSound);
        }

        DOTween.KillAll();

        SetupLevelButtons();
        StartCoroutine(AnimateLevelButtons());
        homeButtonTransform.localScale = Vector3.zero;
        homeButtonTransform.DOScale(Vector3.one * 1f, 0.5f).SetEase(Ease.OutBack);

        selectLevelBannerTransform.localScale = Vector3.zero;
        selectLevelBannerTransform.DOScale(Vector3.one * 1f, 0.5f).SetEase(Ease.OutBack).SetDelay(0.1f);

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SelectLevel")
        {
            SetupLevelButtons();
        }
    }

    void SetupLevelButtons()
    {

        if (LevelManager.Instance == null)
        {
            Debug.LogError("LevelManager chưa được khởi tạo.");
            return;
        }

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i;  
            levelButtons[i].onClick.RemoveAllListeners();  
            levelButtons[i].onClick.AddListener(() => SelectLevel(index));  
        }
    }

    public void SelectLevel(int levelIndex)
    {
        LevelManager.Instance.selectedLevelIndex = levelIndex;

        SceneManager.LoadScene("Game");
    }
    public void BackHome()
    {
        SceneManager.LoadScene("Home");
    }
    IEnumerator AnimateLevelButtons()
    {
        Vector3 originalScale = Vector3.one * 2f; 

        foreach (Button btn in levelButtons)
        {
            btn.transform.localScale = Vector3.zero; 
        }

        yield return new WaitForSeconds(0.3f); 

        for (int i = 0; i < levelButtons.Length; i++)
        {
            Button btn = levelButtons[i];

            btn.transform.DOScale(originalScale * 1.1f, 0.4f)
                .SetEase(Ease.OutBack)
                .OnComplete(() => {
                    btn.transform.localScale = originalScale;
                });

            yield return new WaitForSeconds(0.1f);
        }
    }
}
