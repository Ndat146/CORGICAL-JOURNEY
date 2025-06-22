using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    public GameObject winPanel;  
    public Button playAgainButton;
    private Vector3 originalScale;
    private bool isShowing = false;

    public AudioClip winSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("Thiếu AudioSource trên WinPanel GameObject.");
        }

        originalScale = winPanel != null ? winPanel.transform.localScale : Vector3.one;

        if (winPanel == null)
        {
            Debug.LogError("Win Panel chưa được gán trong Inspector.");
        }

        if (playAgainButton == null)
        {
            Debug.LogError("Play Again Button chưa được gán trong Inspector.");
        }

        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(OnPlayAgainClicked);
        }
    }
    public void OnWin()
    {
        Debug.Log("Level Completed! Moving to the next level...");
        ShowWinPanel();  
    }

    public void ShowWinPanel()
    {
        if (winPanel != null && !isShowing)
        {
            isShowing = true;

            DOTween.Kill(winPanel.transform); 
            winPanel.SetActive(true);

            if (audioSource != null && winSound != null)
            {
                audioSource.PlayOneShot(winSound);
            }

            winPanel.transform.localScale = Vector3.zero;
            winPanel.transform.DOScale(originalScale, 0.5f).SetEase(Ease.OutBack);
        }
        else if (winPanel == null)
        {
            Debug.LogError("Win Panel không được gán hoặc đã bị hủy.");
        }
    }

    private void OnPlayAgainClicked()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void ResetWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
            isShowing = false; 
        }
    }

    public void BackHome()
    {
        SceneManager.LoadScene("Home");
    }
}
