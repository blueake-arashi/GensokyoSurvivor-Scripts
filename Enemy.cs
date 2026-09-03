using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;
using System.Collections;
public class 怪物类 : 角色基类
{
    public GameObject 伤害数字;
    public 角色状态 状态;
    public GameObject 目标角色;
    private Transform 玩家层;
    private Animator ani;
    public float 缩放;
    public Material 普通材质;
    public Material 受击材质;
    public GameObject 掉落物;
    public AudioClip 被击音效;


    public enum 角色状态
    {
        闲置,
        移动,
        死亡,
    }


    void OnEnable()//被激活的时候调用
    {
        玩家层 = GameObject.Find("玩家对象层").transform;
        ani = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player 玩家 = collision.gameObject.GetComponent<Player>();

            if (玩家.HP > 0)
            {
                玩家.HP -= ATT;
                GameObject 数字 = Instantiate(伤害数字, collision.transform.position, default);

                数字.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ATT.ToString();
                collision.gameObject.GetComponent<Player>().开始受击反馈();
                if (玩家.HP <= 0)
                {
                    玩家.死亡事件();
                }
            }
        }
    } 



    public void 开始受击反馈()
    {
        if (被击音效 != null)
        {
            GetComponent<AudioSource>().clip = 被击音效;
            GetComponent<AudioSource>().volume = GameObject.Find("声音管理").GetComponent<声音管理>().SE音量;
            GetComponent<AudioSource>().Play();
        }
        StartCoroutine(受击反馈());
    }
    public IEnumerator 受击反馈()
    {
        GetComponent<SpriteRenderer>().material = 受击材质;
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().material = 普通材质;
    }

    public void 死亡事件()
    {
        if (状态 != 角色状态.死亡)
        {
            状态 = 角色状态.死亡;
            Instantiate(掉落物, transform.position, Quaternion.Euler(45, 0, 0));
            ani.SetTrigger("死亡");
            StartCoroutine(删除自己());
        }

    }
    public IEnumerator 删除自己()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void 获取目标()
    {
        float 最近距离 = 999;
        Transform 临时最近单位 = null;
        if (玩家层.childCount > 0)
        {
            foreach (Transform item in 玩家层)
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
    }

    void FixedUpdate()
    {
        if (玩家层.GetChild(0).GetComponent<Player>().玩家死亡 == false)
        {
            if (目标角色 != null && 状态 != 角色状态.死亡)
            {
                //目标在左边，怪物朝向向左
                //目标在右边，怪物朝向向右
                float 差值 = 目标角色.transform.position.x - transform.position.x;
                if (差值 > 0)
                {
                    transform.localScale = new Vector3(缩放, 缩放, 缩放);
                }
                else
                {
                    transform.localScale = new Vector3(-1 * 缩放, 缩放, 缩放);
                }
            }







            switch (状态)
            {   //1
                case 角色状态.闲置:
                    ani.SetBool("移动中", false);








                    //如果怪物目标的角色不存在则获取目标
                    //如果怪物目标角色存在则靠近角色
                    if (目标角色 == null)
                    {
                        获取目标();
                    }
                    else
                    {
                        状态 = 角色状态.移动;
                    }

                    break;
                //2
                case 角色状态.移动:

                    if (目标角色 == null)
                    {
                        状态 = 角色状态.闲置;
                    }

                    else
                    {
                        ani.SetBool("移动中", true);
                        Vector3 position1 = 目标角色.transform.position;
                        Vector3 position2 = transform.position;
                        Vector3 distance = position1 - position2;
                        Vector3 向量 = new Vector3(distance.x, 0, distance.z).normalized * Speed;
                        transform.position += 向量 * Time.fixedDeltaTime;

                    }

                    break;

                //3
                case 角色状态.死亡:


                    break;

            }
        }

    }

}