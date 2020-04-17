using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    Tilemap map;

    void Awake()
    {
        map = GetComponent<Tilemap>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
        //INVESTIGATE: An alternative to setting the color would be to replace the sprite with
        // a highlighted verison. Could easily hook in with the TODO above.
    
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

    public void setTileToColor(Vector2Int loc, Color color){
        Debug.Log($"Setting {loc} to {color}");
        map.SetColor((Vector3Int)loc, color);
    }
}
