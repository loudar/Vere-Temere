using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyCollide : MonoBehaviour
{
    public SpriteRenderer sprite;
    [SerializeField] Color color;
    bool fadingOut = false;
    float t;
    float fadeTime = .5f;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // time
        if (t > 0)
        {
            t -= Time.deltaTime;
        }
        else
        {
            t = 0;
        }

        // color
        if (fadingOut)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, color, 1 - (t * fadeTime));
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(color, Color.white, 1 - (t * fadeTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fadingOut = true;
            t = fadeTime;
            GetComponent<SpriteRenderer>().sortingOrder = 7;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fadingOut = false;
            t = fadeTime;
            GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}
