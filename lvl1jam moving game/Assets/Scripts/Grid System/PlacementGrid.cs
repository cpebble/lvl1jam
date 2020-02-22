using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementGrid : MonoBehaviour
{
    [SerializeField]
    Cell[] cells;
    [SerializeField]
    List<DragAbleObject> heldObjects = new List<DragAbleObject>();

    //[SerializeField]
    //private Color canPlaceHighlightColor, cantPlaceHighlightColor, defaultColor;


    public void Color(Cell cells){

    }
    public void Place(DragAbleObject obj,LayerMask gridLayer)
    {
        Cell c = obj.GetCell(0);
        Collider2D col;
        col = Physics2D.OverlapPoint(c.transform.position, gridLayer, -100f, 100f);
        Cell gridCell = null;
        if (col != null && col.transform.CompareTag("PlacementGrid"))
        {
            gridCell = col.GetComponent<Cell>();
        }
        else
            return;
        obj.transform.position = obj.transform.position + (gridCell.transform.position - c.transform.position);
        obj.transform.position += new Vector3(0,0,-1);
        AddObject(obj.GetComponent<DragAbleObject>());
    }
    public void RemoveObject(DragAbleObject obj)
    {
        if (heldObjects.Contains(obj))
        {
            heldObjects.Remove(obj);
            obj.transform.parent = null;
        }
    }
    public void AddObject(DragAbleObject obj)
    {
        if (!heldObjects.Contains(obj)){
            heldObjects.Add(obj);
            obj.transform.parent = this.transform;
        }
    }
    

    // Update is called once per frame
    void Update()
    {

    }
}
