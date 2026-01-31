using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour, IPointerClickHandler
{
    Card card;
    GameManager gameManager;
    void Awake()
    {
        card = GetComponent<Card>();
        gameManager = FindFirstObjectByType<GameManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            gameManager.GetCardShapeMask(card.GetShapeMatrix());
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            gameManager.GetCardShapeUnMask(card.GetShapeMatrix());
        }
    }
}