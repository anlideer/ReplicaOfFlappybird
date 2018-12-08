using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 卡牌3和4的功能（黑屏 | 隐身）
public class Skill34 : MonoBehaviour {

    public Camera cam;
    public Animator playerAnim;

    bool setted = false;
    float totalTime;
    ArrayList toBlack = new ArrayList();
    ArrayList toDisappear = new ArrayList();
    int times1, times2;
    Vector3 camP = new Vector3(0, 0, -10);
    Vector3 camBlackP = new Vector3(0, 0, 10);
    int cnt1, cnt2;
    bool blacking = false;
    float nextTime = 0;
    float thisTime = 0;

	// Use this for initialization
	void Start () {
        setted = false;
        totalTime = PlayerPrefs.GetInt("HighestScore", 0);
        if (totalTime < 20)
            totalTime = 20;
        totalTime = 2f * totalTime;    // 先不管管子速度的影响了，反正本来就很粗略
        toBlack.Clear();
        toDisappear.Clear();
        cnt1 = 0;
        cnt2 = 0;
        blacking = false;
        nextTime = 0;
        thisTime = Time.time;
    }

    // Update is called once per frame
    void Update () {
        // 初始化抽卡结果
		if (!setted && GM.skillSetted)
        {
            setted = true;
            float tmp = 0;
            times1 = GM.skills[3];
            times2 = GM.skills[4];
            // 3
            for (int i = 0; i < times1; i++)
            {
                bool flag = true;
                while (flag)
                {
                    flag = false;
                    tmp = Random.Range(5, totalTime);   // 最开头几秒还是算了8...
                    for (int j = 0; j < i; j++)
                    {
                        if (Mathf.Abs((float)toBlack[j] - tmp) < 1f)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                toBlack.Add(tmp);
            }
            // 4
            for (int i = 0; i < times2; i++)
            {
                bool flag = true;
                while(flag)
                {
                    flag = false;
                    tmp = Random.Range(5, totalTime);
                    for (int j = 0; j < i; j++)
                    {
                        if (Mathf.Abs((float)toDisappear[j] - tmp) < 2f)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        for (int j = 0; j < times1; j++)
                        {
                            if (Mathf.Abs((float)toBlack[j] - tmp) < 2f)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                }
                toDisappear.Add(tmp);
            }
            toBlack.Sort();
            toDisappear.Sort();
        }


        // 执行
        ReleaseSkill();
	}

    private void ReleaseSkill()
    {
        // 3
        if (cnt1 < times1)
        {
            if (Mathf.Abs(Time.time - ((float)toBlack[cnt1] + thisTime)) < 0.5f)
            {
                cnt1++;
                blacking = true;
                nextTime = Time.time + 1f;
                cam.transform.position = camBlackP;
            }
        }
        if (blacking && nextTime < Time.time)
        {
            blacking = false;
            cam.transform.position = camP;
        }
        
        // 4
        if (cnt2 < times2)
        {
            if (Mathf.Abs(Time.time - ((float)toDisappear[cnt2] + thisTime)) < 0.5f)
            {
                cnt2++;
                playerAnim.Play("birdDisappear");
            }
        }
    }
}
