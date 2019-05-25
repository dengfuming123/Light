using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    public Collider2D top;// 房顶
    // Use this for initialization
    void Start()
    {
     
    }

   
    void Update()
    {

    }
    private void OnCollisionStay2D(Collision2D col)  //进入梯子时候
    {
       
        if (col.gameObject.tag == "Player")   
        {
           col.gameObject.GetComponent<Fplayer>().jump = true;
            
        }
    }
    private void OnCollisionExit2D(Collision2D col)  
    {
        if (col.gameObject.tag == "Player")   
        {
            col.gameObject.GetComponent<Fplayer>().jump = false;
        }
    }
}