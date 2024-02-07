using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] bool onGround;
    [SerializeField] LayerMask engel;
    private float width;
    public Rigidbody2D myBody;
    private static int totalEnemy = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalEnemy++;
        width = GetComponent<SpriteRenderer>().bounds.extents.x;
        width /= 2;
        myBody = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * width), Vector2.down, 2f,engel);
        if (hit.collider != null)
        {
            onGround = true;
            
        }
        else
        {
            onGround = false;

        }
        if (onGround == true)
        {
myBody.velocity = new Vector2(transform.right.x * 3, 0);
        }
        else
        {
            transform.eulerAngles += new Vector3(0, 180, 0);
        }
        
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            myBody.velocity = new Vector2(0, 0);
            enabled = false;
        }
        
    }
    
}

