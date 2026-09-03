using UnityEngine;

public class 提升技能属性类 : 升级项base
{
    public override void 选择升级()
    {
        player = GameObject.Find("玩家对象层").transform.GetChild(0).gameObject.GetComponent<Player>();
        battleUI = GameObject.Find("battleUI").GetComponent<battleUI>();
        Skillbase 目标技能 = null;
        // 遍历寻找匹配的技能
        foreach (Transform skill in player.技能表)
        {
            Skillbase s = skill.GetComponent<Skillbase>();
            if (对应技能.Skillname == s.Skillname)
            {
                目标技能 = s;

            }
        }

        // 此时目标技能绝对不为 null，可以安全操作
        switch (技能属性)
        {
            case 技能升级属性.伤害:
                目标技能.damage = Mathf.RoundToInt(目标技能.damage * (1f + 提升数值));
                break;
            case 技能升级属性.大小:
                目标技能.size *= (1f + 提升数值);
                break;
            case 技能升级属性.穿透:
                目标技能.pass += (int)提升数值;

                break;
            case 技能升级属性.数量:
                目标技能.number += (int)提升数值;

                break;
            case 技能升级属性.生命周期:
                目标技能.lifetime += 提升数值;

                break;
        }
        关闭三选一();
    }
}