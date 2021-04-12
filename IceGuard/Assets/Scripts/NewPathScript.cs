using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPathScript : MonoBehaviour
{
    public static List<Vector3> PathPoints = new List<Vector3>();
    public static Vector3 PreviousPoint;
    

    private void OnMouseDown()
    {
        if(PauseManager.GameState == PauseManager.GameStates.Play)
        {
            if (CorrectNextPathPoint(PreviousPoint, gameObject.transform.position))
            {
                PathPoints.Add(gameObject.transform.position);
                PreviousPoint = gameObject.transform.position;
            }
        }
    }

    public bool CorrectNextPathPoint(Vector3 previousPosition, Vector3 newPoint)
    {
        bool flag = false;
        float x = previousPosition.x;
        float z = previousPosition.z;
        if (x == newPoint.x || z == newPoint.z)
        {
            flag = true;
        }
        return flag;
    }

    public static Vector3 GetPathPoint()
    {
        Vector3 pathPoint = PathPoints[0];
        PathPoints.RemoveAt(0);
        return pathPoint;
    }

    public static void ClearPathPoints()
    {
        PathPoints.Clear();
    }
  

    
}
