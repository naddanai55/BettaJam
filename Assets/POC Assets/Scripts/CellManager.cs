using UnityEngine;
using System.Collections.Generic;

public class CellManager : MonoBehaviour
{
    [SerializeField] GameObject cellPrefab;
    [SerializeField] int cellX = 10;
    [SerializeField] int cellY = 10;
    [SerializeField] float cellSize = 1f;
    [SerializeField] Transform spawnPoint;
    private List<List<GameObject>> cells = new List<List<GameObject>>();

    void Start()
    {
        InitializeCells();
    }

    private void InitializeCells()
    {
        for (int y = 0; y < cellY; y++)
        {
            List<GameObject> rowCell = new List<GameObject>();

            for (int x = 0; x < cellX; x++)
            {
                Vector3 pos = new Vector3(x * cellSize, y * cellSize, 0f);
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
                cell.GetComponent<Renderer>().material.color = Color.red;
                rowCell.Add(cell);
            }
            cells.Add(rowCell);
        }
    }

    public void UpdateCellColor(List<List<bool>> newstatus)
    {
        for (int y = 0; y < cellY; y++)
        {
            for (int x = 0; x < cellX; x++)
            {
                if (newstatus[y][x])
                {
                    cells[y][x].GetComponent<Renderer>().material.color = Color.green;
                }
                else
                {
                    cells[y][x].GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
    }
}