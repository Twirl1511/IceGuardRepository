using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonState : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image image;
    private bool _isPressed  = false;
    public bool IsPressed = false;
    public static bool IsBlocked = false;
    private bool _flagToNextMove = true;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }
    private void Update()
    {
        if (_isPressed && _flagToNextMove)
        {
            image.fillAmount += 0.05f;
            if (image.fillAmount == 1)
            {
                IsPressed = true;
                image.fillAmount = 0f;
                _flagToNextMove = false;
                StartCoroutine(WaitForReachNextTile());
            }
        }
        else
        {
            image.fillAmount = 0f;
        }
    }


    private IEnumerator WaitForReachNextTile()
    {
        yield return new WaitForSeconds(PlayerController.TimeToReachNextTile);
        _flagToNextMove = true;
    }
}
