using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    public static List<GameObject> PathsPoints = new List<GameObject>();
    public static Vector3[] adjacentPositionsForCell = new Vector3[4];
    public static Vector3 previousCell;
    
    private void Start()
    {
        previousCell = PlayerControllerDrawPath._startPosition;
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            if (IsCellAdjacentToPrevoiusOne(transform.position))
            {
                
                
                GameObject newPathPoint = Object.Instantiate(Resources.Load("Path"), transform.position, Quaternion.identity) as GameObject;
                PathsPoints.Add(newPathPoint);
                WichSide(previousCell, newPathPoint);
                previousCell = transform.position;
                for (int i = 0; i < PathsPoints.Count-1; i++)
                {

                    if ((PathsPoints[i].transform.position).Equals(transform.position))
                    {
                        for (int j = i + 1; j < PathsPoints.Count; j++)
                        {
                            GameObject.Destroy(PathsPoints[j].gameObject);
                        }
                        PathsPoints.RemoveRange(i + 1, PathsPoints.Count - (i + 1));
                    }
                }
            }
        }
        
        



        
    }

    public bool IsCellAdjacentToPrevoiusOne(Vector3 currentPosition)
    {
        adjacentPositionsForCell[0] = previousCell + Vector3.left;
        adjacentPositionsForCell[1] = previousCell + Vector3.left * -1;
        adjacentPositionsForCell[2] = previousCell + Vector3.forward;
        adjacentPositionsForCell[3] = previousCell + Vector3.forward * -1;

        foreach (var e in adjacentPositionsForCell)
        {
            if (e == currentPosition) return true;
        }

        return false;
    }

    public void WichSide(Vector3 previousVector3, GameObject pathPoint)
    {
        if(transform.position.x < previousVector3.x)
        {
            pathPoint.GetComponent<PathPointScript>().Left(true);
        }

        if (transform.position.x > previousVector3.x)
        {
            pathPoint.GetComponent<PathPointScript>().Right(true);
        }

        if (transform.position.z < previousVector3.z)
        {
            pathPoint.GetComponent<PathPointScript>().Forward(true);
        }

        if (transform.position.z > previousVector3.z)
        {
            pathPoint.GetComponent<PathPointScript>().Back(true);
        }
    }
}
