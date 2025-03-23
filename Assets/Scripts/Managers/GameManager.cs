using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;
    public event Action<PlayerController> OnPlayerSpawned;
    public UnityEvent <int> OnLifeValueChanged;
    public UnityEvent <int> OnScoreValueChanged;

    #region GAME PROPERTIES
    //lives
    [SerializeField] private int maxLives = 10;
    private int _lives = 5;
    public int lives
    {
        get => _lives;
        set
        {
            if (value <= 0)
            {
                GameOver();
                return;
            }

            if (_lives > value) Respawn();

            _lives = value;

            if (_lives > maxLives) _lives = maxLives;

            OnLifeValueChanged?.Invoke(_lives);

            Debug.Log($"{gameObject.name} lives has changed to {_lives}");
        }
    }

    //score
    private int _score = 0;
    public int score
    {
        get => _score;
        set
        {
            _score = value;
            Debug.Log($"{gameObject.name} score has changed to {+score}");

            OnScoreValueChanged?.Invoke(_score);
        }
    }
    #endregion

    #region PLAYER CONTROLLER INFO
    [SerializeField] private PlayerController playerPrefab;
    private PlayerController _playerInstance;
    public PlayerController PlayerInstance => _playerInstance;
    #endregion

    private MenuController currentMenuController;
    private Transform currentCheckpoint;

    public void SetMenuController(MenuController newMenuController) => currentMenuController = newMenuController;

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

    private void Start()
    {
        if (maxLives <= 0) maxLives = 5;
        _lives = maxLives;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string sceneName = (SceneManager.GetActiveScene().name.Contains("SewerLevel")) ? "TitleScreen" : "SewerLevel";
            SceneManager.LoadScene(sceneName);
        }

        if (SceneManager.GetActiveScene().name.Contains("SewerLevel"))
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (currentMenuController.CurrentState.state != MenuStates.Pause)
                    currentMenuController.SetActiveState(MenuStates.Pause);
                else
                    currentMenuController.JumpBack();
            }
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over goes here");
    }

    void Respawn()
    {
        _playerInstance.transform.position = currentCheckpoint.position;
    }

    public void InstantiatePlayer(Transform spawnLocation)
    {
        _playerInstance = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        currentCheckpoint = spawnLocation;
        OnPlayerSpawned?.Invoke(_playerInstance);
    }

    public void UpdateCheckpoint(Transform updatedCheckpoint)
    {
        currentCheckpoint = updatedCheckpoint;
        Debug.Log("Checkpoint updated");
    }
}
