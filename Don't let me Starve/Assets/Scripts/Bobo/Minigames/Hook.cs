using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public bool finished;
    public GameObject hookedFood;

    public Slingshot slingshot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            finished = true;
            hookedFood.SetActive(true);
            Destroy(collision.gameObject);

            slingshot.Hit();
        }
    }
}
