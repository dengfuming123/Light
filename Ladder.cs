using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {
   
    public bool CenterCharacterOnLadder = true;
    public GameObject Player;
  
   
    public Collider2D top;






    protected virtual void Start()
    {
       
    
        top = GameObject.FindWithTag("Top").GetComponent<Collider2D>();

    }

    void Update()
    {



    }

    /// <summary>
    /// Triggered when something exits the ladder
    /// </summary>
    /// <param name="collider">Something colliding with the ladder.</param>
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
      
        if (collider.tag == "Player")
        {
          
            Fplayer characterLadder = collider.GetComponent<Fplayer>();
           
            characterLadder._gravityActive = 0;
           
            characterLadder.LadderColliding = true;
          
        }

    }
    protected virtual void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {

            Fplayer characterLadder = collider.GetComponent<Fplayer>();
            characterLadder._gravityActive = 0;
            characterLadder.LadderColliding = true;
            //Vector2 targetposition = new Vector2(_boundsBottomCenterPosition.x, characterLadder.transform.position.y);

            Physics2D.IgnoreCollision(collider.GetComponent<Collider2D>(), top, true); //与屋顶不碰撞
        }

    }
    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {

            Fplayer characterLadder = collider.GetComponent<Fplayer>();
            characterLadder._gravityActive =1;
            characterLadder.LadderColliding = false;
            collider.transform.GetComponent<Fplayer>().Control();


            Physics2D.IgnoreCollision(collider.GetComponent<Collider2D>(), top, false); //与屋顶有碰撞
        }

    }
    //IEnumerator back()
    //{
    //    yield return new WaitForSeconds(1f);
    //    iTween.MoveTo(Player, targetposition, 2f);
    //}
}
