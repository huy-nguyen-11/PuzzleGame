using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public GameObject squareShapeImage ;
    //[HideInInspector]
    public ShapeData currentShapedata ;

    private List<GameObject> currentShape = new List<GameObject>();
   
    void Start()
    {
        RequestNewShape(currentShapedata);
    }

    public void RequestNewShape(ShapeData shapeData){
        CreatShape(shapeData);
    }

    public void CreatShape(ShapeData shapeData){
        currentShapedata = shapeData;
        var totalSquareNumber = GetNumberOfSquares(shapeData);
        while (currentShape.Count <= totalSquareNumber){
            currentShape.Add(Instantiate(squareShapeImage,transform) as GameObject);
        }
       foreach (var square in currentShape){
        square.gameObject.transform.position = Vector3.zero;
        square.gameObject.SetActive(false);
       }
       var squareRect = squareShapeImage.GetComponent<RectTransform>();
       var moveDistance = new Vector2(squareRect.rect.width * squareRect.localScale.x , squareRect.rect.height * squareRect.localScale.y);

       int currentIndexInlist = 0 ;

       for( var row=0 ; row< shapeData.rows; row++){
        for(var column =0 ;column <shapeData.columns ; column++){
            if(shapeData.board[row].column[column]){
                currentShape[currentIndexInlist].SetActive(true);
                currentShape[currentIndexInlist].GetComponent<RectTransform>().localPosition = new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance), GetYPostionForShapeSquare(shapeData, row, moveDistance));

                currentIndexInlist++;
            }
        }
       }

    }
    
    private float GetYPostionForShapeSquare(ShapeData shapeData , int row , Vector2 moveDistance){
        float shiftOnY = 0f;
     if(shapeData.rows > 1){
        if(shapeData.rows %2 !=0){
            var mildSquareIndex = (shapeData.rows - 1)/2 ;
            var multiplier = (shapeData.rows - 1)/2 ;
            if(row < mildSquareIndex){
                shiftOnY = moveDistance.y * 1;
                shiftOnY  *= multiplier;
            }
            else if(row > mildSquareIndex){
                shiftOnY = moveDistance.y *-1;
                shiftOnY *= multiplier;
            }
        }
    }
    else{
        var mildSquareIndex2 = (shapeData.rows ==2) ? 1 : (shapeData.rows /2);
        var mildSquareIndex1 = (shapeData.rows ==2) ? 0 : (shapeData.rows - 2);
        var multiplier = shapeData.rows/2;

        if(row == mildSquareIndex1 || row == mildSquareIndex2){
            if(row == mildSquareIndex2){
                  shiftOnY = (moveDistance.y /2) *-1;
            }
            if( row== mildSquareIndex1){
                shiftOnY = moveDistance.y /2;
            }

        if(row < mildSquareIndex1 && row <mildSquareIndex2){
            shiftOnY = moveDistance.y * 1;
            shiftOnY *= multiplier;
        }
        else if(row> mildSquareIndex1 && row > mildSquareIndex2){
            shiftOnY = moveDistance.y * -1;
            shiftOnY *= multiplier;
        }
          
        }
    }
    return shiftOnY;
    }


    private float GetXPositionForShapeSquare(ShapeData shapeData , int column , Vector2 moveDistance)
{
    float shiftOnX = 0f;
    if(shapeData.columns > 1){
        if(shapeData.columns %2 !=0){
            var mildSquareIndex = (shapeData.columns - 1)/2 ;
            var multiplier = (shapeData.columns - 1)/2 ;
            if(column < mildSquareIndex){
                shiftOnX = moveDistance.x * -1;
                shiftOnX  *= multiplier;
            }
            else if(column > mildSquareIndex){
                shiftOnX = moveDistance.x *1;
                shiftOnX *= multiplier;
            }
        }
    }
    else{
        var mildSquareIndex2 = (shapeData.columns ==2) ? 1 : (shapeData.columns /2);
        var mildSquareIndex1 = (shapeData.columns ==2) ? 0 : (shapeData.columns - 1);
        var multiplier = shapeData.columns/2;

        if(column == mildSquareIndex1 || column == mildSquareIndex2){
            if(column == mildSquareIndex2){
                  shiftOnX = moveDistance.x /2;
            }
            if( column == mildSquareIndex1){
                shiftOnX = (moveDistance.x /2) * -1;
            }

        if(column < mildSquareIndex1 && column <mildSquareIndex2){
            shiftOnX = moveDistance.x * -1;
            shiftOnX *= multiplier;
        }
        else if(column > mildSquareIndex1 && column > mildSquareIndex2){
            shiftOnX = moveDistance.x * 1;
            shiftOnX *= multiplier;
        }
          
        }
    }
    return shiftOnX;
}


    private int GetNumberOfSquares(ShapeData shapeData)
    {
        int number = 0;
        foreach( var rowData in shapeData.board){
            foreach( var active in rowData.column){
                if(active)
                number++;
            }
        }
        return number;
    }

   
}
