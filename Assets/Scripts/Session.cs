using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Session : MonoBehaviour
{
    [SerializeField] private int health;
    private int _numsOfGameSession;
    public int score;
    [SerializeField] private Image liveCount;

    [SerializeField] private TextMeshProUGUI text;
    public void Start()
    {
        score = 0;
    }
    public void Update()
    {
        text.text = score.ToString();
        liveCount.rectTransform.sizeDelta = new Vector2(health * 25, 25);
    }

    public void HealthLost()
    {
        if (health > 1)
        {
            health--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            FindObjectOfType<ScenePersist>().ResetGamePersist();
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }

    private void Awake()
    {
        _numsOfGameSession = FindObjectsOfType<Session>().Length;
        if (_numsOfGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
