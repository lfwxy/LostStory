using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// 場景狀態控制者
public class SceneStateController
{
    private ISceneState m_State;
    private bool m_bRunBegin = false;
    private static AsyncOperation AsyncOperation;//异步操作
    public SceneStateController()
    { }

    // 設定狀態
    public void SetState(ISceneState State, string LoadSceneName)
    {
        //Debug.Log ("SetState:"+State.ToString());
        m_bRunBegin = false;

        // 載入場景
        LoadScene(LoadSceneName);

        // 通知前一個State結束
        if (m_State != null)
            m_State.StateEnd();

        // 設定
        m_State = State;
    }

    // 載入場景
    private void LoadScene(string LoadSceneName)
    {
        if (LoadSceneName == null || LoadSceneName.Length == 0)
            return;
        //Application.LoadLevel( LoadSceneName );
        AsyncOperation = SceneManager.LoadSceneAsync(LoadSceneName);//异步操作状态
    }

    // 更新
    public void StateUpdate()
    {
        if (AsyncOperation != null)
        {
            if (AsyncOperation.isDone == false)
            {
                Debug.Log("没加载完");
                return;
            }
        }

        // 通知新的State開始
        if (m_State != null && m_bRunBegin == false)
        {
            m_State.StateBegin();
            m_bRunBegin = true;
        }

        if (m_State != null)
            m_State.StateUpdate();
    }
}
