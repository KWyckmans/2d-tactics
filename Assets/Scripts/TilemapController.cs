using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    Tilemap map;
    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{

            // Code below does not really work. Don't actually know what Tile.Color does.
            // Want to figure it out, but if I really want to get it working
            // replacing the sprites for a highlighted version would work as well

            Debug.Log("Found a mousedown!");
            // map.RefreshAllTiles();

            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var cellLoc = map.WorldToCell(point);

            GridLayout grid = map.GetComponentInParent<GridLayout>();
            map.SetColor(cellLoc, Color.red);
            
            // map.SetTileFlags(cellLoc, TileFlags.None); //Needed because I can't uncheck this in the unity editor
            //  new Color(0.9820799f, 0.6273585f, 1)
            // Tile tile = map.GetTile<Tile>(cellLoc);
            // tile.flags = TileFlags.None;
            // tile.color = Color.green;
            // tile.SetDirty();'x
            // if(tile) 
            //     tile.sprite = null;
            
            // map.SetTile(cellLoc, tile);

            
            // map.SetColor(cellLoc, Color.green);
            // map.SetTile(cellLoc, tile);
            // Debug.Log(tile.color);

            // map.SetColor(cellLoc, Color.blue);

            Debug.Log($"{cellLoc.x}, {cellLoc.y}");
            // map.RefreshTile(cellLoc);
            

        }
    }
}
