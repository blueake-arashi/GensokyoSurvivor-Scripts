using System.Collections;
using UnityEngine;

public class Skillbase : MonoBehaviour
{
    public string Skillname;//技能名字
    public float CDtime;//技能冷却时间
    public float CDkey;//冷却键    
    public int damage;//技能的基础伤害
    public int level;//技能的等级
    public float lifetime;//生命周期
    public int pass;//穿透
    public float speed;//子弹速度
    public int  number;//鸡蛋数量
    public GameObject bullet;//子弹
    public float size;//子弹的大小
    public float interval;//间隔时间
    public GameObject player;
    public float angle;//旋转角度
    public bool 是否朝向最近敌人;
    public Sprite icon;
   void FixedUpdate()
    {
        CDkey +=Time.fixedDeltaTime;
        if(CDkey>CDtime)
        {
             CDkey= CDtime ;
        }
     
    }

       

    public virtual IEnumerator UseSkill()//使用技能
    {
        CDkey = 0;
        for(int i=0;i<number;++i)
        {
            GameObject newbullet=Instantiate(bullet,player.transform.position,Quaternion.Euler(new Vector3(0,0,angle)));
            Bulletbase n = newbullet.GetComponent<Bulletbase>();
            n.fatherskill = this;
            n.Getfather();
            n.获取目标();
            n.cango = true;

            yield return new WaitForSeconds(interval);  
        }
    }

}
