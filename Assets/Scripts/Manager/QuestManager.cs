using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Q1 [구현사항 1]
    private static QuestManager instance;

    // Q2 [구현사항 2]
    public static QuestManager Instance
    {

        get // 값을 읽을 때
        {
            if (instance == null) // instance가 null인지 확인하고
            {
                instance = FindObjectOfType<QuestManager>(); // null이라면 QuestManager 타입을 가진 컴포넌트를 찾아서 넣으세요

                if (instance == null) // 위의 과정에도 불구하고 null이라면
                {
                    // 새로운 게임오브젝트를 생성하세요
                    GameObject gameObject = new GameObject(nameof(QuestManager)); // 이름만 'QuestManager'임
                    instance = gameObject.AddComponent<QuestManager>();
                }

            }
            
            
            return instance;
        }
        
    }

    // [구현사항 3] 인스턴스 검사 로직
    private void Awake()
    {
        instance = this;
    }
}
