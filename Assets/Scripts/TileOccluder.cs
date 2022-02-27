using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileOccluder : MonoBehaviour
{
    //public Rigidbody2D rb;
    GameObject player;
    public Tilemap tilemap;
    public SpriteRenderer playerRenderer;
    public SpriteRenderer playerShadow;
    public Rigidbody2D rigidbody;
    Color previousColor = Color.white;
    Color occludeColor;
    readonly double gridSize = 2.5;
    int currentTileX;
    int currentTileY;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        tilemap = GetComponent<Tilemap>();
        player = GameObject.Find("Player");
        rigidbody = player.GetComponent<Rigidbody2D>();
        playerRenderer = player.GetComponent<SpriteRenderer>();
        playerShadow = GameObject.Find("Shadow").GetComponent<SpriteRenderer>();
    }

    void revertCurrentTile()
    {
        Vector3Int checkPos = new Vector3Int(currentTileX, currentTileY, 0);
        occludeColor = tilemap.GetColor(checkPos);

        tilemap.SetTileFlags(checkPos, TileFlags.None);
        tilemap.SetColor(checkPos, previousColor);
        //tilemap.SetColor(checkPos, Color.Lerp(occludeColor, previousColor, 0.1f));

        // clear used position so next tile can be transparent
        currentTileX = 0;
        currentTileY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        double gridX = rigidbody.transform.position.x / (gridSize * 2);
        double gridY = rigidbody.transform.position.y / gridSize;

        //gridX += .5 * Mathf.Sign((float) gridX);

        double xRest = Mathf.Abs((float) gridX) % 1;
        double xFactor = 1 - ((xRest - .5) / .5);
        double yRest = gridY % 1;
        yRest += .5;

        int yOffset = 0;
        if (yRest < .4 * xFactor)
        {
            yOffset = -1;
        }
        else if (yRest > 1.4 - .4 * xFactor)
        {
            yOffset = 1;
        }

        int xOffset = 0;
        if (yOffset != 0)
        {
            xOffset = -1;
        }

        if ((gridY + yOffset) % 2 != 0)
        {
            gridX -= .5;
        }
        if (xRest > .5 && yOffset == 0)
        {
            xOffset = (int) Mathf.Sign((float) gridX);
        }

        int y = (int) (gridY + yOffset);
        int x = (int) (gridX + xOffset);

        Debug.Log($"x: {xRest}, y: {yRest}");

        Vector3Int playerPos = new Vector3Int(x, y, 0);
        TileBase tileBase = tilemap.GetTile(playerPos);

        if (tileBase != null)
        {
            playerRenderer.sortingOrder = 5;
            playerShadow.sortingOrder = 5;
            if ((currentTileX == 0 && currentTileY == 0) || (currentTileX != x || currentTileY != y))
            {
                revertCurrentTile();
                previousColor = new Color(tilemap.GetColor(playerPos).r, tilemap.GetColor(playerPos).g, tilemap.GetColor(playerPos).b, 1f);
                occludeColor = new Color(previousColor.r, previousColor.g, previousColor.b, 0);
                currentTileX = x;
                currentTileY = y;

                tilemap.SetTileFlags(playerPos, TileFlags.None);
                tilemap.SetColor(playerPos, Color.Lerp(previousColor, occludeColor, 0.5f));
            }
        }
        else
        {
            playerRenderer.sortingOrder= 7;
            playerShadow.sortingOrder = 7;
            if (currentTileX != 0 || currentTileY != 0)
            {
                revertCurrentTile();
            }
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
