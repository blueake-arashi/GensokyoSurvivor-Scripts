using System.Collections;
using UnityEngine;

public class 获得新技能类 : 升级项base
{


    public override void 选择升级()
    {

        player = GameObject.Find("玩家对象层").transform.GetChild(0).gameObject.GetComponent<Player>();
        battleUI = GameObject.Find("battleUI").GetComponent<battleUI>();
        Instantiate(对应技能.gameObject, player.技能表);
        battleUI.刷新技能();
        关闭三选一();
    }


}
 

    