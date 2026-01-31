using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private List<List<bool>> status = new List<List<bool>>();
    private List<List<bool>> Modifiers = new List<List<bool>>();
    StatusManager statusManager;
    CellManager cellManager;
    void Awake()
    {
        cellManager = GetComponent<CellManager>();
        statusManager = GetComponent<StatusManager>();
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
}
