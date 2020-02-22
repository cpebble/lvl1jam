using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAbleObject : MonoBehaviour
{
    [SerializeField]
    Cell[] cells = new Cell[5];
    [SerializeField]
    LayerMask gridLayer;
    public int cellAmount {get { return cells.Length;}}
    private PlacementGrid currentGrid;
    public bool beingMoved;
    [HideInInspector]
    public Vector3 oldPosition;
    [HideInInspector]
    public Quaternion oldRotation;
    // Start is called before the first frame update
    public List<Cell> connectedToCells;
    public Cell GetCell(int index){
        return cells[0];
    }
    public void RemoveFromGrid(){
        currentGrid.RemoveObject(this);
        currentGrid = null;
    }
    public void AddToGrid(PlacementGrid grid){
        grid.Place(this,gridLayer);
        connectedToCells = GetOverlapCells(false);
        currentGrid = grid;
    }
    public bool IsOnGrid(){
        return currentGrid != null;
    }
    public void Update(){
        if(!beingMoved && !IsOnGrid())
            GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100f);
        else
            GetComponent<SpriteRenderer>().sortingOrder = 1000;
    }
    public List<Cell> GetOverlapCells(bool checkIfInUse)
    {
        List<Cell> overlapCells = new List<Cell>();
        foreach (Cell c in cells) {
            Collider2D col;
            col = Physics2D.OverlapPoint(c.transform.position, gridLayer, -100f, 100f);
            if (col != null && col.transform.CompareTag("PlacementGrid") && (!col.transform.GetComponent<Cell>().inUse || !checkIfInUse)) {
                overlapCells.Add(col.GetComponent<Cell>());
            }
            else
                return null;
        }
        return overlapCells;
    }
}
