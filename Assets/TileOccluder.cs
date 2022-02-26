using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileOccluder : MonoBehaviour
{
    public Tilemap tm;
    Tile testTile;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tm = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        int x = (int) rb.position.x;
        int y = (int) rb.position.y;

        TileBase tile = tm.GetTile(new Vector3Int(x, y, 0));

        Vector3 newPosition = transform.position;
        newPosition.z = player.transform.z;
        transform.position = newPosition;

        /*
        if ()
        {
            
            Debug.Log(tm.GetTile(new Vector3Int(x, y, 0)));
            tm.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
            tm.SetColor(new Vector3Int(x, y, 0), Color.Lerp(tm.GetColor(new Vector3Int(x, y, 0)), Color.clear, 0.1f));
        }*/

        /*
        if (tm.GetTile(new Vector3Int(x - 1, y - 1, 0)) == wall)
        {
            tm.SetTileFlags(new Vector3Int(x - 1, y - 1, 0), TileFlags.None);
            tm.SetColor(new Vector3Int(x - 1, y - 1, 0), Color.Lerp(tm.GetColor(new Vector3Int(x - 1, y - 1, 0)), Color.clear, 0.1f));
        }
        if (tm.GetTile(new Vector3Int(x + 1, y - 1, 0)) == wall)
        {
            tm.SetTileFlags(new Vector3Int(x + 1, y - 1, 0), TileFlags.None);
            tm.SetColor(new Vector3Int(x + 1, y - 1, 0), Color.Lerp(tm.GetColor(new Vector3Int(x + 1, y - 1, 0)), Color.clear, 0.1f));
        }
        */
    }
}
