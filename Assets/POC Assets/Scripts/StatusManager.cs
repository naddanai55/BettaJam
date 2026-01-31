using UnityEngine;
using System.Collections.Generic;

public class StatusManager : MonoBehaviour
{
    public List<List<bool>> ApplyModifiers(List<List<bool>> status, List<List<bool>> modifiers, string operation)
    {
        List<List<bool>> newStatus = new List<List<bool>>();

        switch (operation)
        {
            case "AND":
                for (int y = 0; y < status.Count; y++)
                {
                    List<bool> newRow = new List<bool>();
                    for (int x = 0; x < status[y].Count; x++)
                    {
                        newRow.Add(status[y][x] && modifiers[y][x]);
                    }
                    newStatus.Add(newRow);
                }
                break;

            case "OR":
                for (int y = 0; y < status.Count; y++)
                {
                    List<bool> newRow = new List<bool>();
                    for (int x = 0; x < status[y].Count; x++)
                    {
                        newRow.Add(status[y][x] || modifiers[y][x]);
                    }
                    newStatus.Add(newRow);
                }
                break;

            default:
                Debug.LogError("Unsupported operation: " + operation);
                return status;
        }

        return newStatus;
    }
}