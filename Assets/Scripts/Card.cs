using UnityEngine;
using Random = UnityEngine.Random;
using System;
using System.Collections.Generic;

[Serializable]
public class ShapeDataWrapper { public List<ShapeDefinition> shapes; }

[Serializable]
public class ShapeDefinition { public string shapeName; public List<RowWrapper> rows; }

[Serializable]
public class RowWrapper { public List<bool> cols; }

public class Card : MonoBehaviour
{
    [SerializeField] GameObject borderSprite;
    [SerializeField] List<GameObject> symbolSprite = new List<GameObject>();
    public enum ShapeType { ShapeCross, ShapeBox1, ShapeBox2, ShapeBox3, ShapeX, ShapeBorder }
    public ShapeType currentShape;
    private Vector3 defaultPos;
    public bool isUsed = false;
    private static Dictionary<ShapeType, List<List<bool>>> cachedShapes;

    void Awake()
    {
        defaultPos = gameObject.transform.position;

        if (cachedShapes == null)
        {
            LoadShapeData();
        }
    }

    void Start()
    {
        List<List<bool>> myShape = GetShapeMatrix();
    }

    private void LoadShapeData()
    {
        cachedShapes = new Dictionary<ShapeType, List<List<bool>>>();

        TextAsset jsonFile = Resources.Load<TextAsset>("card_shapes");
        if (jsonFile == null) { Debug.LogError("JSON file not found!"); return; }

        ShapeDataWrapper data = JsonUtility.FromJson<ShapeDataWrapper>(jsonFile.text);

        foreach (var item in data.shapes)
        {
            // --- THE MAGIC PART ---
            // Try to convert the String from JSON into your Enum
            if (Enum.TryParse(item.shapeName, out ShapeType typeEnum))
            {
                // Convert rows/cols wrappers to List<List<bool>>
                List<List<bool>> matrix = new List<List<bool>>();
                foreach (var row in item.rows)
                {
                    matrix.Add(row.cols);
                }

                // Add to dictionary using the Enum as the Key
                if (!cachedShapes.ContainsKey(typeEnum))
                {
                    cachedShapes.Add(typeEnum, matrix);
                }
            }
            else
            {
                Debug.LogWarning($"JSON contains shape '{item.shapeName}' which is not in the C# Enum. Skipping.");
            }
        }
    }
    public List<List<bool>> GetShapeMatrix()
    {
        // 4. Look up data using the Enum
        if (cachedShapes != null && cachedShapes.ContainsKey(currentShape))
        {
            // Return a copy of the list
            return new List<List<bool>>(cachedShapes[currentShape]);
        }

        Debug.LogError($"Data for {currentShape} not found in JSON!");
        return new List<List<bool>>();
    }

    public void setNewCard()
    {
        // 5. Logic remains the same, picking a random Enum
        currentShape = (ShapeType)Random.Range(0, Enum.GetNames(typeof(ShapeType)).Length);

        // Debug.Log((int)currentShape);

        symbolSprite[(int)currentShape].SetActive(true);
        borderSprite.GetComponent<Renderer>().material.color = Color.red;

    }


    public void cardUp()
    {
        if (isUsed) return;

        Vector3 pos = defaultPos + (Vector3.up * 1f);
        if (gameObject.transform.position == pos) gameObject.transform.position = defaultPos;
        else gameObject.transform.position = pos + (Vector3.up * 1f);

    }

    public void updateCardMode(string cardMode)
    {
        if (cardMode != "ADD")
        {
            borderSprite.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            borderSprite.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    public void cardDown()
    {
        if (isUsed) return;
        gameObject.transform.position = defaultPos;
    }

    public void useCard()
    {
        isUsed = true;
        gameObject.transform.position = defaultPos + (Vector3.up * -10f);
    }
}