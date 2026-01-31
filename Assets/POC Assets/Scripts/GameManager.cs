using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject cardObject;
    [SerializeField] Transform cardSpawnPoint;

    private List<List<bool>> status = new List<List<bool>>();
    private List<List<bool>> statusPreview = new List<List<bool>>();
    private List<List<bool>> Modifiers = new List<List<bool>>();

    private List<GameObject> hand = new List<GameObject>();
    private int selectedCardIndex = -1;
    private int handSize = 5;

    public string currentCardMode = "ADD";

    StatusManager statusManager;
    CellManager cellManager;
    Card card;

    void Awake()
    {
        cellManager = GetComponent<CellManager>();
        statusManager = GetComponent<StatusManager>();
        card = FindFirstObjectByType<Card>();
    }

    void Start()
    {
        for (int y = 0; y < 10; y++)
        {
            List<bool> row = new List<bool>();

            for (int x = 0; x < 10; x++)
            {
                row.Add(false);

            }
            status.Add(row);
        }
        Debug.Log(status.Capacity);
        spawnCard();

    }

    void onCardSelect(int cardIndex, List<List<bool>> cardModifier) {
        selectedCardIndex = cardIndex;
        updatePreview(cardModifier);
    }

    void updatePreview(List<List<bool>> cardModifier) {
        
        // Get mode
        var mode = "OR";
        if (currentCardMode != "ADD") {
            mode = "AND";
        }

        // Apply modifier to preview
        statusPreview = statusManager.ApplyModifiers(status, cardModifier, mode);
        cellManager.UpdateCellColor(statusPreview);

        for (int i = 0; i < handSize; i++) {
            if (selectedCardIndex >= 0 && i == selectedCardIndex) {
                hand[i].GetComponent<Card>().cardUp();
            }
            else
            {
                hand[i].GetComponent<Card>().cardDown();
            }
        }
    }

    void spawnCard()
    {
        for (int i = 0; i < handSize; i++)
        {
            int offset = i * 5;

            GameObject newcard = Instantiate(cardObject, cardSpawnPoint.position + new Vector3(offset, 0, 0), Quaternion.identity);
            newcard.GetComponent<Card>().currentShape = (Card.ShapeType)Random.Range(0, 2);

            List<List<bool>> shapeMatrix = newcard.GetComponent<Card>().GetShapeMatrix();

            // Add clicker to Object
            var clicker = newcard.AddComponent<ClickCallback>();
            var cardIndex = i;
            clicker.OnClickAction = () => onCardSelect(cardIndex, shapeMatrix);

            // Add card to hand
            hand.Add(newcard);
        }

    }

    public void GetCardShapeMask(List<List<bool>> shape)
    {
        Modifiers = shape;
        Debug.Log(Modifiers.Capacity);

    }

    public void GetCardShapeUnMask(List<List<bool>> shape)
    {
        Modifiers = shape;
        Debug.Log(Modifiers);


    }

    public void UpdateGameStatus()
    {
        // List<List<bool>> newStatus = statusManager.ApplyModifiers(status, Modifiers, "OR");
        status = statusPreview;
        cellManager.UpdateCellColor(status);
    }

}
