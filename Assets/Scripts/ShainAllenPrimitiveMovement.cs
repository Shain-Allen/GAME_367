using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShainAllenPrimitiveMovement : MonoBehaviour
{
    private int _nextPos;
    
    [SerializeField] private Transform _pos1;
    [SerializeField] private Transform _pos2;
    [SerializeField] private Transform _pos3;

    [SerializeField] private Color _color1;
    [SerializeField] private Color _color2;
    [SerializeField] private Color _color3;

    [SerializeField] private float _moveSpeed;

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        
        Vector3 startPos = Vector3.zero;

        Vector3 pos1Position = _pos1.position;
        Vector3 pos2Position = _pos2.position;
        
        startPos.x = Random.Range(pos1Position.x, pos2Position.x);
        startPos.y = Random.Range(pos1Position.y, pos2Position.y);
        startPos.z = Random.Range(pos1Position.z, pos2Position.z);

        transform.position = startPos;

        _nextPos = 1;
    }

    private void Update()
    {
        //switch statement to control which pos to go to
        switch (_nextPos)
        {
            case 1:
                MoveToPoint(_pos1, _color1, 2);
                break;
            case 2:
                MoveToPoint(_pos2, _color2, 3);
                break;
            case 3:
                MoveToPoint(_pos3, _color3, 1);
                break;
        }
    }

    //function to reduce code duplication.
    //takes in the pos to move to, color to change to when getting to that pos, and the new next pos to go to when current nextPos is reached
    private void MoveToPoint(Transform currentPos, Color newColor, int newNextPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPos.position, _moveSpeed * Time.deltaTime);
        //Check if we have reached the position and if we have set _nextPos to newNextPos
        if (transform.position != currentPos.position) return;
        _renderer.material.color = newColor;
        _nextPos = newNextPos;
    }
}
