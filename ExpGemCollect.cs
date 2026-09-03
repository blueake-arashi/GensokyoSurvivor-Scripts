using GLTFast.Schema;
using System.Linq;
using UnityEngine;

public class 经验石拾取 : MonoBehaviour
{
    public Player player;
    public bool 是否飞向玩家 = false;
    private 声音管理 声音;

    private void OnEnable()
    {
        player=GameObject.Find("玩家对象层").transform.GetChild(0).gameObject.GetComponent<Player>();
        声音 = GameObject.Find("声音管理").GetComponent<声音管理>();
    }

    //经验石获取+10
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            声音.播放拾取音效();
            Player p=collision.gameObject.GetComponent<Player>();
            p.经验值 += 1;
            if(p.经验值>=p.最大经验值)
            {
                p.经验值 = 0;
                p.升级();
            }

            Destroy(gameObject);
        }
    }


     void Update()
    {
        Collider[] 检测集 = Physics.OverlapSphere(transform.position, player.拾取范围);
        if (检测集.Contains(player.GetComponent<CapsuleCollider>()))
        {
            是否飞向玩家 = true;
        }
        if (是否飞向玩家 == true)
        {
            Vector3 position1 = player.transform.position;
            Vector3 position2 = transform.position;
            Vector3 distance = position1 - position2;
            Vector3 向量 = new Vector3(distance.x, 0, distance.z).normalized * 10;
            transform.position += 向量 * Time.deltaTime;
        }
    }
}
