using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class 标题画面 : MonoBehaviour
{
    public Transform 怪物层;
    public GameObject 战斗画面;
    public GameObject 天赋页面;
    public Player 玩家;

    public TextMeshProUGUI 天赋点文本;
    public Transform 生命值加成容器;
    public Transform 攻击力加成容器;
    public 存档管理 存档管理;
    public 声音管理 声音;
    public Slider SE音量滑条;
    public Slider BGM音量滑条;
    private void OnEnable()
    {
        RenderSettings.fogColor = new Color(0.42f, 0.47f, 0.62f);
        RenderSettings.fogDensity = 0.05f;
        Time.timeScale = 1.0f;
        存档管理.load();
        声音.播放BGM(声音.标题画面bgm);
        SE音量滑条.value = 声音.SE音量;
        BGM音量滑条.value = 声音.BGM音量; 
    }
        
    public void Click_开始游戏()

    {
        声音.播放UI按钮音效();  
        if (怪物层.childCount > 0)//删除所有怪物
        {
            foreach (Transform monster in 怪物层)
            {
                Destroy(monster.gameObject);
            }
        }
        战斗画面.SetActive(true);
        RenderSettings.fogColor = new Color(0.23f, 0.37f, 0.42f);
        RenderSettings.fogDensity = 0.025f;
        玩家.玩家死亡 = false;
        玩家.transform.position = new Vector3(-1.721f, 10f, -7.531f);



        玩家.同步初始属性();
        玩家.天赋加成();
        gameObject.SetActive(false);



    }







    public void 调整BGM()
    {
        声音.同步显示BMG滑条(BGM音量滑条);
    }
    public void 调整SE()
    {
        声音.同步显示SE滑条(SE音量滑条);
    }







    public void Click_加入玩家群()
    {
        声音.播放UI按钮音效();

        Application.OpenURL("https://qm.qq.com/q/f38UBteUrm");
    }

    public void 打开天赋()
    {
        天赋页面.SetActive(true);
        刷新天赋页面();
    }
    public void 关闭天赋()
    {
        天赋页面.SetActive(false);
    }
    public void Click_退出游戏()
    {
        声音.播放UI按钮音效();

        Application.Quit();
    }

    public void Click_增加生命值()
    {
        声音.播放UI按钮音效();

        增加天赋("生命值");
    }
    public void Click_减少生命值()
    {
        声音.播放UI按钮音效();

        减少天赋("生命值");
    }
    public void Click_增加攻击力()
    {
        声音.播放UI按钮音效();

        增加天赋("攻击力");

    }
    public void Click_减少攻击力()
    {
        声音.播放UI按钮音效();

        减少天赋("攻击力");
    }


    //减少天赋
    public void 减少天赋(string 天赋名)
    {
        int 当前次数 = (天赋名 == "生命值") ? 玩家.生命值加成 : 玩家.攻击力加成;

        if (当前次数 > 0)
        {
            // 返还的天赋点等于【当前这级】升上来时消耗的点数
            int 返还天赋点 = (当前次数 - 1) * 10 + 10;
            switch (天赋名)
            {
                case "生命值":
                    玩家.生命值加成 -= 1;
                    break;
                case "攻击力":
                    玩家.攻击力加成 -= 1;
                    break;
            }

            玩家.天赋点 += 返还天赋点; // 正确返还天赋点
            刷新天赋页面();
        }
    }


    public void 增加天赋(string 天赋名)
    {
        int 当前次数 = (天赋名 == "生命值") ? 玩家.生命值加成 : 玩家.攻击力加成;
        int 当前消耗天赋值 = 当前次数 * 10 + 10;
        if (玩家.天赋点 >= 当前消耗天赋值)
        {
            玩家.天赋点 -= 当前消耗天赋值;
            switch (天赋名)
            {
                case "生命值":
                    玩家.生命值加成 += 1;
                    break;
                case "攻击力":
                    玩家.攻击力加成 += 1;
                    break;
            }
            刷新天赋页面();
        }
    }


    public void Click_重置天赋()
    {
        声音.播放UI按钮音效();

        玩家.天赋点 = 玩家.真实天赋点;
        玩家.生命值加成 = 0;
        玩家.攻击力加成 = 0;
        刷新天赋页面();
    }


    public void 刷新天赋页面()
    {
        // ⭐ 核心修复：第二参数应该传【点了几次加成】，而不是【玩家的初始HP/ATT数值】！
        刷新单个天赋容器(生命值加成容器, 玩家.生命值加成, 20, "生命值");
        刷新单个天赋容器(攻击力加成容器, 玩家.攻击力加成, 5, "攻击力");

        天赋点文本.text = "TP:" + 玩家.天赋点.ToString();
    }


    public void 刷新单个天赋容器(Transform 容器, int 天赋次数, int 倍数, string 天赋名)
    {
        TextMeshProUGUI 属性文本 = 容器.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI 消耗文本 = 容器.GetChild(2).GetComponent<TextMeshProUGUI>();

        string 英文属性名 = "";
        if (天赋名 == "生命值") 英文属性名 = "Max HP";
        else if (天赋名 == "攻击力") 英文属性名 = "ATK";

        if (属性文本 != null)
        {
            // 拼接成英文：Max HP+20 (Added:0)
            属性文本.text = 英文属性名 + "+" + (倍数 + 天赋次数 * 倍数) +
                          "<color=#FF6C6C>(Added:" + (天赋次数 * 倍数) + ")</color>";
        }

        if (消耗文本 != null)
        {
            // 2. 翻译消耗
            消耗文本.text = "Cost: " + (天赋次数 * 10 + 10);
        }
    }


}
