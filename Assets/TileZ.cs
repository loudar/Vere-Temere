using UnityEngine;
using UnityEngine.Tilemaps;

public class TileZ : MonoBehaviour
{

	Tilemap tilemap;
	SpriteMask mask;
	Vector3Int tilePos;
	GameObject player;

	void Start()
	{
		tilemap = GameObject.Find("Walls").GetComponent<Tilemap>();
		mask = GetComponent<SpriteMask>();
		player = GameObject.Find("Player");

		// 0.5f adjustment, because it gets rounded down, and because my tiles' pivot is at (0.5, 0.5)
		// you may have to tweak this to get better results. select your tilemap in play mode to see
		// which SpriteMasks are active
		tilePos = new Vector3Int((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f), 0);
		Sprite sprite = tilemap.GetSprite(tilePos);

		if (sprite != null)
		{
			mask.sprite = sprite;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (player.transform.position.y < mask.transform.position.y)
			mask.enabled = false;
		else
			mask.enabled = true;
	}
}