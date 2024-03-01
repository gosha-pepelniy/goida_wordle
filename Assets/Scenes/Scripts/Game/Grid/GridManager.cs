using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    private List<Row> Rows;
    private TileState None;

    private void Awake()
    {
        Rows = new List<Row>();
        None = Resources.Load("TileStates/None") as TileState;
    }

    public void GenerateGrid(int rows, int cols)
    {
        GameObject rowRef = Resources.Load("Prefabs/Row") as GameObject;
        GameObject tileRef = Resources.Load("Prefabs/Tile") as GameObject;
        for (int i = 0; i < rows; i++)
        {
            var row = Instantiate(rowRef, transform);
            Row component = row.GetComponent<Row>();

            List<Tile> tiles = new List<Tile>();
            for (int j = 0; j < cols; j++)
            {
                var tile = Instantiate(tileRef, row.transform);
                tiles.Add(tile.GetComponent<Tile>());
            }
            component.SetTiles(tiles);
            Rows.Add(component);
        }
    }

    public void ClearGrid()
    {
        for(int i = 0;i < Rows.Count;i++)
        {
            foreach (Tile tile in Rows[i]) 
            {
                tile.SetLetter('\0');
                tile.SetState(None);
            }
        }
    }

    public void DestroyGrid()
    {
        for (int i = 0; i < Rows.Count; i++)
            Destroy(Rows[i].gameObject);
        Rows.Clear();
    }

    public Row GetRow(int rowIndex) => Rows[rowIndex];
}