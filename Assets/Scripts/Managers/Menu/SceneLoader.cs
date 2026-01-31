using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ProcessLoadScene(Scenes sceneToLoad, bool shouldUseFade)
    {
        switch (sceneToLoad)
        {
            case Scenes.MainMenu:
                if (shouldUseFade)
                {
                    FadeTransitioner.Instance.FadeOut(() => LoadScene(0));
                    return;
                }
                LoadScene(0);
                break;
            case Scenes.GameScene:
                if (shouldUseFade)
                {
                    FadeTransitioner.Instance.FadeOut(() => LoadScene(1));
                    return;
                }
                LoadScene(1);
                break;
        }
    }

    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
