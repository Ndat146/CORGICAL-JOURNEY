using UnityEngine;

public class LoadPanel : MonoBehaviour
{
    public GameObject homePanel;
    public GameObject helpPanel;
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
}
