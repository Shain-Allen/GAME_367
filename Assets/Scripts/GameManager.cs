using System;
using System.Collections.Generic;
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
    private List<EnemyHidingPoint> _enemyHidingPoints;

    private void Start()
    {
        _gameState = GameState.Playing;
        
        _enemyHidingPoints = new List<EnemyHidingPoint>();
        foreach (EnemyHidingPoint enemyHidingPoint in FindObjectsOfType<EnemyHidingPoint>())
        {
            _enemyHidingPoints.Add(enemyHidingPoint);
        }
    }

    // when a enemy requests a hiding point, return the one closets to it that is invisible
    public Transform GetClosestHidingPoint(Transform enemyTransform)
    {
        Transform closestHidingPoint = null;
        float closestDistance = float.MaxValue;
        foreach (EnemyHidingPoint enemyHidingPoint in _enemyHidingPoints)
        {
            if (enemyHidingPoint.IsPointVisible)
            {
                continue;
            }
            float distance = Vector3.Distance(enemyTransform.position, enemyHidingPoint.transform.position);
            if (!(distance < closestDistance)) continue;
            closestHidingPoint = enemyHidingPoint.transform;
            closestDistance = distance;
        }
        return closestHidingPoint;
    }
    
}
