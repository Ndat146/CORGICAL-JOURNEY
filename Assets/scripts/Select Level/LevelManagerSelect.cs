using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int selectedLevelIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // xóa bản trùng
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // giữ lại khi load scene
    }
}
