using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balcony : MonoBehaviour {
    public GameObject Player;
    private Vector2 pos;
    protected Vector3 _boundsBottomCenterPosition;
    protected BoxCollider2D _boxCollider;
    public void Start()
    { Player = GameObject.FindWithTag("Player");
        pos = this.transform.position;
       
       
     
    }
    public void Update()
    { pos.x = Player.transform.position.x;

    }
    public void OnCollisionEnter2D(Collision2D col)
    {
       

        if (col.transform.tag  == "Player")
        {
            col.transform.GetComponent<Fplayer>().jump = true;
            col.transform.GetComponent<Fplayer>()._gravityActive = 1;
       
        }

    }
    public void OnCollisionStay2D(Collision2D col)
    {
        
        if (col.transform.tag == "Player")
        {
           

            col.transform.GetComponent<Fplayer>().Control();

          
            col.transform.GetComponent<Fplayer>().jump = true;
        }
    }
    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            col.transform.GetComponent<Fplayer>().jump = false;
            col.transform.GetComponent<Fplayer>()._gravityActive = 1;
           
        }
        
    }
}
