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
            Debug.Log("Found a mousedown!");

            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var cellLoc = map.WorldToCell(point);

            GridLayout grid = map.GetComponentInParent<GridLayout>();
            map.SetColor(cellLoc, Color.red);

            Debug.Log($"{cellLoc.x}, {cellLoc.y}");
        }
    }
}
