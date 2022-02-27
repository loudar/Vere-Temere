using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyCollide : MonoBehaviour
{
    public SpriteRenderer sprite;
    [SerializeField] Color color;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = color;
            GetComponent<SpriteRenderer>().sortingOrder = 7;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}
