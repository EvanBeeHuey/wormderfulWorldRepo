using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    //score
    private int _score = 0;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            Debug.Log($"Score has changed to {_score}");
        }
    }

    //life
    private int _life = 5;
    public int Life
    {
        get => _life;
        set
        {
            _life = value;
            Debug.Log($"Life has changed to {_life}");
        }
    }

    //singleton instance, i think?
    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            return;
        }

        Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string sceneName = (SceneManager.GetActiveScene().name.Contains("SewerLevel")) ? "TitleScreen" : "SewerLevel";
            SceneManager.LoadScene(sceneName);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            Score++;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            Life++;

        if (Input.GetKeyDown(KeyCode.Alpha3))
            Life--;
    }
}
