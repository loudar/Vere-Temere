using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileOccluder : MonoBehaviour
{
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int x = (int) rb.position.x;
        int y = (int) rb.position.y;

        Vector3 newPosition = transform.position;

        if (y < transform.position.y)
        {
            newPosition.z = 8;
            transform.position = newPosition;
        }
        else
        {
            newPosition.z = 6;
            transform.position = newPosition;
        }

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
