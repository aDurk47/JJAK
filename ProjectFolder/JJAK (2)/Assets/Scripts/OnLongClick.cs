using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnLongClick : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float pointerDownTimer;
    public BattleSystem bs;
    public GameUI gameUI;
    public int input;
    public GameObject des;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    void Update()
    {
        if(pointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if(pointerDownTimer > .5f)
            {
                if(bs != null) bs.ChangeDescription(input);
                if(gameUI != null) gameUI.ToggleDescription(input);
                des.SetActive(true);
            }
        }
    }

    void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0;
        des.SetActive(false);
    }
}
