using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementGrid : MonoBehaviour
{
    [SerializeField]
    Cell[] cells;

    List<DragAbleObject> heldObjects = new List<DragAbleObject>();

    [SerializeField]
    private LayerMask gridLayer;

    // Start is called before the first frame update
    void Start()
    {

    }


    public void Place(DragAbleObject obj)
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
    }
    public void RemoveObject(DragAbleObject obj)
    {
        if (heldObjects.Contains(obj))
            heldObjects.Remove(obj);
    }
    public void CheckIfOnGrid()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
