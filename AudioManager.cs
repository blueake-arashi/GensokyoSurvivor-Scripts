using UnityEngine;
using UnityEngine.UI;

public class 声音管理 : MonoBehaviour
{
    public AudioClip 标题画面bgm;
    public AudioClip 战斗bgm;
    public AudioClip UI按钮音效;
    public AudioClip 拾取音效;
    public AudioSource BGM播放器;
    public AudioSource SE播放器;
    public float BGM音量 = 0.5f;
    public float SE音量 = 0.5f;



    public void 同步显示BMG滑条(Slider bgm)
    {
        BGM音量 = bgm.value;
        BGM播放器.volume = BGM音量;
        储存音量();
    }
    public void 同步显示SE滑条(Slider se)
    {

        SE音量 = se.value;
        SE播放器.volume = SE音量;
        储存音量();
    }

    public void 储存音量()
    {
        ES3.Save<float>("SE音量", SE音量);
        ES3.Save<float>("BGM音量", BGM音量);
    }


    public void 播放BGM(AudioClip BGM)
    {
        BGM播放器.volume = BGM音量;
        BGM播放器.clip = BGM;
        BGM播放器.Play();

    }

    public void 播放UI按钮音效()
    {
        SE播放器.volume = SE音量;
        SE播放器.clip = UI按钮音效;
        SE播放器.Play();

    }


    public void 播放拾取音效()
    {
        SE播放器.volume = SE音量;
        SE播放器.clip = 拾取音效;
        SE播放器.Play();

    }

    public void 播放自定义音效(AudioClip SE)
    {
        SE播放器.volume = SE音量;
        SE播放器.clip = SE;
        SE播放器.Play();

    }
}
