using UnityEngine;
using System.Collections.Generic;

public class Card : MonoBehaviour
{
    public enum ShapeType{ShapeCross, ShapeBox}
    public ShapeType currentShape;
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
    void Start()
    {
        List<List<bool>> myShape = GetShapeMatrix();
    }
}