using UnityEngine;

// Technique for making sure there isn't a null reference during runtime if you are going to use get component
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 5;

    //-----------------------------------------------------------------------------
    void Start()
    {
        Fire();
    }

    //-----------------------------------------------------------------------------
    private void Fire()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        Debug.Log("Wwweeeeee");
    }
}
