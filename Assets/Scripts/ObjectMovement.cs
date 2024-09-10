using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectMovement : MonoBehaviour {

    public float speed = 20.0f;
    public delegate void ObjectDestroyed();
    public event ObjectDestroyed OnObjectDestroyed;

    private void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -9f) {

            OnObjectDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}