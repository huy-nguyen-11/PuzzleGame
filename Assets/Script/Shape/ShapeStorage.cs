using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStorage : MonoBehaviour
{
    public List<ShapeData> shapeData;
    public List<Shape> shapeList;
    void Start()
    {
        foreach (var shape in shapeList)
        {
            var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
            shape.CreatShape(shapeData[shapeIndex]);
        }
    }

    public Shape GetCurentSelectedShape(){
        foreach (var shape in shapeList)
        {
            if(shape.OnStartPos() == false && shape.AnyShapeSquareActive()){
                return shape;
            }
        }

        Debug.LogError("khong co shape duoc chon!");
        return null;
    }

}
