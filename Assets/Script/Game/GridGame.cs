using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGame : MonoBehaviour
{
    public ShapeStorage _shapeStorage;
    public int colums = 0;
    public int rows = 0;
    public  float squaresgap = 0.1f;
    public  GameObject gridsquare;
    public  Vector2 startPosition = new Vector2(0.0f,0.0f);
    public  float squareScale = 0.5f;
    public float everySquareOffset = 0.0f;

    private Vector2 _offset = new Vector2(0.0f,0.0f) ;
    private List<GameObject> _gridSquares = new List<GameObject>();  

    private void OnEnable() {
        GameEvents.CheckShapePlaced += CheckShapePlaced;
    }

    private void OnDisable() {
        GameEvents.CheckShapePlaced -= CheckShapePlaced;
    }


    private void Start() {
        CreatGrid();
    }

    private void CreatGrid(){
        SpawnGridsquares();
        SetGridSquaresPositions();
    }

    private void SpawnGridsquares()
    {
         int square_index = 0;

         for(var row = 0; row< rows ; ++row)
         {
            for( var colum= 0 ; colum < colums ; ++colum){
                _gridSquares.Add(Instantiate(gridsquare) as GameObject);// tao ra cac gridsquare sau do add vao danh sach _gridSquares ( as Gameobject la ep kieu ve doi tuong gameobject)
                _gridSquares[_gridSquares.Count -1].transform.SetParent(this.transform);// gọi đến đối tượng tượng cuối cùng trong danh sách _gridSquare , sau đó xét nó làm con của danh sách _gridsquares
                _gridSquares[_gridSquares.Count -1].transform.localScale = new Vector3(squareScale,squareScale,squareScale);
                _gridSquares[_gridSquares.Count -1].GetComponent<GridSquare>().SetImage(square_index %2 ==0);
                square_index ++;
            }
         }
    }

    private void SetGridSquaresPositions(){
        int colum_number = 0;
        int row_number = 0;
        Vector2 square_gap_number = new Vector2(0.0f,0.0f);
        bool row_moved = false;

        var square_rect = _gridSquares[0].GetComponent<RectTransform>();

        _offset.x = square_rect.rect.width * square_rect.transform.localScale.x + everySquareOffset;
        _offset.y = square_rect.rect.height * square_rect.transform.localScale.y + everySquareOffset;


        foreach(GameObject square in _gridSquares){
            if(colum_number + 1 > colums)
            {
               square_gap_number.x = 0;
               colum_number = 0;
               row_number++;
               row_moved = false;
            }
         
        

             var pos_x_offset = _offset.x * colum_number + (square_gap_number.x * squaresgap);
             var pos_y_offset = _offset.y * row_number + (square_gap_number.y * squaresgap);

            if( colum_number > 0 && colum_number % 3 == 0){
                square_gap_number.x++;
                pos_x_offset += squaresgap;
            }
             if( row_number > 0 && row_number % 3 == 0 && row_moved == false)
             {
                  row_moved = true;
                   square_gap_number.y++;
                   pos_y_offset += squaresgap;
             }

             square.GetComponent<RectTransform>().anchoredPosition = new Vector2( startPosition.x + pos_x_offset, startPosition.y - pos_y_offset);
             square.GetComponent<RectTransform>().localPosition = new Vector3( startPosition.x + pos_x_offset, startPosition.y - pos_y_offset , 0.0f);

             colum_number++;
             
        }
       
    }

    private void CheckShapePlaced(){
       // var squareIndexs = new List<int>();

        foreach (var square in _gridSquares)
        {
            var gridsquare = square.GetComponent<GridSquare>();
            if(gridsquare.UseSquare()==true){
                gridsquare.ActiveSquare(); 
            }
            // if(square.Selected && !square.SquareOccupied){
            //     squareIndexs.Add(square.SquareIndex);
            //     square.Selected = false;
            //    //gridsquare.ActiveSquare();
            // }
        }

        _shapeStorage.GetCurentSelectedShape().DeactiveShape();
    //    var currentSelectedShape = _shapeStorage.GetCurentSelectedShape();
    //    //Debug.Log(currentSelectedShape);
    //    if(currentSelectedShape == null) return;

    //    if(currentSelectedShape.data == squareIndexs.Count){
    //     foreach (var _squareIdex in squareIndexs)
    //     {
    //         _gridSquares[_squareIdex].GetComponent<GridSquare>().PlaceShapeOnBoard();
    //     }

    //     currentSelectedShape.DeactiveShape();
    //    }
    //    else{
    //     GameEvents.MoveShapeToStartPosition();
    //    }
    }

}
