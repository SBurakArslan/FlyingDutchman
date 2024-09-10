using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundRepeat : MonoBehaviour
{
    Vector2 StartPos;
    public static float repeatWidth;
    private float speed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x /2;
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector2.left * Time.deltaTime * speed);
        if (transform.position.x  < StartPos.x - repeatWidth) {

            transform.position = StartPos;
        }
    }
}
