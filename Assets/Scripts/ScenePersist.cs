using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    public void ResetGamePersist()
    {
        Destroy(gameObject);
    }
    private void Awake()
    {
        int len = FindObjectsOfType<ScenePersist>().Length;
        if (len > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
