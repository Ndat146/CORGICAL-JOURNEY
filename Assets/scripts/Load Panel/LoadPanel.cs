using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadPanel : MonoBehaviour
{
    public GameObject homePanel;
    public GameObject helpPanel;
    //public GameObject winPanel;
    //public Button playAgainButton;

    void Start()
    {
        helpPanel.SetActive(false);
    }
    public void GoHelp()
    {
        helpPanel.SetActive(true);
        homePanel.SetActive(false);
    }
    public void BackHome()
    {
        helpPanel.SetActive(false);
        homePanel.SetActive(true);
    }
    //public void OnWin()
    //{
    //    Debug.Log("Level Completed! Moving to the next level...");
    //    ShowWinPanel();
    //}

    //private void ShowWinPanel()
    //{
    //    winPanel.SetActive(true);
    //}

    //public void GoSelectLevel()
    //{

    //    SceneManager.LoadScene("LevelSelect");
    //}
}
