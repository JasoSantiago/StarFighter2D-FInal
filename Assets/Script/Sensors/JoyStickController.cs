using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image JoyStickParent;
    public Image JoyStick;
    public Image GamePanel;

    private Vector2 JoyStickVector;

    [SerializeField] private float Speed = 5.0f;

    [SerializeField] private GameObject player;


    private void FixedUpdate()
    {
        if (player != null)
        {
            float change = JoyStickVector.x * Speed * Time.deltaTime;

            float leftBound = GamePanel.transform.position.x - GamePanel.rectTransform.rect.width / 2 ;
            float rightBound = GamePanel.transform.position.x + GamePanel.rectTransform.rect.width / 2;

            SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();

            float spriteBound = 75/2;
            

            //Debug.Log($"SpriteBound {spriteBound}");

            if ((change < 0 && player.transform.localPosition.x - spriteBound + change > leftBound) || 
                change > 0 && player.transform.localPosition.x + spriteBound + change < rightBound)
            {
                player.transform.Translate(JoyStickVector.x * Speed * Time.deltaTime, 0, 0);
            }
            else if (change < 0)
            {
                Vector3 temp = player.transform.localPosition;
                temp.x = leftBound + spriteBound;
                player.transform.localPosition = temp;
            }
            else if (change > 0)
            {
                Vector3 temp = player.transform.localPosition;
                temp.x = rightBound - spriteBound;
                player.transform.localPosition = temp;
            }


        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 locPos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(JoyStickParent.rectTransform, eventData.position,
             eventData.pressEventCamera, out locPos))
        {
            float half_w = JoyStickParent.rectTransform.rect.width / 2;
            float half_h = JoyStickParent.rectTransform.rect.height / 2;
            float x = locPos.x / half_w;
            float y = locPos.y / half_h;

            JoyStickVector = new Vector2(x, y);

            if(JoyStickVector.magnitude > 1) JoyStickVector.Normalize();

            JoyStick.rectTransform.localPosition = new Vector2(JoyStickVector.x * half_w, JoyStickVector.y * half_h);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        JoyStickVector = JoyStick.rectTransform.localPosition = Vector2.zero;
    }
}
