using UnityEngine;
using System.Collections.Generic;

public class Card : MonoBehaviour
{
    public enum ShapeType{ShapeCross, ShapeBox}
    public ShapeType currentShape;
    private Vector3 defaultPos;

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

            default:
                return new List<List<bool>>();
        }
    }

    public void cardUp() {
        gameObject.transform.position += Vector3.up * 1f;
    }

    public void cardDown() {
        // gameObject.transform.position += Vector3.up * -5f;
        // Debug.Log();
        gameObject.transform.position = defaultPos;
    }

    void Start()
    {
        List<List<bool>> myShape = GetShapeMatrix();
    }

    void Awake() {
        defaultPos = gameObject.transform.position;
    }
}