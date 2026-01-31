using UnityEngine;
using Random = UnityEngine.Random;
using System;
using System.Collections.Generic;

public class Card : MonoBehaviour
{
    public enum ShapeType{ShapeCross, ShapeBox, ShapeX}
    public ShapeType currentShape;
    private Vector3 defaultPos;
    public bool isUsed = false;

    public List<List<bool>> GetShapeMatrix()
    {
        switch (currentShape)
        {
            case ShapeType.ShapeCross:
                return new List<List<bool>>
                {
                    new() {false,false,false,false,true ,false,false,false,false,false},
                    new() {false,false,false,false,true ,false,false,false,false,false},
                    new() {false,false,false,false,true ,false,false,false,false,false},
                    new() {false,false,false,false,true ,false,false,false,false,false},
                    new() {true ,true ,true ,true ,true ,true ,true ,true ,true ,true },
                    new() {false,false,false,false,true ,false,false,false,false,false},
                    new() {false,false,false,false,true ,false,false,false,false,false},
                    new() {false,false,false,false,true ,false,false,false,false,false},
                    new() {false,false,false,false,true ,false,false,false,false,false},
                    new() {false,false,false,false,true ,false,false,false,false,false}
                };

            case ShapeType.ShapeBox:
                return new List<List<bool>>
                {
                    new() {false,false,false,false,false,false,false,false,false,false},
                    new() {false,false,false,false,false,false,false,false,false,false},
                    new() {false,false,false,false,false,false,false,false,false,false},
                    new() {false,false,false,true ,true ,true ,true ,false,false,false},
                    new() {false,false,false,true ,true ,true ,true ,false,false,false},
                    new() {false,false,false,true ,true ,true ,true ,false,false,false},
                    new() {false,false,false,true ,true ,true ,true ,false,false,false},
                    new() {false,false,false,false,false,false,false,false,false,false},
                    new() {false,false,false,false,false,false,false,false,false,false},
                    new() {false,false,false,false,false,false,false,false,false,false}
                };

            case ShapeType.ShapeX:
                return new List<List<bool>>
                {
                    new() {true ,false,false,false,false,false,false,false,false,true },
                    new() {false,true ,false,false,false,false,false,false,true ,false},
                    new() {false,false,true ,false,false,false,false,true ,false,false},
                    new() {false,false,false,true ,false,false,true ,false,false,false},
                    new() {false,false,false,false,true ,true ,false,false,false,false},
                    new() {false,false,false,false,true ,true ,false,false,false,false},
                    new() {false,false,false,true ,false,false,true ,false,false,false},
                    new() {false,false,true ,false,false,false,false,true ,false,false},
                    new() {false,true ,false,false,false,false,false,false,true ,false},
                    new() {true ,false,false,false,false,false,false,false,false,true }
                };

            default:
                return new List<List<bool>>();
        }
    }

    public void setNewCard()
    {
        currentShape = (ShapeType)Random.Range(0, Enum.GetNames(typeof(ShapeType)).Length);
    }

    public void cardUp() 
    {
        gameObject.transform.position = defaultPos + (Vector3.up * 1f);
    }

    public void cardDown() {
        if(isUsed) return;
        // gameObject.transform.position += Vector3.up * -5f;
        // Debug.Log();
        gameObject.transform.position = defaultPos;
    }

    public void useCard()
    {
        isUsed = true;
        gameObject.transform.position = defaultPos + (Vector3.up * -10f);
    }

    void Start()
    {
        List<List<bool>> myShape = GetShapeMatrix();
    }

    void Awake() {
        defaultPos = gameObject.transform.position;
    }
}