using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformOneWay : MonoBehaviour {

    // Pixel Life Studio Disclaimer:
    // THIS IS JUST A CRUDE DEMONSTRATION AND SIMPLIFICATION OF HOW TO DO AND IMPLEMENT SOME FEATURES
    // WE APOLOGISE IF OUR IMPLEMENTATION OF SOME FEATURE MAY NOT LOOK HIGH END CODE

    private GameObject currentOneWayPlatform;                   // Reference the GameObject

    [SerializeField] private CapsuleCollider2D playerCollider;  // Reference the Player Collider

    // If press Down key start Coroutine
    void Update() {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            if (currentOneWayPlatform != null)
            StartCoroutine(DisableCollision());
        }
    }

    // Enter Trigger function
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("OneWayPlatform")) {
            currentOneWayPlatform = collision.gameObject;
        }
    }
    // Exit Trigger function
    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("OneWayPlatform")) {
            currentOneWayPlatform = null;
        }
    }
    // Disable collisions function for X amount of time
    IEnumerator DisableCollision() {
        TilemapCollider2D plaformCollider = currentOneWayPlatform.GetComponent<TilemapCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, plaformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, plaformCollider, false);

    }
}
