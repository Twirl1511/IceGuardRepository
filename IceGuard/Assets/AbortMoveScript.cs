using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbortMoveScript : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject PlayerGhost;
    private Vector3 oldPosition;
    private Vector3 newPosition;

    private void OnMouseDown()
    {
        if (UIManager.GameState == UIManager.GameStates.Play)
        {
            
                Player = GameObject.FindGameObjectWithTag(Player.tag);
                PlayerGhost = GameObject.FindGameObjectWithTag(PlayerGhost.tag);
            

            oldPosition = Player.transform.position;
            StartCoroutine(NewPositionDelay());
        }


        IEnumerator NewPositionDelay()
        {
            Vector3 endPosition;
            float x = 0;
            float z = 0;
            yield return new WaitForSeconds(0.2f);
            newPosition = Player.transform.position;
            Debug.LogError("newPosition = " + newPosition);

            if(oldPosition == newPosition)
            {
                yield break;
            }
            else if(oldPosition.x == newPosition.x)
            {
                if((oldPosition.z - newPosition.z) > 0)
                {
                    z = Mathf.Floor(newPosition.z);
                }
                else
                {
                    z = Mathf.Ceil(newPosition.z);
                }
                x = oldPosition.x;
            }
            else if(oldPosition.z == newPosition.z)
            {
                if ((oldPosition.x - newPosition.x) > 0)
                {
                    x = Mathf.Floor(newPosition.x);
                }
                else
                {
                    x = Mathf.Ceil(newPosition.x);
                }
                z = oldPosition.z;
            }
            endPosition = new Vector3(x, 0, z);

            Player.GetComponent<NewPlayerController>()._startPosition = newPosition;
            Player.GetComponent<NewPlayerController>()._endPosition = endPosition;

            PlayerGhost.GetComponent<PlayerGhostScript>()._startPosition = endPosition;
            PlayerGhost.GetComponent<PlayerGhostScript>()._endPosition = endPosition;

            NewPathScript.PreviousPoint = endPosition;
            NewPathGhostScript.GhostPreviousPoint = endPosition;
            PlayerGhost.transform.position = endPosition;

            NewPathScript.ClearPathPoints();
            NewPathGhostScript.ClearPathPoints();
            GameObject[] PathPoints = GameObject.FindGameObjectsWithTag("Path");
            foreach (var e in PathPoints)
            {
                Destroy(e);
            }
        }
    }

   

}
