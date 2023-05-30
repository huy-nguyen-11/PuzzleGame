using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shape : MonoBehaviour , IPointerClickHandler , IPointerUpHandler , IBeginDragHandler , IDragHandler , IEndDragHandler , IPointerDownHandler
{
    public GameObject squareShapeImage ;
    public Vector3 shapeSelectedScale ;
    public Vector2 offset = new Vector2(0.0f , 700.0f);
    
    [HideInInspector]
    public ShapeData currentShapedata ;

    public int data {get;set;}

    [SerializeField] int size = 100;
    private bool shapeDragable = true ;
    private Canvas _canvas;
    private Vector3 startPos;
    private bool _shapeActicve = true;
     private Vector3 _shapeStartScale;
     private RectTransform _rectTransform;

    private List<GameObject> currentShape = new List<GameObject>();

    public void Awake() {
        _shapeStartScale = this.GetComponent<RectTransform>().localScale;
        _rectTransform = this.GetComponent<RectTransform>();
        _canvas = this.GetComponentInParent<Canvas>();
         shapeDragable = true ;
         startPos = _rectTransform.localPosition;
        _shapeActicve = true;
        
    }
    // private void Start() {
    //     if(startPos==endPos){
    //         DeactiveShape();
    //     }
    // }
    

    // private void OnEnable(){
    //     GameEvents.MoveShapeToStartPosition += MoveShapeToStartPosition;
    // }

    // private void OnDisable(){
    //     GameEvents.MoveShapeToStartPosition -= MoveShapeToStartPosition;
    // }
     public bool OnStartPos(){
        return _rectTransform.localPosition == startPos ;
        }

    public bool AnyShapeSquareActive(){
        foreach (var square in currentShape)
        {
            if(square.gameObject.activeSelf){
                return true;
            }
        }
        return false;
    }

     public void DeactiveShape(){
       if(_shapeActicve){
        foreach (var square in currentShape)
        {
            square?.GetComponent<ShapeSquare>().DeactivateShape();//check d != null then all method deactivate    
        }
       }
       _shapeActicve = false;
    }

    public void ActiveShape(){
        if(!_shapeActicve){
        foreach (var square in currentShape)
        {
            square?.GetComponent<ShapeSquare>().ActivateShape();
        }
       }
       _shapeActicve = true;
    }    


   
    public void RequestNewShape(ShapeData shapeData){
        _rectTransform.localPosition = startPos;
        CreatShape(shapeData);
    }

    public void CreatShape(ShapeData shapeData){
        currentShapedata = shapeData;
        var data = shapeData.GetBlocks();
        foreach(var b in data)
        {
            var _gameobject = Instantiate(squareShapeImage, transform);
            var _transform = _gameobject.GetComponent<RectTransform>();
            _transform.anchoredPosition = new Vector2(b.x * size, -b.y * size);
        }
    //     var totalSquareNumber = GetNumberOfSquares(shapeData);
    //     while (currentShape.Count <= totalSquareNumber){
    //         currentShape.Add(Instantiate(squareShapeImage,transform) as GameObject);
    //     }
    //    foreach (var square in currentShape){
    //     square.gameObject.transform.position = Vector3.zero;
    //     square.gameObject.SetActive(false);
    //    }
    //    var squareRect = squareShapeImage.GetComponent<RectTransform>();
    //    var moveDistance = new Vector2(squareRect.rect.width * squareRect.localScale.x , squareRect.rect.height * squareRect.localScale.y);

    //    int currentIndexInlist = 0 ;

    //    for( var row=0 ; row< shapeData.rows; row++){
    //     for(var column =0 ;column <shapeData.columns ; column++){
    //         if(shapeData.board[row].column[column]){
    //             currentShape[currentIndexInlist].SetActive(true);
    //             currentShape[currentIndexInlist].GetComponent<RectTransform>().localPosition = new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance), GetYPostionForShapeSquare(shapeData, row, moveDistance));

    //             currentIndexInlist++;
    //         }
    //     }
    //    }

    }
    
//     private float GetYPostionForShapeSquare(ShapeData shapeData , int row , Vector2 moveDistance){
//         float shiftOnY = 0f;
//      if(shapeData.rows > 1){
//         if(shapeData.rows %2 !=0){
//             var mildSquareIndex = (shapeData.rows - 1)/2 ;
//             var multiplier = (shapeData.rows - 1)/2 ;
//             if(row < mildSquareIndex){
//                 shiftOnY = moveDistance.y * 1;
//                 shiftOnY  *= multiplier;
//             }
//             else if(row > mildSquareIndex){
//                 shiftOnY = moveDistance.y *-1;
//                 shiftOnY *= multiplier;
//             }
//         }
//     }
//     else{
//         var mildSquareIndex2 = (shapeData.rows ==2) ? 1 : (shapeData.rows /2);
//         var mildSquareIndex1 = (shapeData.rows ==2) ? 0 : (shapeData.rows - 2);
//         var multiplier = shapeData.rows/2;

//         if(row == mildSquareIndex1 || row == mildSquareIndex2){
//             if(row == mildSquareIndex2){
//                   shiftOnY = (moveDistance.y /2) *-1;
//             }
//             if( row== mildSquareIndex1){
//                 shiftOnY = moveDistance.y /2;
//             }

//         if(row < mildSquareIndex1 && row <mildSquareIndex2){
//             shiftOnY = moveDistance.y * 1;
//             shiftOnY *= multiplier;
//         }
//         else if(row> mildSquareIndex1 && row > mildSquareIndex2){
//             shiftOnY = moveDistance.y * -1;
//             shiftOnY *= multiplier;
//         }
          
//         }
//     }
//     return shiftOnY;
//     }


//     private float GetXPositionForShapeSquare(ShapeData shapeData , int column , Vector2 moveDistance)
// {
//     float shiftOnX = 0f;
//     if(shapeData.columns > 1){
//         if(shapeData.columns %2 !=0){
//             var mildSquareIndex = (shapeData.columns - 1)/2 ;
//             var multiplier = (shapeData.columns - 1)/2 ;
//             if(column < mildSquareIndex){
//                 shiftOnX = moveDistance.x * -1;
//                 shiftOnX  *= multiplier;
//             }
//             else if(column > mildSquareIndex){
//                 shiftOnX = moveDistance.x *1;
//                 shiftOnX *= multiplier;
//             }
//         }
//     }
//     else{
//         var mildSquareIndex2 = (shapeData.columns ==2) ? 1 : (shapeData.columns /2);
//         var mildSquareIndex1 = (shapeData.columns ==2) ? 0 : (shapeData.columns - 1);
//         var multiplier = shapeData.columns/2;

//         if(column == mildSquareIndex1 || column == mildSquareIndex2){
//             if(column == mildSquareIndex2){
//                   shiftOnX = moveDistance.x /2;
//             }
//             if( column == mildSquareIndex1){
//                 shiftOnX = (moveDistance.x /2) * -1;
//             }

//         if(column < mildSquareIndex1 && column <mildSquareIndex2){
//             shiftOnX = moveDistance.x * -1;
//             shiftOnX *= multiplier;
//         }
//         else if(column > mildSquareIndex1 && column > mildSquareIndex2){
//             shiftOnX = moveDistance.x * 1;
//             shiftOnX *= multiplier;
//         }
          
//         }
//     }
//     return shiftOnX;
// }


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

       public void OnPointerClick(PointerEventData eventData){

      }

       public void OnPointerUp(PointerEventData eventData){

       }
       public void OnBeginDrag(PointerEventData eventData){
        this.GetComponent<RectTransform>().localScale = shapeSelectedScale;
       }
       public void OnDrag(PointerEventData eventData){
        _rectTransform.anchorMin = new Vector2(0,0);
        _rectTransform.anchorMax = new Vector2(0,0);
        _rectTransform.pivot     = new Vector2(0,0);
        Vector2 _pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, eventData.position, Camera.main, out _pos);
        _rectTransform.localPosition = _pos + offset ;
       }
       public void OnEndDrag(PointerEventData eventData){
        this.GetComponent<RectTransform>().localScale = shapeSelectedScale;
        GameEvents.CheckShapePlaced();
       }
       public void OnPointerDown(PointerEventData eventData){

       }
    
    // private void MoveShapeToStartPosition(){
    //     _rectTransform.transform.localPosition = startPos;
    // }
      

   
}
