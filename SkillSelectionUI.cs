using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class 三选一UI : MonoBehaviour
{
    public List<GameObject> 选项表;
    public List<GameObject> 提升自己属性;
    public List<GameObject> 提升火球术;
    public List<GameObject> 提升飓风; 
    public GameObject 飓风;
    public GameObject 黑暗齿轮;
    public Transform 选项框1;
    public Transform 选项框2;
    public Transform 选项框3;
    public GameObject 选项1;
    public GameObject 选项2;
    public GameObject 选项3;
    public Transform 玩家技能表;
    public Player 玩家;
    private 声音管理 声音;

    void OnEnable()
    {
        刷新三选一();
        声音=GameObject.Find("声音管理").GetComponent<声音管理>();  


    }
    private void Update()
    {
        if(玩家.玩家死亡==true)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }




    public void 刷新三选一()
    {
        选项表 = new List<GameObject>();
        选项表.AddRange(提升自己属性);
        选项表.AddRange(提升火球术);
        bool 有飓风 = false;
        bool 有黑暗齿轮 = false;
         
        foreach (Transform 技能 in 玩家技能表)
        {
            Skillbase skill = 技能.GetComponent<Skillbase>();
            if (skill.Skillname == "飓风")
            {
                有飓风 = true;

            }
            if (skill.Skillname == "黑暗齿轮")
            {
                有黑暗齿轮 = true;

            }
        }
         
        if (有飓风)
        {
            选项表.AddRange(提升飓风);
        }
        else
        {
            选项表.Add(飓风);
        }
        if (有黑暗齿轮)
        {
            //选项表.AddRange(提升黑暗齿轮);
        }
        else
        { 
            选项表.Add(黑暗齿轮);
        }

        选项1 = 获取随机项();
        选项2 = 获取随机项();
        while (选项1 == 选项2)
        {
            选项2 = 获取随机项();
        }

        选项3 = 获取随机项();
        while (选项1 == 选项3 || 选项2 == 选项3)
        {
            选项3 = 获取随机项();
        }




        刷新单个选项框(选项框1, 选项1);
        刷新单个选项框(选项框2, 选项2);
        刷新单个选项框(选项框3, 选项3);
    }

  

    public GameObject 获取随机项()
    {
        int 项目数量 = 选项表.Count;
        int 随机值 = Random.Range(0, 项目数量);
        return 选项表[随机值];
    }







    public void 刷新单个选项框(Transform 选项框, GameObject 对应选项)
    {
        升级项base 升级项 = 对应选项.GetComponent<升级项base>();
        选项框.GetChild(0).GetComponent<TextMeshProUGUI>().text = 升级项.升级项名称;
        if (升级项.图标)
        {
            选项框.GetChild(2).gameObject.SetActive(true);
            选项框.GetChild(2).GetComponent<Image>().sprite = 升级项.图标;
        }
      
        选项框.GetChild(1).GetComponent<TextMeshProUGUI>().text = 升级项.升级项描述;
    }


    public void Click_选项1()
    {
        声音.播放UI按钮音效();

        选项1.GetComponent<升级项base>().选择升级();
    }
    public void Click_选项2() 
    {
        声音.播放UI按钮音效();

        选项2.GetComponent<升级项base>().选择升级();
    }
    public void Click_选项3()
    {
        声音.播放UI按钮音效();

        选项3.GetComponent<升级项base>().选择升级();
    }

}
