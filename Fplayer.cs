using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fplayer : MonoBehaviour
{
    public Transform m_transform;
    // Use this for initialization
    public float speed;
    public float life = 10; //生命值
    public GameObject player;
    /// <summary>
    /// ///////////
    /// </summary>
    public string i; // 判断四个位置
    public float _gravityActive = 1;
    public bool LadderColliding;  //爬梯状态
    Ladder ladder;
    public  GameObject LT; //左边管道
    public bool center; //是否位于可攀爬的地方
    public Vector2 targetposition;
    public Rigidbody2D rb;
    public Animator m_ani;
   
    public Vector2 pos0;
    public GameObject RT; //右边管道
    private SpriteRenderer spriteRenderer; //获取sprite renderer组件
    public bool jump; //判断是否能跳跃
    private GameObject Bridge;
    public enum Pos
    {
        leftl,
        leftr,
        rightl,
        rightr,
        bridge,
    }
    public Pos pos;
    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        m_transform = this.transform;
        player = GameObject.FindWithTag("Player");
        LT = GameObject.FindWithTag("LT");
        RT = GameObject.FindWithTag("RT");
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        m_ani = player.GetComponent<Animator>();
        pos = Pos.leftl;
        pos0.x= LT.transform.position.x - 1.5f;
        jump = false;
        Bridge = GameObject.FindWithTag("Bridge");
    }

    // Update is called once per frame
    void Update()
    {
        pos0.y = m_transform.position.y;
        // spriteRenderer.color = new Color32(0, 0,timer, 255);

        AnimatorStateInfo stateInfo = m_ani.GetCurrentAnimatorStateInfo(1);
        if (stateInfo.fullPathHash == Animator.StringToHash("damage.damage") && !m_ani.IsInTransition(0))
        {
            m_ani.SetBool("damage", false);
        }

        this.GetComponent<Rigidbody2D>().gravityScale = _gravityActive;


        if (LadderColliding == false) //不处于攀爬状态
        {
            
            center = false;
            // Debug.LogError("ss");
            Control();

            Debug.Log("move");
        }

        else if (LadderColliding == true)

        {
           
           
            Laddermove(); }
        if ((m_transform.position.x >= pos0.x-1)||(m_transform.position.x<=pos0.x+1))
        { center = true; }
        else { center = false; }
        Where();
    }
    public void Control()
    {
        m_ani.SetBool("climb", false);
        if (Input.GetKey(KeyCode.A))
        {
           
            m_transform.Translate(Vector2.left * Time.deltaTime*speed,Space.World);
          
            iTween.RotateTo(player, new Vector3(0, -180, 0), 0.02f);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_transform.Translate(Vector2.right * Time.deltaTime*speed,Space.World);
          
            iTween.RotateTo(player, new Vector3(0, 0, 0), 0.02f);
        }
        if (Input.GetKeyDown(KeyCode.W)&&jump==true)   //最后翻转还是通过动画
        {
           
            rb.velocity = new Vector2(rb.velocity.x, 8);
          
        }
    }

    public void Laddermove()   //攀爬时刻的行动
    {
       




        if (Input.GetKey(KeyCode.W) && (center == true))
        {
            rb.bodyType = RigidbodyType2D.Static;   //当主角从阳台跳向水管时，消去惯力影响
            rb.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("Up");
           
        
            rb.MovePosition(rb.position + Vector2.up * Time.deltaTime * speed);
            m_ani.SetBool("climb", true);


        }
        else if (Input.GetKey(KeyCode.S) && (center == true))
        {
            rb.bodyType = RigidbodyType2D.Static;   //当主角从阳台跳向水管时，消去惯力影响
            rb.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("Down");
            
            
            rb.MovePosition(rb.position + Vector2.down * Time.deltaTime * speed);
            m_ani.SetBool("climb", true);


        }
        else if (Input.GetKeyDown(KeyCode.D))
        {

            Offlladder();

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {




            Offrladder();

        }
        else
        { m_ani.SetBool("climb", false); }
       
    }
    public void Offlladder()//离开左边朝右
    {
       
        m_ani.SetBool("climb", false);
       
        switch (pos)
        { case Pos.leftl:
                //pos = Pos.leftr;
                pos0.x = LT.transform.position.x+1.5f;
        //        iTween.RotateTo(player, new Vector3(0, -180, 0), 0.02f);
                Center();
                break;
          case Pos.leftr:
               // pos = Pos.rightl;
                pos0.x = RT.transform.position.x -1.5f;
                //iTween.MoveTo(this.gameObject, pos0, 2f);
                iTween.RotateTo(player, new Vector3(0, 0, 0), 0.02f);
                rb.AddForce(new Vector2(8, 0), ForceMode2D.Impulse);
                break;
          case Pos.rightl:

                //pos = Pos.rightr;
                pos0.x = RT.transform.position.x +1.5f;
             //   iTween.RotateTo(player, new Vector3(0, -180, 0), 0.02f);
                Center();
                break;
            case Pos.rightr:
                
                
                rb.AddForce(new Vector2(5, -5), ForceMode2D.Impulse);
                break;
            default:
                break;
        }
       
    
    }
    public void Offrladder()  //离开右边朝左

    {
        
        m_ani.SetBool("climb", false);
        //this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-30, 2), ForceMode2D.Force);
        switch (pos)
        {
            case Pos.leftl:
               
                rb.AddForce(new Vector2(-5, -5), ForceMode2D.Impulse);
                break;
            case Pos.leftr:
               ;
                pos0.x = LT.transform.position.x - 1.5f;
            
                Center();
                break;
            case Pos.rightl:
               // pos = Pos.leftr;
                pos0.x =LT.transform.position.x +1.5f;
                
                iTween.RotateTo(player, new Vector3(0, -180, 0), 0.02f);
                rb.AddForce(new Vector2(-8, 0), ForceMode2D.Impulse);
                break;
            case Pos.rightr:
                //pos = Pos.rightl;
                pos0.x = RT.transform.position.x -1.5f;
           //     iTween.RotateTo(player, new Vector3(0, 0, 0), 0.02f);
                Center();
                break;
            default:
                break;
        }
       
     
    }
    public void Where()  //判断处于水管的那个位置
    {
        if ((m_transform.position.x >= RT.transform.position.x - 3f) && (m_transform.position.x <= RT.transform.position.x))
        {
            pos = Pos.rightl;
            iTween.RotateTo(player, new Vector3(0, 0, 0), 0.02f);
        }
        else if ((m_transform.position.x >=LT.transform.position.x) && (m_transform.position.x <= LT.transform.position.x + 3f))
        { pos = Pos.leftr;
          iTween.RotateTo(player, new Vector3(0, -180, 0), 0.02f);
        }

        else if ((m_transform.position.x > RT.transform.position.x) && (m_transform.position.x <= RT.transform.position.x + 3f))
        {
            pos = Pos.rightr;
            iTween.RotateTo(player, new Vector3(0, -180, 0), 0.02f);
        }
        else if ((m_transform.position.x >= LT.transform.position.x -3f) && (m_transform.position.x < LT.transform.position.x))
        { pos = Pos.leftl;
         iTween.RotateTo(player, new Vector3(0, 0, 0), 0.02f);
        }
        else if((m_transform.position.x>Bridge.transform.position.x-1)&&(m_transform.position.x<Bridge.transform.position.x+1)) { pos = Pos.bridge; }
    }
    public void Center() 
    {

       
        
      iTween.MoveTo(this.gameObject, pos0, 0.2f);
}

   
      
    
    public void Damage(int n)
    { life -= n;
      m_ani.SetBool("damage", true);
    }
    
}