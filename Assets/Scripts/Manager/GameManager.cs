using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private string playerTag;
    

    public Transform Player {  get; private set; } // 플레이어의 위치, 회전을 제어하고자 함. GameObject의 전체를 가져올 필요 없이, Transform에만 접근
    public ObjectPool ObjectPool { get; private set; } // 오브젝트 풀 - 게임 전반에 사용되기에 GameManager로 이동

    private void Awake()
    {
        if(Instance != null) Destroy(gameObject); // 이미 인스턴스가 있다면 파괴한다
        Instance = this; // 없을 경우, 인스턴스를 현재 오브젝트(본인)으로 채움

        Player = GameObject.FindGameObjectWithTag(playerTag).transform; // player 태그를 가진 게임 오브젝트를 찾아, 해당 오브젝트의 위치, 회전, 크기 정보를 Player 변수에 할당함
                                                                        // FindGameObjectsWithTag - Objects일 경우, 배열이라 바로 transform 접근이 되지 않는다.

        ObjectPool = GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
