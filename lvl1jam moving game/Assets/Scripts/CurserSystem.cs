using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurserSystem : MonoBehaviour, ILevelEventHandler
{
    [SerializeField]
    LayerMask gridLayer;
    [SerializeField]
    private PlacementGrid placementGrid,trashCan;
    private Transform currentObject;
    private DragAbleObject currentDragAble;
    private Vector2 objectOffset;

    [SerializeField]
    private LevelLoader levelMan;
    // Start is called before the first frame update
    void Start()
    {
        levelMan.RegisterEventHandler(this);
    }
    Collider2D ObjectUnderCursor()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.OverlapPoint(mousePos, gridLayer, -100f, 100f);

    }
    void Grab()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = ObjectUnderCursor();
        if (col != null && col.transform.CompareTag("DragAbleObject"))
        {
            currentObject = col.transform.parent;
            objectOffset = (Vector2)currentObject.position - mousePos;
            currentDragAble = currentObject.GetComponent<DragAbleObject>();

            if(currentObject.GetComponent<DragAbleObject>().IsOnGrid()){
                List<Cell> cells = currentDragAble.GetOverlapCells(false);
                foreach (Cell c in cells) { c.inUse = false; }
                currentObject.GetComponent<DragAbleObject>().RemoveFromGrid();
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
    bool CheckIfTrashcan()
    {
        List<Cell> cells = currentDragAble.GetOverlapCells(false);
        if(cells == null || cells.Count == 0)
            return false;
        foreach (Cell c in cells)
        {
            if(c.transform.parent != null && c.transform.parent.GetComponent<PlacementGrid>().IsTrashcan)
                return true;
        }
        return false;
    }
    void Drop(){
        if(CheckIfTrashcan())
        {
            Destroy(currentObject.gameObject);
            currentDragAble = null;
            currentObject = null;
            objectOffset = Vector3.zero;
            return;
        }
        if(CheckIfDropAble())
        {
            List<Cell> cells = currentDragAble.GetOverlapCells(true);
            foreach (Cell c in cells) { c.inUse = true; }
            currentDragAble.AddToGrid(placementGrid);
        }
        else
            currentObject.transform.position = currentDragAble.oldPosition;
        currentDragAble.beingMoved = false;
        currentDragAble = null;
        currentObject = null;
        objectOffset = Vector2.zero;
    }
    bool CheckIfDropAble(){
        DragAbleObject dragObject = currentObject.GetComponent<DragAbleObject>();
        return currentObject != null && dragObject.GetOverlapCells(true)?.Count == dragObject.cellAmount;
    }
    void MoveObject()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentObject.position = (Vector3)mousePos + (Vector3)objectOffset + new Vector3(0, 0, -1);
    }
    void CheckForToolTips()
    {

        Collider2D col = ObjectUnderCursor();
        col.GetComponent<ToolTip>().ShowTooltip();
        print("Should");
    }

    void RotateObject(){
        currentObject.Rotate(Vector3.forward * 90f);
        currentObject.GetChild(currentObject.childCount-1).Rotate(-Vector3.forward * 90f);
    }
    // Update is called once per frame
    void Update()
    {
        // Grabablelogic
        if (Input.GetMouseButtonDown(0))
            Grab();
        if (currentObject != null)
        {
            MoveObject();
            if(Input.GetKeyDown(KeyCode.R))
                RotateObject();
        }
        if(Input.GetMouseButtonUp(0) && currentDragAble != null)
            Drop();

        // Tooltip logic
        //CheckForToolTips();
    }

    public void HandleEvent(LevelLoadState state)
    {
        switch (state)
        {
            case LevelLoadState.POSTLOAD:
                placementGrid =  levelMan.Levels[levelMan.currentLevel].levelGameObject.GetComponent<PlacementGrid>();
                break;
        }
    }
}
