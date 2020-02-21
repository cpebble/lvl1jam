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
            if(currentObject.GetComponent<DragAbleObject>().IsOnGrid()){
                currentObject.GetComponent<DragAbleObject>().RemoveFromGrid();
            }
            currentDragAble = currentObject.GetComponent<DragAbleObject>();
        }
    }
    void Drop(){
        if(CheckIfDropAble())
        {
            currentDragAble.AddToGrid(placementGrid);   
        }
        currentObject = null;
        objectOffset = Vector2.zero;
        
    }
    bool CheckIfDropAble(){
        return currentObject.GetComponent<DragAbleObject>().CheckIfOnGrid();
    }
    void MoveObject(){
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentObject.position = mousePos + objectOffset;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            Grab();
        if(currentObject != null)
        {
            MoveObject();
        }
        if(Input.GetMouseButtonUp(0))
            Drop();
    }
}
