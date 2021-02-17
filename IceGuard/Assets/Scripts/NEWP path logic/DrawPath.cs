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
                previousCell = transform.position;

                PathsPoints.Add(Object.Instantiate(Resources.Load("Path"), transform.position, Quaternion.identity) as GameObject);
                for (int i = 0; i < PathsPoints.Count-1; i++)
                {
                    Debug.Log("1111111");
                    if ((PathsPoints[i].transform.position).Equals(transform.position))
                    {
                        for (int j = i + 1; j < PathsPoints.Count; j++)
                        {
                            GameObject.Destroy(PathsPoints[j].gameObject);
                        }

                        Debug.Log("222222");
                        PathsPoints.RemoveRange(i + 1, PathsPoints.Count - (i + 1));
                        
                        //PathsPoints.RemoveRange(i + 1, PathsPoints.Count);
                        Debug.Log("33333");
                    }
                }

                Debug.Log(PathsPoints.Count);
                Debug.Log(previousCell);
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
}
