using UnityEngine;

public class 提升玩家属性类 : 升级项base
{
    public override void 选择升级()
    {
        player = GameObject.Find("玩家对象层").transform.GetChild(0).gameObject.GetComponent<Player>(); 
        battleUI = GameObject.Find("battleUI").GetComponent<battleUI>();
        switch (玩家属性)
        {
            case 玩家升级属性.HP:
                player.MaxHP += (int)提升数值;
                player.HP += (int)提升数值;
                break;

            case 玩家升级属性.ATT:
                player.ATT += (int)提升数值;
                break;
            case 玩家升级属性.DEF:
                player.DEF += (int)提升数值;
                break;
            case 玩家升级属性.Dodge:
                player.Dodge += 提升数值;
                break;
            case 玩家升级属性.CritRate:
                player.CritRate += 提升数值;
                break;
        }
        关闭三选一();

    }
}
