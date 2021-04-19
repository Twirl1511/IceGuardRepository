using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMoving : MonoBehaviour
{
    [SerializeField] private PlayerGhostScript _playerGhost;
    private void OnMouseDown()
    {
        print(this.gameObject.name);
        PathPointController.singleton.Delete(this.gameObject);


    }
    private void OnDestroy()
    {
        PathPointController.singleton.RemovePathPoint(this.gameObject);
    }
}
