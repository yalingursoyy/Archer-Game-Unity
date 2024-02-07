using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mySpeedX;
    private Rigidbody2D myBody;
    private Vector3 defaultLocalScale;
    public bool onGround;
    private bool doubleJump;

    // Start is called before the first frame update
    [SerializeField] GameObject arrow;
    [SerializeField] bool attacked;
    private float currentAttackTimer;
    private float defaultAttackTimer = 1;
    float mySpeedY;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        defaultLocalScale = transform.localScale;
        attacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        mySpeedX = Input.GetAxis("Horizontal") * 5;
        
        gameObject.GetComponent<Animator>().SetFloat("Speed", Mathf.Abs(mySpeedX));
        myBody.velocity = new Vector2(mySpeedX, GetComponent<Rigidbody2D>().velocity.y);
        #region playerın yönü

        if (mySpeedX > 0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x,defaultLocalScale.y,defaultLocalScale.z);
        }
        else if (mySpeedX < 0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        #endregion

        #region zıplama
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            if (onGround == true) { 
            myBody.velocity = new Vector2(myBody.velocity.x, 8);
                doubleJump = true;
                gameObject.GetComponent<Animator>().SetTrigger("Jump");
            }
            else
            {
                if(doubleJump== true)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, 5);
                    doubleJump = false;
                }
            }
            
        }
        #endregion

        #region ok atma


        if (Input.GetMouseButtonDown(0))
        {
            if (attacked == false)
            {
                Fire();
                attacked = true;
                gameObject.GetComponent<Animator>().SetTrigger("Attack");
            }
            
        }




        #endregion

        if (attacked == true)
        {
            currentAttackTimer -= Time.deltaTime;
        }
        else
        {
            currentAttackTimer = defaultAttackTimer;
        }

        if (currentAttackTimer <= 0.5)
        {
            attacked = false;
        }

        

}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") { 
        Die();
    }}
    void Fire()
        {
            GameObject okumuz = Instantiate(arrow, transform.position, Quaternion.identity);
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);

            if (transform.localScale.x > 0)
            {
                okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
            }
            else
            {
                Vector3 okumuzScale = okumuz.transform.localScale;
                okumuz.transform.localScale = new Vector3(-okumuzScale.x, okumuzScale.y, okumuzScale.z);
                okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
            }
        }

    void Die()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Die");
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;
        
        

        enabled = false;
    }
}