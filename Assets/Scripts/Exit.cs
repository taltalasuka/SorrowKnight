using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{
    private int _curLevelIndex;
    private int _levelQuan;
    void Start()
    {
        _curLevelIndex = SceneManager.GetActiveScene().buildIndex;
        _levelQuan = SceneManager.sceneCountInBuildSettings;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ScenePersist>().ResetGamePersist();
            if (_curLevelIndex == _levelQuan - 1)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(_curLevelIndex + 1);
            }
        }
    }
}
