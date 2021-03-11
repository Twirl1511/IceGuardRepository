using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    public AnimationCurve Easing;
    public static float TimeToReachNextTile = 0.5f;
    [Range(0.1f, 3f)]
    public float PublicTimeToReachNextTile = 0.5f; /// УДАЛИТЬ ПОСЛЕ ТЕСТОВ
    
    public enum States
    {
        ReadyToMove,
        Moving
    }
    public States PlayerState;

    private float _time;
    private int _lengthBtwPoints;


    private void Start()
    {
        PlayerState = States.ReadyToMove;
        _startPosition = transform.position;
        NewPathScript.PreviousPoint = transform.position;
    }
    private void Update()
    {
        TimeToReachNextTile = PublicTimeToReachNextTile; /// УДАЛИТЬ ПОСЛЕ ТЕСТОВ
        Move();
    }

    public void Move()
    {
        if(NewPathScript.PathPoints.Count > 0 && PlayerState == States.ReadyToMove)
        {
            _endPosition = NewPathScript.GetPathPoint();
            PlayerState = States.Moving;

            _lengthBtwPoints = (int)Mathf.Abs(_startPosition.x - _endPosition.x);
            if(_lengthBtwPoints == 0)
            {
                _lengthBtwPoints = (int)Mathf.Abs(_startPosition.z - _endPosition.z);
            }
        }

        if(PlayerState == States.Moving)
        {
            transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile * _lengthBtwPoints);
            _time += Time.deltaTime;

            if (_time >= TimeToReachNextTile * _lengthBtwPoints)
            {
                _time = 0;
                PlayerState = States.ReadyToMove;
                transform.position = _endPosition;
                _startPosition = _endPosition;
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EmptyField"))
        {
            GameObject NewForceField;
            NewForceField = GameObject.Instantiate(Resources.Load("NewForceField"), other.transform.position, Quaternion.identity) as GameObject;
            NewForceFieldScript.allForceFields.Add(NewForceField);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            GameObject.Destroy(other.gameObject);
        }
    }



    private Vector3 LerpMoveTo(Vector3 start, Vector3 end, float time)
    {
        return Vector3.Lerp(start, end, Easing.Evaluate(_time / time));
    }

}
