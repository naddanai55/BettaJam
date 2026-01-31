using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject cardObject;
    [SerializeField] Transform cardSpawnPoint;
    private List<List<bool>> status = new List<List<bool>>();
    private List<List<bool>> Modifiers = new List<List<bool>>();
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

    void spawnCard()
    {
        for (int i = 0; i < 5; i++)
        {
            int offset = i * 5;

            GameObject newcard = Instantiate(cardObject, cardSpawnPoint.position + new Vector3(offset, 0, 0), Quaternion.identity);
            newcard.GetComponent<Card>().currentShape = (Card.ShapeType)Random.Range(0, 2);
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
        List<List<bool>> newStatus = statusManager.ApplyModifiers(status, Modifiers, "OR");
        cellManager.UpdateCellColor(newStatus);
    }

    // void SpawnCard()
    // {
    //     // Instantiate(cardObject, Vector3.zero, Quaternion.identity);
    // }
}
