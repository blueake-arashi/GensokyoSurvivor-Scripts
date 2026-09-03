using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bulletbase : MonoBehaviour
{

    public int damage;//技能的基础伤害
    public int level;//技能的等级
    public float lifetime;//生命周期
    public int pass;//穿透
    public float speed;//子弹速度
    public float size;//子弹的大小
    public Skillbase fatherskill;//创建他的父亲技能
    public 角色基类 player;
    public bool cango = false;//子弹是否可以发射
    public Rigidbody rb;
    public Transform 怪物层;
    public GameObject 目标角色;
    public Vector3 distance;
    public AudioClip 技能发射音效;
    public AudioClip 子弹击中音效;

    public void OnEnable()
    {
        if(技能发射音效!=null)
        {
            GetComponent<AudioSource>().volume = GameObject.Find("声音管理").GetComponent<声音管理>().SE音量;

            GetComponent<AudioSource>().clip = 技能发射音效;
            GetComponent<AudioSource>().Play();
        }
     
    }



    public virtual void Getfather()//获取父属性
    {
        damage = fatherskill.damage;
        level = fatherskill.level;
        lifetime = fatherskill.lifetime;
        pass = fatherskill.pass;
        speed = fatherskill.speed;
        size = fatherskill.size;
        //................
        player = GameObject.Find("玩家对象层").transform.GetChild(0).GetComponent<角色基类>();
        rb = GetComponent<Rigidbody>();
        怪物层 = GameObject.Find("怪物层").transform;

        // ⭐【必须加上这句】：用拿到的 size 去真正改变子弹的缩放！
        // 假设原本的初始大小是 1。如果是增加30%，你的“提升数值”填 0.3f，这里就是 Vector3.one * (1 + 0.3f)
        transform.localScale = Vector3.one * size;
    }
    private void OnTriggerEnter(Collider other)
    {




        if (other.CompareTag("Monster"))
        {
            if(子弹击中音效!=null)
            {
                GetComponent<AudioSource>().volume = GameObject.Find("声音管理").GetComponent<声音管理>().SE音量;

                GetComponent<AudioSource>().clip = 子弹击中音效;
                GetComponent<AudioSource>().Play();
            }
         

            pass -= 1;
            if (pass <= 0)
            {
                销毁自己();
            }
            怪物类 Monster = other.GetComponent<怪物类>();

            if (Monster.HP > 0)
            {
                float 最终伤害 = damage + player.ATT;
                float random = Random.value;
                if (player.CritRate > random)
                {
                    最终伤害 *= 2;
                }
                最终伤害 -= Monster.DEF;
                Monster.HP -= (int)最终伤害;
                GameObject 伤害数字 = Monster.伤害数字;
                GameObject 数字 = Instantiate(伤害数字, other.transform.position, default);

                数字.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ((int)最终伤害).ToString();
                other.GetComponent<怪物类>().开始受击反馈();
                if (Monster.HP <= 0)
                {
                    Monster.死亡事件();
                }
            }
        }
    }

    public void 销毁自己()
    {
        Destroy(gameObject);
    }

    public void 获取目标()
    {
        float 最近距离 = 999;
        Transform 临时最近单位 = null;
        if (怪物层.childCount > 0)
        {
            foreach (Transform item in 怪物层)
            {
                Vector3 i = item.position;
                float distance = Vector3.Distance(i, transform.position);
                if (distance < 最近距离)
                {
                    最近距离 = distance;
                    临时最近单位 = item;
                }
            }
            目标角色 = 临时最近单位.gameObject;
        }
        if (目标角色 != null)
        {
            Vector3 position1 = 目标角色.transform.position;//目标坐标
            Vector3 position2 = transform.position;//自己的坐标
            distance = position1 - position2;
        }
        else
        {
            Vector3 position1 = transform.position + new Vector3(1, 0, 0);//目标坐标
            Vector3 position2 = transform.position;//自己的坐标
            distance = position1 - position2;
        }
    }

    void FixedUpdate()
    {
        if (cango == true)
        {
            Vector3 向量 = new Vector3(distance.x, 0, distance.z).normalized * speed;
            GetComponent<Rigidbody>().linearVelocity = 向量;
            float angle = Mathf.Atan2(distance.z, distance.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);



            if (目标角色 == null)
            {
                获取目标();
            }

            lifetime -= Time.fixedDeltaTime;
            if (lifetime <= 0)
            {
                销毁自己();
            }
        }
    }






}
