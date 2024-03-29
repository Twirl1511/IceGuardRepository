﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    public static NewPlayerController singleton;
    public Vector3 _startPosition;
    public Vector3 _endPosition;
    public AnimationCurve Easing;
    public static float TimeToReachNextTile = 0.5f;
    [Range(0.1f, 3f)]
    public float PublicTimeToReachNextTile = 0.5f; /// УДАЛИТЬ ПОСЛЕ ТЕСТОВ
    [SerializeField] private GameObject OutterRim;
    [SerializeField] private GameObject InnerRim;
    [SerializeField] private float RotationSpeed;
    private float _outerY;
    private float _innerY;
    public GameObject ExplosionName;
    private GameObject ExplosionObject;
    private bool _isExplosionExist = false;
    public static int TutorialMinesCounter;

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
        if(singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }


        TutorialMinesCounter = 0;
        NewPathScript.PathPoints.Clear();
        gameObject.GetComponent<NewPlayerController>().enabled = true;
        PlayerState = States.ReadyToMove;
        _startPosition = transform.position;
        NewPathScript.PreviousPoint = transform.position;
    }
    private void Update()
    {
        if (PauseManager.GameState == PauseManager.GameStates.Play)
        {
            TimeToReachNextTile = PublicTimeToReachNextTile; /// УДАЛИТЬ ПОСЛЕ ТЕСТОВ
            Move();
        }
    }

    private void RingsRotation(float speed)
    {
        _outerY +=  speed * Time.deltaTime;
        OutterRim.transform.rotation = Quaternion.Euler(0, _outerY, 0);
        _innerY -= speed * Time.deltaTime;
        InnerRim.transform.rotation = Quaternion.Euler(0, _innerY, 0);
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
            _time += Time.deltaTime;
            transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile * _lengthBtwPoints);
            RingsRotation(RotationSpeed);

            if (_time >= TimeToReachNextTile * _lengthBtwPoints)
            {
                _time = 0;
                PlayerState = States.ReadyToMove;
                transform.position = _endPosition;
                _startPosition = _endPosition;
            }
        }
    }

    private void CreateExplosion(Vector3 position)
    {
        if (!_isExplosionExist)
        {
            _isExplosionExist = true;
            ExplosionObject = GameObject.Instantiate(Resources.Load(ExplosionName.name), position, Quaternion.identity) as GameObject;
            StartCoroutine(LaterTurnOff(ExplosionObject));
        }
        else
        {
            ExplosionObject.transform.position = position;
            StartCoroutine(LaterTurnOff(ExplosionObject));
        }
    }

    IEnumerator LaterTurnOff(GameObject gameObject)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EmptyField"))
        {
            GameObject NewForceField;
            NewForceField = GameObject.Instantiate(Resources.Load("Mine"), other.transform.position, Quaternion.identity) as GameObject;
            NewForceFieldScript.allForceFields.Add(NewForceField);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Path"))
        {
            GameObject.Destroy(other.gameObject);
        }
        if (other.CompareTag("Mine"))
        {
            CreateExplosion(other.transform.position);
            TutorialMinesCounter++;
        }
    }



    private Vector3 LerpMoveTo(Vector3 start, Vector3 end, float time)
    {
        return Vector3.Lerp(start, end, Easing.Evaluate(_time / time));
    }

}
