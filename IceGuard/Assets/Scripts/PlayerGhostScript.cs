﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostScript : MonoBehaviour
{
    public Vector3 _startPosition;
    public Vector3 _endPosition;
    public AnimationCurve Easing;
    public static float TimeToReachNextTile = 0.5f;
    [SerializeField] private GameObject PathPoint;
    //private List<GameObject> PathObjectsArray = new List<GameObject>(50);
    public enum States
    {
        ReadyToMove,
        Moving
    }
    public States PlayerState;

    private float _time;

    private void Start()
    {

        PathPointController.singleton.ClearAllPathPoints();
        NewPathGhostScript.GhostPathPoints.Clear();
        gameObject.GetComponent<PlayerGhostScript>().enabled = true;
        PlayerState = States.ReadyToMove;
        _startPosition = transform.position;
        NewPathGhostScript.GhostPreviousPoint = transform.position;
    }
    private void Update()
    {
        if (PauseManager.GameState == PauseManager.GameStates.Play)
        {
            Move();
            Death();
        }
           
    }
    public void Death()
    {
        if (PlayerHitPoints.HitPoints <= 0)
        {
            gameObject.GetComponent<PlayerGhostScript>().enabled = false;
        }
    }
    public void Move()
    {
        if (NewPathGhostScript.GhostPathPoints.Count > 0 && PlayerState == States.ReadyToMove)
        {
            _endPosition = NewPathGhostScript.GetPathPoint();
            PlayerState = States.Moving;


        }

        if (PlayerState == States.Moving)
        {
            transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
            _time += Time.deltaTime;

            if (_time >= TimeToReachNextTile)
            {
                _time = 0;
                PlayerState = States.ReadyToMove;
                transform.position = _endPosition;
                _startPosition = _endPosition;
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EmptyField"))
        {
            GameObject NewPathPoingObject;
            NewPathPoingObject = GameObject.Instantiate(Resources.Load(PathPoint.name), other.transform.position, Quaternion.identity) as GameObject;
            //PathObjectsArray.Add(NewPathPoingObject);
            PathPointController.singleton.AddPathPoint(NewPathPoingObject);

        }
    }

    public void ClearPathObjects(GameObject pathPoint)
    {
        //int index = PathObjectsArray.IndexOf(pathPoint);
        //PathObjectsArray[index].SetActive(false);
        //for(int i = index+1; i < PathObjectsArray.Count; i++)
        //{
        //    Destroy(PathObjectsArray[i]);
        //}
        //PathObjectsArray.RemoveRange(index + 1, PathObjectsArray.Count);
    }



    private Vector3 LerpMoveTo(Vector3 start, Vector3 end, float time)
    {
        return Vector3.Lerp(start, end, Easing.Evaluate(_time / time));
    }
}
