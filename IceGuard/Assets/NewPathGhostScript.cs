using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPathGhostScript : MonoBehaviour
{
    public static List<Vector3> GhostPathPoints = new List<Vector3>();
    public static Vector3 GhostPreviousPoint;

    private void OnMouseDown()
    {
        if (CorrectNextPathPoint(GhostPreviousPoint, gameObject.transform.position))
        {
            GhostPathPoints.Add(gameObject.transform.position);
            GhostPreviousPoint = gameObject.transform.position;
            Debug.Log(GhostPathPoints.Count);
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
        Vector3 pathPoint = GhostPathPoints[0];
        GhostPathPoints.RemoveAt(0);
        return pathPoint;
    }

}
