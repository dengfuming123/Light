using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour {

    public GameObject player;  //主角
    public float speed;  //相机跟随速度


    void Start()
    { player = GameObject.FindWithTag("Player"); }
    void Update()
    {
        Vector3 ca =new Vector3( player.transform.position.x,player.transform.position.y,player.transform.position.z-10);
        this.transform.position = ca;
    }

    

}
