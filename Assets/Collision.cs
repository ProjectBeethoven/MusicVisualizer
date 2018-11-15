using UnityEngine;

public class Collision : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        Debug.Log(collisionInfo.collider.name);
    }
}
 