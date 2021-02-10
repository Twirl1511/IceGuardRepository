using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControllerDrawPath : MonoBehaviour
{
    public GameObject[] AllCells = new GameObject[36];
    public  Queue<Vector3> queuePath;
    public static bool Stop;
    public AnimationCurve Easing;
    public static float TimeToReachNextTile = 0.5f;
    public static Vector3 _startPosition;
    private Vector3 _previousPosition;
    private Vector3 _endPosition;
    public static Vector3[] adjacentPositionsForStart = new Vector3[4];
    public static List<GameObject> allForceFields = new List<GameObject>(36);
    public static List<GameObject> allPathPoints = new List<GameObject>(36);

    private float _time;
    [SerializeField] private int RotationSpeed = 1;
    void Start()
    {
        _startPosition = transform.position;
        queuePath = new Queue<Vector3>();
    }

    private void Update()
    {
        RemoveNullForceFields();
        AdjacentPositionsForStart();
        Move();
        PlayerMoveSound();
    }
    /// <summary>
    /// массив где храним смежные позиции для начала пути
    /// </summary>
    private void AdjacentPositionsForStart()
    {
        adjacentPositionsForStart[0] = _startPosition + Vector3.left;
        adjacentPositionsForStart[1] = _startPosition + Vector3.left * -1;
        adjacentPositionsForStart[2] = _startPosition + Vector3.forward;
        adjacentPositionsForStart[3] = _startPosition + Vector3.forward * -1;
    }
    private bool flagStartMoving = false;
    private void PlayerMoveSound()
    {
        if (flagStartMoving)
        {
            FindObjectOfType<SoundManager>().PlaySound(Sound.SoundName.PlayerMove);
        }
    }
    public static void RemoveNullForceFields()
    {
        if (allForceFields.Count > 0)
        {
            foreach (var e in allForceFields)
            {
                if (e == null) allForceFields.Remove(e);
            }
        }
    }
    private void Move()
    {
        /// если мы отпустили мышку/тач и в очереди движения есть куда двигаться (мы нарисовали путь)
        /// а также счетчик времени сброшен (мы закончили предыдущее движение или еще не начинали никакого),
        /// то запускаем скрипт движения
        /// if (!Input.GetMouseButton(0) && queuePath.Count > 0 && _time == 0)
        if ( queuePath.Count > 0 && _time == 0)
        {
            /// в массиве силовых полей проверяем нет ли уже тех что исчезли, если есть, то убирем их из массива


            /// в буферную векторную переменную записываем следующую координату для пути игрока

            _endPosition = queuePath.Dequeue();

            /// избавляемся от того что иногда в очередь пути добавляются координаты того же места где стоит игрок, там образуется поле и игрок получает урон 0_о
            while(_endPosition == transform.position)
            {
                _endPosition = queuePath.Dequeue();
            }

            
            flagStartMoving = true;

            //_endPosition = queuePath.Dequeue();
            //flagStartMoving = true;
        }

        /// если движение не закончено, то мы перемещаем игрока на новую позицию, движение должно быть
        /// закончено ровно за время TimeToReachNextTile
        if (flagStartMoving)
        {
            transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
            _time += Time.deltaTime;
        }
        /// когда заканчивается время движения игрок должен закончить свой путь и мы можем сказать что его новая начальная
        /// позиция равна той на которую он перемещался, также создаем энерго поле на последней позиции где он был
        /// обнуляем время
        /// flagStartMoving = false не даем больше заходить в цикл где происходит движение, считаем что оно закончено и ждем нового пути
        if (_time > TimeToReachNextTile)
        {
            
            transform.position = _endPosition;
            CreateEnergyField(_startPosition);
            Debug.Log("создали поле");
            _time = 0;
            _startPosition = transform.position;
            flagStartMoving = false;
              
        }
    }
    /// <summary>
    /// уничтожаем все точки пути
    /// </summary>
    private void DestroyAllPathPoints()
    {
        foreach (var e in allPathPoints)
        {
            Destroy(e.gameObject);
        }
    }
    /// <summary>
    /// создаем энергополя на предыдущей клетке движения и заносим их в массив allForceFields
    /// </summary>
    /// <param name="position">позиция где будет создано поле</param>
    private void CreateEnergyField(Vector3 position)
    {
        FindObjectOfType<SoundManager>().PlaySoundOneShot(Sound.SoundName.ForceFieldCreate);
        allForceFields.Add(Instantiate(Resources.Load("force_field"), position, Quaternion.identity) as GameObject);

    }




    /// <summary>
    /// Плавно двигаем объект
    /// </summary>
    /// <param name="start">начальная позиция объекта</param>
    /// <param name="end">конечная позиция объекта</param>
    /// <param name="time">время за которое он переместится</param>
    /// <returns></returns>
    private Vector3 LerpMoveTo(Vector3 start, Vector3 end, float time)
    {
        return Vector3.Lerp(start, end, Easing.Evaluate(_time / time));
    }

}
