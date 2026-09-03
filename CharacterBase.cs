using JetBrains.Annotations;
using UnityEngine;

public class 角色基类 : MonoBehaviour
{
    public string Name;//名称
    public int HP;//血量
    public int MaxHP;//最大血量
    public int ATT;//攻击力
    public int DEF;//防御力
    public float CritRate;//暴击率
    public float Dodge;//闪避率
    public int Speed;//速度
    public int Gold;//金币
    public float DropRate;//掉落概率
    public int 经验值;
    public int 最大经验值;
    public int 等级;
}
