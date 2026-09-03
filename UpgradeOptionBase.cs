using UnityEngine;

public class 升级项base : MonoBehaviour
{
    public string 升级项名称;
    public string 升级项描述;
    public 升级项类型 类型;
    public float 提升数值;
    public 玩家升级属性 玩家属性;
    public 技能升级属性 技能属性;
    public Player player;
    public Sprite 图标;
    public Skillbase 对应技能;
    public battleUI battleUI;


    public enum 升级项类型
    {
        提升自己属性,
        提升已有技能,
        获得新技能,

    }

    public enum 玩家升级属性
    {
        HP,//血量
        ATT,//攻击力
        DEF,//防御力
        CritRate,//暴击率
        Dodge,//闪避率
    }
    public enum 技能升级属性
    {
        伤害,
        生命周期,
        穿透,
        数量,
        大小,

    }

   　void OnEnable()
    {
        player = GameObject.Find("玩家对象层").transform.GetChild(0).gameObject.GetComponent<Player>();
        battleUI = GameObject.Find("battleUI").GetComponent<battleUI>();
    }

    public virtual void 选择升级()
    {

    }
    public void 关闭三选一()
    {
        player = GameObject.Find("玩家对象层").transform.GetChild(0).gameObject.GetComponent<Player>();
        battleUI = GameObject.Find("battleUI").GetComponent<battleUI>();
        Time.timeScale = 1;
        battleUI.三选一UI.SetActive(false); 
    }
  

}
