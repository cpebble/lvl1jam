using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAbleObject : MonoBehaviour
{
    [SerializeField]
    Cell[] cells = new Cell[5];
    [SerializeField]
    LayerMask gridLayer;
    
    private PlacementGrid currentGrid;
    // Start is called before the first frame update
    void Start()
    {

    }
    public Cell GetCell(int index){
        return cells[0];
    }
    public void RemoveFromGrid(){
        currentGrid.RemoveObject(this);
    }
    public void AddToGrid(PlacementGrid grid){
        grid.Place(this,gridLayer);
        currentGrid = grid;
    }
    public bool IsOnGrid(){
        return currentGrid != null;
    }
    public bool CheckIfOnGrid()
    {
        List<Cell> overlapCells = new List<Cell>();
        foreach (Cell c in cells)
        {
            Collider2D col;
            col = Physics2D.OverlapPoint(c.transform.position, gridLayer, -100f, 100f);
            if (col != null && col.transform.CompareTag("PlacementGrid") && !col.transform.GetComponent<Cell>().inUse)
            {
                if(overlapCells.Contains(c))
                    return false;
                overlapCells.Add(col.GetComponent<Cell>());
            }
            else
                return false;
        }
        //print("overlap"+overlapCells.Count);
        //print("cells:"+cells.Length);
        return overlapCells.Count == cells.Length;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
