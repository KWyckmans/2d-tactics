﻿using System.Collections;
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
        //TODO: Switch this over from mousedown to an area around the player
        //TODO: (optional) Give the area that is highlighted a border. This could potentially(!)
        // be done by implementing a custom Tile class (https://docs.unity3d.com/Manual/Tilemap-ScriptableTiles-Example.html)
        // that in GetTileData switches to a sprite with a border in the required direction for tiles at the 
        // edge. If needed can even be scaled a bit to make it "stick out". Alternatively: don't do a border.
        // Proposed solution would also allow for rounded borders.
    
        if (Input.GetMouseButtonDown(0))
		{
            

            Debug.Log("Found a mousedown!");

            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var cellLoc = map.WorldToCell(point);

            GridLayout grid = map.GetComponentInParent<GridLayout>();
            map.SetColor(cellLoc, Color.red);

            Debug.Log($"{cellLoc.x}, {cellLoc.y}");
        }
    }
}
