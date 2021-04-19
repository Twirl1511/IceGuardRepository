using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPointController : MonoBehaviour
{
    public static PathPointController singleton;
    private List<GameObject> PathObjectsArray = new List<GameObject>(50);
    void Start()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddPathPoint(GameObject pathPoint)
    {
        PathObjectsArray.Add(pathPoint);
    }
    public void ClearAllPathPoints()
    {
        PathObjectsArray.Clear();
    }
    public void RemovePathPoint(GameObject pathPoint)
    {
        PathObjectsArray.Remove(pathPoint);
    }
    public void Delete(GameObject pathPoint)
    {
        print(PathObjectsArray.Count);
        print(PathObjectsArray.IndexOf(pathPoint));
        //RemovePathPoint(pathPoint);
        //Destroy(pathPoint);
    }
}
