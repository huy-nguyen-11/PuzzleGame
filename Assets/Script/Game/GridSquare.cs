using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridSquare : MonoBehaviour
{
     public Image normalImnage;
     public List<Sprite> normalImages; 

    public void SetImage(bool setFirstImage){
        normalImnage.GetComponent<Image>().sprite = setFirstImage ? normalImages[1] : normalImages[0]; // true thì trả về hình ảnh trong mảng 1 , false thì trả về mảng 0.
    }
}
