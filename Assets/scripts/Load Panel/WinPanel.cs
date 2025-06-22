using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    public GameObject winPanel;  
    public Button playAgainButton;  

    private void Start()
    {
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
        if (winPanel != null)
        {
            winPanel.SetActive(true);  
        }
        else
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
        }
    }
}
