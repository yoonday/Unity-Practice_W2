using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Q1 [�������� 1]
    private static QuestManager instance;

    // Q2 [�������� 2]
    public static QuestManager Instance
    {

        get // ���� ���� ��
        {
            if (instance == null) // instance�� null���� Ȯ���ϰ�
            {
                instance = FindObjectOfType<QuestManager>(); // null�̶�� QuestManager Ÿ���� ���� ������Ʈ�� ã�Ƽ� ��������

                if (instance == null) // ���� �������� �ұ��ϰ� null�̶��
                {
                    // ���ο� ���ӿ�����Ʈ�� �����ϼ���
                    GameObject gameObject = new GameObject(nameof(QuestManager)); // �̸��� 'QuestManager'��
                    instance = gameObject.AddComponent<QuestManager>();
                }

            }
            
            
            return instance;
        }
        
    }

    // [�������� 3] �ν��Ͻ� �˻� ����
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject); // �̹� ������ �ν��Ͻ��� �ִٸ� ���� ������ ���� ������Ʈ �ı��ϱ�

        instance = this;
    }
}
