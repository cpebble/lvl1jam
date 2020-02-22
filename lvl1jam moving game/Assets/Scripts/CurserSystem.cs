using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurserSystem : MonoBehaviour
{
    [SerializeField]
    LayerMask gridLayer;
    [SerializeField]
    private PlacementGrid placementGrid;
    private Transform currentObject;
    private DragAbleObject currentDragAble;
    private Vector2 objectOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Grab(){
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col;
        col = Physics2D.OverlapPoint(mousePos,gridLayer,-100f,100f);
        if(col != null && col.transform.CompareTag("DragAbleObject"))
        {
            currentObject = col.transform.parent;
            objectOffset = (Vector2)currentObject.position - mousePos;
            currentDragAble = currentObject.GetComponent<DragAbleObject>();

            if(currentObject.GetComponent<DragAbleObject>().IsOnGrid()){
                currentObject.GetComponent<DragAbleObject>().RemoveFromGrid();
                List<Cell> cells = currentDragAble.GetOverlapCells();
                foreach (Cell c in cells) { c.inUse = false; }
            }
            else
            {
                currentObject.GetComponent<DragAbleObject>().oldPosition = currentObject.transform.position;
                currentObject.GetComponent<DragAbleObject>().oldRotation = currentObject.transform.rotation;
            }
            currentDragAble = currentObject.GetComponent<DragAbleObject>();
            currentDragAble.beingMoved = true;
        }
    }
    void Drop(){
        
        if(CheckIfDropAble())
        {
            List<Cell> cells = currentDragAble.GetOverlapCells();
            foreach (Cell c in cells) { c.inUse = true; }
            currentDragAble.AddToGrid(placementGrid);
        }
        else
            currentObject.transform.position = currentDragAble.oldPosition ;
        currentDragAble.beingMoved = false;
        currentDragAble = null;
        currentObject = null;
        objectOffset = Vector2.zero;
        
    }
    bool CheckIfDropAble(){
        DragAbleObject dragobject = currentObject.GetComponent<DragAbleObject>();
        return currentObject != null && dragobject.GetOverlapCells()?.Count == dragobject.cellAmount;
    }
    void MoveObject(){
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentObject.position = (Vector3)mousePos + (Vector3)objectOffset + new Vector3(0,0,-1);
    }

    void RotateObject(){
        currentObject.Rotate(Vector3.forward * 90f);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            Grab();
        if(currentObject != null)
        {
            MoveObject();
            if(Input.GetKeyDown(KeyCode.R))
                RotateObject();
        }
        if(Input.GetMouseButtonUp(0) && currentDragAble != null)
            Drop();
    }
}
