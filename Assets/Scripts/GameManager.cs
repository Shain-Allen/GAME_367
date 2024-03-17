using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManagerInstance;
    public static GameManager GameManagerInstance
    {
        get
        {
            if (_gameManagerInstance == null)
            {
                _gameManagerInstance = GameObject.FindObjectOfType<GameManager>();
            }
            return _gameManagerInstance;
        }
    }

    private static PlayerStateMachine _player;
    public static PlayerStateMachine Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindObjectOfType<PlayerStateMachine>();
            }
            return _player;
        }
    }

    public GameState _gameState { get; set; }

    private void Start()
    {
        _gameState = GameState.Playing;
    }
}
