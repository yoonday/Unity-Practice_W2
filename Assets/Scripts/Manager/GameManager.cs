using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private string playerTag;
    

    public Transform Player {  get; private set; } // �÷��̾��� ��ġ, ȸ���� �����ϰ��� ��. GameObject�� ��ü�� ������ �ʿ� ����, Transform���� ����
    public ObjectPool ObjectPool { get; private set; } // ������Ʈ Ǯ - ���� ���ݿ� ���Ǳ⿡ GameManager�� �̵�

    private void Awake()
    {
        if(Instance != null) Destroy(gameObject); // �̹� �ν��Ͻ��� �ִٸ� �ı��Ѵ�
        Instance = this; // ���� ���, �ν��Ͻ��� ���� ������Ʈ(����)���� ä��

        Player = GameObject.FindGameObjectWithTag(playerTag).transform; // player �±׸� ���� ���� ������Ʈ�� ã��, �ش� ������Ʈ�� ��ġ, ȸ��, ũ�� ������ Player ������ �Ҵ���
                                                                        // FindGameObjectsWithTag - Objects�� ���, �迭�̶� �ٷ� transform ������ ���� �ʴ´�.

        ObjectPool = GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
