using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
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
      //  GetComponent<Animator>().SetBool("Climp", false);
        movement();
        climpladder();
    }

    public void movement()
    {
        
       // if (!GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        var moveXpos = Input.GetAxis("Horizontal") * Time.deltaTime * player_speed;
        var moveYpos = Input.GetAxis("Jump") * Time.deltaTime * Jump_speed;

        var newXpos = transform.position.x + moveXpos;
        var newYpos = transform.position.y + moveYpos;

        transform.position = new Vector2(newXpos, newYpos);

        bool HorziontalMovement = Mathf.Abs(moveXpos) > Mathf.Epsilon;
        if (HorziontalMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(moveXpos), 1f); 
        }

        GetComponent<Animator>().SetBool("Running", HorziontalMovement);
    }

    void climpladder()
    {
        if (!GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            GetComponent<Animator>().SetBool("Climping", false);
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            var moveYpos = Input.GetAxis("Vertical") * Time.deltaTime * Climp_speed;
            var newYpos = transform.position.y + moveYpos;
            bool climp = Mathf.Abs(newYpos) > Mathf.Epsilon;
            GetComponent<Animator>().SetBool("Climping", true);
            transform.position = new Vector2(transform.position.x, newYpos);
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.GetComponent<Enemy>() || collider.gameObject.name == "Spikes")
        {
            GameObject parcticle = Instantiate(Death_vfx, transform.position, Quaternion.identity) as GameObject;
            FindObjectOfType<GameSession>().DeathControl();
            Destroy(parcticle, 2f);
            GetComponent<Animator>().SetTrigger("Death");
           // GetComponent<Rigidbody2D>().velocity = new Vector2(50f, 50f);
        }
    }
}
