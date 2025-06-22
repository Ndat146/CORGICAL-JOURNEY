using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using DG.Tweening; 

public class LoadPanel : MonoBehaviour
{
    public GameObject homePanel;
    public GameObject helpPanel;
    public AudioClip winSound;
    private AudioSource audioSource;
    void Start()
    {
        helpPanel.SetActive(false);
        BackHome();

        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void GoHelp()
    {
        DOTween.KillAll();

        helpPanel.SetActive(true);
        helpPanel.transform.localScale = Vector3.zero;
        helpPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack); 

        homePanel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack)
            .OnComplete(() => homePanel.SetActive(false));
    }

    public void BackHome()
    {
        DOTween.KillAll();

        homePanel.SetActive(true);
        homePanel.transform.localScale = Vector3.zero;
        homePanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        helpPanel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack)
            .OnComplete(() => helpPanel.SetActive(false));
    }
    public void GoSelectLevel()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
