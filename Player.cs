//Unity Headers.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    //variables.
    [Header("Game Options")]
    [SerializeField] float player_speed = 10f;
    [SerializeField] float Jump_speed = 7f;
    [SerializeField] float Climp_speed = 10f;
    [SerializeField] GameObject Death_vfx;

    void Start()
    {
    }

    void Update()
    {
        //Functions responsible for movements of player.
        movement();
        climpladder();
    }

    //function responsible for horizontal and vertical movement.
    public void movement()
    {
        //Input buttons
        var moveXpos = Input.GetAxis("Horizontal") * Time.deltaTime * player_speed;
        var moveYpos = Input.GetAxis("Jump") * Time.deltaTime * Jump_speed;
        
        //Movements
        var newXpos = transform.position.x + moveXpos;
        var newYpos = transform.position.y + moveYpos;

        transform.position = new Vector2(newXpos, newYpos);
        
        //for waliking in opposite direction
        bool HorziontalMovement = Mathf.Abs(moveXpos) > Mathf.Epsilon;
        if (HorziontalMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(moveXpos), 1f); 
        }
        
        //Animation
        GetComponent<Animator>().SetBool("Running", HorziontalMovement);
    }
    
    //Function for climping ladders.
    void climpladder()
    {
        //Checking whether the player is on ladder.
        if (!GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            //if not on ladder,
            GetComponent<Animator>().SetBool("Climping", false);
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        else
        {
            //if on ladder
            GetComponent<Rigidbody2D>().gravityScale = 0;
            var moveYpos = Input.GetAxis("Vertical") * Time.deltaTime * Climp_speed;
            var newYpos = transform.position.y + moveYpos;
            bool climp = Mathf.Abs(newYpos) > Mathf.Epsilon;
            GetComponent<Animator>().SetBool("Climping", true);
            transform.position = new Vector2(transform.position.x, newYpos);
        }
    }

    //Collision check
    private void OnCollisionEnter2D(Collision2D collider)
    {
        //if the player collidede with any enemy or obstacle
        if(collider.gameObject.GetComponent<Enemy>() || collider.gameObject.name == "Spikes")
        {
            // activate vfx, deacrease health, death animation
            GameObject parcticle = Instantiate(Death_vfx, transform.position, Quaternion.identity) as GameObject;
            FindObjectOfType<GameSession>().DeathControl();
            Destroy(parcticle, 2f);
            GetComponent<Animator>().SetTrigger("Death");
        }
    }
}
