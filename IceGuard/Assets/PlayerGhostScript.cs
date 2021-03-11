using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostScript : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    public AnimationCurve Easing;
    public static float TimeToReachNextTile = 0.5f;
    [Range(0.1f, 1f)]
    public float PublicTimeToReachNextTile = 0.5f; /// УДАЛИТЬ ПОСЛЕ ТЕСТОВ
    [SerializeField] private GameObject PathPoint;
    public enum States
    {
        ReadyToMove,
        Moving
    }
    public States PlayerState;

    private float _time;

    private void Start()
    {
        PlayerState = States.ReadyToMove;
        _startPosition = transform.position;
        NewPathGhostScript.GhostPreviousPoint = transform.position;
    }
    private void Update()
    {
        TimeToReachNextTile = PublicTimeToReachNextTile; /// УДАЛИТЬ ПОСЛЕ ТЕСТОВ
        Move();
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

        }
    }




    private Vector3 LerpMoveTo(Vector3 start, Vector3 end, float time)
    {
        return Vector3.Lerp(start, end, Easing.Evaluate(_time / time));
    }
}
