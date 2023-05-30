using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridSquare : MonoBehaviour
{
     public Image normalImnage;
     public Image hooverImage;
     public Image activeImage;
     public List<Sprite> normalImages;

     public bool Selected{get ; set;}
     public int SquareIndex{get ; set;}
     public bool SquareOccupied{get ; set;}

     private void Start() {
        Selected = false;
        SquareOccupied = false;
     }

    //  public void PlaceShapeOnBoard(){
    //     ActiveSquare();
    //  }

     public void MoveShapeToStartPosition(){

     }

     public bool UseSquare(){
        return hooverImage.gameObject.activeSelf;
     }
     public void ActiveSquare(){
        hooverImage.gameObject.SetActive(false);
        activeImage.gameObject.SetActive(true);
        Selected  = true;
        SquareOccupied = true;
     }
    public void SetImage(bool setFirstImage){
        normalImnage.GetComponent<Image>().sprite = setFirstImage ? normalImages[1] : normalImages[0]; // true thì trả về hình ảnh trong mảng 1 , false thì trả về mảng 0.
    }

    private void OnTriggerEnter2D(Collider2D other) {
         hooverImage.gameObject.SetActive(true);
        // if(SquareOccupied == false){
        //     Selected = true;
        //     hooverImage.gameObject.SetActive(true);
        // }
    }

    private void OnTriggerStay2D(Collider2D other) {
         hooverImage.gameObject.SetActive(true);
        // Selected = true;
        //  if(SquareOccupied == false){
        //     hooverImage.gameObject.SetActive(true);
        //  }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
         hooverImage.gameObject.SetActive(false);
        // Selected = false;
        // if(SquareOccupied == false){
        //     hooverImage.gameObject.SetActive(false);
        //  }
    }

    
}
