//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class PlayerControllerDrawPath : MonoBehaviour
//{
//    public  Queue<Vector3> queuePath;
//    public static bool Stop;
//    public AnimationCurve Easing;
//    public static float TimeToReachNextTile = 0.5f;
//    [Range(0.1f,3f)]
//    public float PublicTimeToReachNextTile = 0.5f;
//    public static Vector3 _startPosition;
//    private Vector3 _endPosition;
//    public static Vector3[] adjacentPositionsForStart = new Vector3[4];
//    [SerializeField] private GameObject ForceFied;
//    private string ForceFieldName;

//    public bool ForceField_ON;
//    [Range(1, 60)]
//    public int FPS;

//    private float _time;

//    private void Start()
//    {
//        ForceFieldName = ForceFied.name;
//        _startPosition = transform.position;
//        queuePath = new Queue<Vector3>();
//        AdjacentPositionsForStart();
//    }

//    private void Update()
//    {
//        _time += Time.deltaTime;
//        TimeToReachNextTile = PublicTimeToReachNextTile;
//        AdjacentPositionsForStart();
//        NewMove();
//        //PlayerMoveSound();
//        Application.targetFrameRate = FPS;
//    }

//    private void OnMouseOver()
//    {
//        if (Input.GetMouseButton(0))
//        {
//            DrawPath.ClearDrawPath();
//        }
//    }
//    public Vector3 NewPathPoint()
//    {
//        Vector3 pathPoint = transform.position;
//        try
//        {
//            pathPoint = DrawPath.PathsPoints[0].transform.position;
//        }
//        catch
//        {
//            DrawPath.ClearDrawPath();
//        }
        
//        DrawPath.PathsPoints.RemoveAt(0);

//        return pathPoint;

        
//    }
//    /// <summary>
//    /// массив где храним смежные позиции для начала пути
//    /// </summary>
//    private void AdjacentPositionsForStart()
//    {
//        adjacentPositionsForStart[0] = _startPosition + Vector3.left;
//        adjacentPositionsForStart[1] = _startPosition + Vector3.left * -1;
//        adjacentPositionsForStart[2] = _startPosition + Vector3.forward;
//        adjacentPositionsForStart[3] = _startPosition + Vector3.forward * -1;
//    }
//    private bool flagStartMoving = false;
//    //private void PlayerMoveSound()
//    //{
//    //    if (flagStartMoving)
//    //    {
//    //        FindObjectOfType<SoundManager>().PlaySound(Sound.SoundName.PlayerMove);
//    //    }
//    //}
    

//    public bool IsAdjacentToPlayerPosition()
//    {
//        bool flag = false;
//        Vector3 firstPathPosition = DrawPath.PathsPoints[0].transform.position;
//        Vector3[] adjacentPositions = new Vector3[4];
//        adjacentPositions[0] = _startPosition + Vector3.left;
//        adjacentPositions[1] = _startPosition + Vector3.left * -1;
//        adjacentPositions[2] = _startPosition + Vector3.forward;
//        adjacentPositions[3] = _startPosition + Vector3.forward * -1;

//        foreach(var e in adjacentPositions)
//        {
//            if (e == firstPathPosition) flag = true;
//        }

//        return flag;
//    }
//    private void NewMove()
//    {
//        /// если мы отпустили мышку/тач и в очереди движения есть куда двигаться (мы нарисовали путь)
//        /// а также счетчик времени сброшен (мы закончили предыдущее движение или еще не начинали никакого),
//        /// то запускаем скрипт движения
//        /// if (!Input.GetMouseButton(0) && queuePath.Count > 0 && _time == 0)
//        if (DrawPath.PathsPoints.Count > 0 && flagStartMoving == false)
//        {
//            /// в буферную векторную переменную записываем следующую координату для пути игрока
//            _endPosition = NewPathPoint();

//            /// избавляемся от того что иногда в очередь пути добавляются координаты того же места где стоит игрок, там образуется поле и игрок получает урон 0_о
//            while (_endPosition == transform.position)
//            {
//                _endPosition = NewPathPoint();
//            }

//            /// создаемн энергополе и активируем коллайдер через время которое нужно кораблю чтобы проехать клетку плюс 0.5
//            CreateEnergyField(_startPosition);
//            flagStartMoving = true;
//        }  

//        /// если движение не закончено, то мы перемещаем игрока на новую позицию, движение должно быть
//        /// закончено ровно за время TimeToReachNextTile
//        if (flagStartMoving)
//        {
//            transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
//            //_time += Time.deltaTime;
//        }
//        /// когда заканчивается время движения игрок должен закончить свой путь и мы можем сказать что его новая начальная
//        /// позиция равна той на которую он перемещался, также создаем энерго поле на последней позиции где он был
//        /// обнуляем время
//        /// flagStartMoving = false не даем больше заходить в цикл где происходит движение, считаем что оно закончено и ждем нового пути
//        if (_time >= TimeToReachNextTile)
//        {
//            transform.position = _endPosition;
//            _time = 0;
//            _startPosition = transform.position;
//            flagStartMoving = false;

//            /// если случился баг и мы нарисовали путь который не прилегает к игроку, очищаем массив пути
//            if (DrawPath.PathsPoints.Count > 0 && !IsAdjacentToPlayerPosition())
//            {
//                DrawPath.PathsPoints.Clear();
//            }
//        }
//    }

     
    

//    /// <summary>
//    /// создаем энергополя на предыдущей клетке движения и заносим их в массив allForceFields
//    /// </summary>
//    /// <param name="position">позиция где будет создано поле</param>
//    private void CreateEnergyField(Vector3 position)
//    {
//        /// проигрываем звук создания поля
//        FindObjectOfType<SoundManager>().PlaySoundOneShot(Sound.SoundName.ForceFieldCreate);

//        /// меняем угол поворота поля, которое будем спавнить за кораблем

//        Quaternion forceFieldQuaternion = Quaternion.Euler(0, 0, 0);
//        for (int i = 0; i < adjacentPositionsForStart.Length; i++)
//        {
//            if (adjacentPositionsForStart[i] == _endPosition)
//            {
//                switch (i)
//                {
//                    case 0:
//                        forceFieldQuaternion = Quaternion.Euler(0,180,0); /// верх-право
//                        break;
//                    case 1:
//                        forceFieldQuaternion = Quaternion.Euler(0, 0, 0); /// низ-лево
//                        break;
//                    case 2:
//                        forceFieldQuaternion = Quaternion.Euler(0, 270, 0); /// низ-право
//                        break;
//                    case 3:
//                        forceFieldQuaternion = Quaternion.Euler(0, 90, 0); /// лево-верх
//                        break;
//                    default:
//                        break;
//                }
//            }
//        }

//        if (ForceField_ON)
//        {
//            NewForceField.allForceFields.Add(Instantiate(Resources.Load(ForceFieldName), position, forceFieldQuaternion) as GameObject);
//        }
        
//    }


//    /// <summary>
//    /// Плавно двигаем объект
//    /// </summary>
//    /// <param name="start">начальная позиция объекта</param>
//    /// <param name="end">конечная позиция объекта</param>
//    /// <param name="time">время за которое он переместится</param>
//    /// <returns></returns>
//    private Vector3 LerpMoveTo(Vector3 start, Vector3 end, float time)
//    {
//        return Vector3.Lerp(start, end, Easing.Evaluate(_time / time));
//    }

//}
