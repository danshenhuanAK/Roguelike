using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWay : MonoBehaviour
{
    private ObjectPool objectPool;

    [SerializeField]
    private GameObject[] gameObjects;           //Ԥ��������
    public GameObject line;                     //����
    private GameObject[,] games;                //���ɶ�������
    //ÿ�����������ӵ���һ�㷿��
    private Dictionary<GameObject, List<GameObject>> dir = new Dictionary<GameObject, List<GameObject>>();

    [SerializeField]
    private int maxRoom;                        //ÿ��������
    [SerializeField]
    private int maxFloor;                       //������
    private int[] roomCounts;                   //ʵ��ÿ�����

    [SerializeField]
    private float lineWidth;                    //�������

    private float rawImageHeight;               //�����߶�

    public Vector2 maxRevisePosition;           //������ɷ�Χ
    public Vector2 minRevisePosition;           //��С���ɷ�Χ
    public float lineJuegePosX;                 //�������ƾ���
    public float roomJudgePosX;                 //����֮�����ƾ���

    public GameObject lastClickRoom;            //�ϴε������

    private void Awake()
    {
        objectPool = ObjectPool.Instance;

        lastClickRoom = null;
        rawImageHeight = gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        roomCounts = new int[maxFloor];
        games = new GameObject[maxFloor, maxRoom + 1];

        SetRoom();
    }

    private void OnEnable()
    {
        if(lastClickRoom == null)
        {
            for(int i = 0; i < roomCounts[0]; i++)
            {
                games[0, i].GetComponent<MapButtonFlash>().isButton = true;
            }
        }
        else
        {
            for (int i = 0; i < dir[lastClickRoom].Count; i++)
            {
                dir[lastClickRoom][i].GetComponent<MapButtonFlash>().isButton = true;
            }
        }
    }

    public void SetRoom()
    {
        float beforeFloorAveragePosY;                       //��һ�㷿��yֵ��ƽ��ֵ

        roomCounts[0] = Random.Range(2, 5);

        CreatObject(new Vector2(maxRevisePosition.x, -rawImageHeight + 70), new Vector2(minRevisePosition.x, -rawImageHeight + 50), 0, roomCounts[0], 0);

        beforeFloorAveragePosY = GetAveragePositionY(0);

        for (int i = 1; i < maxFloor - 1; i++)
        {
            Vector2 maxPosition;
            Vector2 minPosition;
            float beforePosX = 0;
            float isCreatNext;
            roomCounts[i] = 0;

            if (roomCounts[i - 1] == 1)                      //��һ��Ϊ1���������ɲ����ӱ���ȫ������
            {
                Vector3 linePosition = games[i - 1, 0].transform.localPosition;

                while (roomCounts[i] <= 3)                      //�ϲ�Ϊһ����������һ�����������������
                {
                    maxPosition = new Vector2(Mathf.Min(linePosition.x + lineJuegePosX, maxRevisePosition.x),
                        linePosition.y + maxRevisePosition.y);

                    if (roomCounts[i] == 0)                     //��һ���������С���ɷ�Χ���ݴ˷������ӵ���һ�㷿���������
                    {
                        minPosition = new Vector2(Mathf.Max(linePosition.x - lineJuegePosX, minRevisePosition.x),
                            linePosition.y + maxRevisePosition.y);
                    }
                    else                                        //֮��ķ������С���ɷ�Χ������һ����һ�����ɵķŽ���������
                    {
                        minPosition = new Vector2(Mathf.Max(beforePosX + roomJudgePosX, minRevisePosition.x),
                            linePosition.y + maxRevisePosition.y);
                    }

                    CreatObject(maxPosition, minPosition, i, 1, roomCounts[i]);
                    SetLine(games[i - 1, 0], games[i, roomCounts[i]]);
                    roomCounts[i]++;
                    beforePosX = games[i, roomCounts[i] - 1].transform.localPosition.x;

                    //��֤�������ɵķ��䲻�����ӷ���̫Զ
                    if (Mathf.Min(linePosition.x + lineJuegePosX, maxRevisePosition.x) - beforePosX >= roomJudgePosX && roomCounts[i] < 3)
                    {
                        isCreatNext = Random.Range(0f, 1f);

                        if (isCreatNext >= 0.5f)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                beforeFloorAveragePosY = GetAveragePositionY(i);

                continue;
            }

            int currentFloorMaxRoom = Mathf.Min(roomCounts[i - 1] * 3, maxRoom);        //������һ�㷿�����������ǰ�㷿��������

            int lineCount = 1;
            float isLineBefore;

            //��Ϊ��һ���һ����������һ�����ӷ��䣬�������ɷ�����ݴ˷������λ������
            maxPosition = new Vector2(Mathf.Min(games[i - 1, 0].transform.localPosition.x + lineJuegePosX, maxRevisePosition.x),
                        beforeFloorAveragePosY + maxRevisePosition.y);

            minPosition = new Vector2(Mathf.Max(games[i - 1, 0].transform.localPosition.x - lineJuegePosX, minRevisePosition.x),
                        beforeFloorAveragePosY + minRevisePosition.y);

            CreatObject(maxPosition, minPosition, i, 1, 0);
            SetLine(games[i - 1, 0], games[i, 0]);
            roomCounts[i]++;
            beforePosX = games[i, roomCounts[i] - 1].transform.localPosition.x;
            currentFloorMaxRoom--;

            for (int j = 1; j < roomCounts[i - 1]; j++)
            {
                float linePosX = games[i - 1, j].transform.localPosition.x;

                //��һ�����һ��������뵽��ǰ���ӵ�Ŀ����ӷ�Χ
                if (beforePosX >= Mathf.Max(linePosX - lineJuegePosX, minRevisePosition.x) &&
                        beforePosX < Mathf.Min(linePosX + lineJuegePosX, maxRevisePosition.x))
                {
                    /*
                     3������±���������һ�����һ������
                        1:��һ�����һ������X���ڵ�ǰ���ӷ���X
                        2:��ǰ���Ѿ����ɷ��������
                        3:��ǰ���ӷ����Ѿ�û��ʣ��λ�������µķ���
                     */
                    if (beforePosX >= linePosX || currentFloorMaxRoom == 0 ||
                            Mathf.Min(linePosX + lineJuegePosX, maxRevisePosition.x) - beforePosX < roomJudgePosX)
                    {
                        SetLine(games[i - 1, j], games[i, roomCounts[i] - 1]);
                        lineCount = 0;
                        continue;
                    }
                    else
                    {
                        isLineBefore = Random.Range(0f, 1f);

                        if (isLineBefore >= 0.65f)
                        {
                            SetLine(games[i - 1, j], games[i, roomCounts[i] - 1]);
                            lineCount = 1;
                        }
                        else
                        {
                            lineCount = 0;
                        }
                    }
                }
                else if (roomCounts[i] != 0)
                {
                    lineCount = 0;
                }

                //��ǰ���ӷ���������Ϊ0ʱҪ��������һ��������������
                if (lineCount == 0)
                {
                    maxPosition = new Vector2(Mathf.Min(linePosX + lineJuegePosX, maxRevisePosition.x),
                        beforeFloorAveragePosY + maxRevisePosition.y);

                    minPosition = new Vector2(Mathf.Max(beforePosX + roomJudgePosX, linePosX - lineJuegePosX),
                        beforeFloorAveragePosY + minRevisePosition.y);

                    CreatObject(maxPosition, minPosition, i, 1, roomCounts[i]);
                    SetLine(games[i - 1, j], games[i, roomCounts[i]]);

                    roomCounts[i]++;
                    beforePosX = games[i, roomCounts[i] - 1].transform.localPosition.x;
                    lineCount++;
                    currentFloorMaxRoom--;
                }

                //��ǰ���ӵ����ɷ���Ҫ�������δ���ӵ����ٿ�������һ������
                if (Mathf.Min(linePosX + lineJuegePosX, maxRevisePosition.x) - beforePosX >= roomJudgePosX && currentFloorMaxRoom > roomCounts[i - 1] - j)
                {
                    isCreatNext = Random.Range(0f, 1f);
                }
                else
                {
                    lineCount = 0;
                    continue;
                }

                while (true)
                {
                    if (isCreatNext >= 0.5f)            //�ж��Ƿ�������һ������
                    {
                        maxPosition = new Vector2(Mathf.Min(linePosX + lineJuegePosX, maxRevisePosition.x),
                           beforeFloorAveragePosY + maxRevisePosition.y);

                        minPosition = new Vector2(Mathf.Max(beforePosX + roomJudgePosX, minRevisePosition.x),
                                beforeFloorAveragePosY + minRevisePosition.y);

                        CreatObject(maxPosition, minPosition, i, 1, roomCounts[i]);
                        SetLine(games[i - 1, j], games[i, roomCounts[i]]);

                        roomCounts[i]++;
                        beforePosX = games[i, roomCounts[i] - 1].transform.localPosition.x;
                        lineCount++;
                        currentFloorMaxRoom--;
                    }
                    else                                 //�������ӵĻ�������һ�����ӵ������   
                    {
                        lineCount = 0;
                        break;
                    }

                    //�ж�������ӵ��Ƿ�߱��ٴ����ӵ��������������ٴ����isCreatNext,�����������������һ���������
                    if (Mathf.Min(linePosX + lineJuegePosX, maxRevisePosition.x) - beforePosX >= roomJudgePosX && lineCount < 3 && currentFloorMaxRoom > 0)
                    {
                        isCreatNext = Random.Range(0f, 1f);
                    }
                    else
                    {
                        lineCount = 0;
                        break;
                    }
                }
            }

            beforeFloorAveragePosY = GetAveragePositionY(i);
        }
    }
    private float GetAveragePositionY(int floor)                    //�����һ�㷿���ƽ��yֵ
    {
        float maxPosY = -1000f;
        float minPosY = 1000f;

        if (roomCounts[floor] == 1)
        {
            return games[floor, 0].transform.localPosition.y;
        }

        for (int i = 0; i < roomCounts[floor]; i++)
        {
            if (games[floor, i].transform.localPosition.y < minPosY)
            {
                minPosY = games[floor, i].transform.localPosition.y;
            }
            if (games[floor, i].transform.localPosition.y > maxPosY)
            {
                maxPosY = games[floor, i].transform.localPosition.y;
            }
        }

        return (maxPosY + minPosY) / 2;
    }

    private void CreatObject(Vector2 maxPosition, Vector2 minPosition, int floor, int roomCount, int startPos)
    {
        int kind;
        float posX = 0;
        float posY;

        if (floor == 0)
        {
            kind = 1;

            for (int i = 0; i < roomCount; i++)
            {
                if (i == 0)
                {
                    posX = Random.Range(minPosition.x, maxPosition.x - (roomCount - 1) * roomJudgePosX);
                }
                else
                {
                    posX = Random.Range(posX + roomJudgePosX, maxPosition.x - (roomCount - i - 1) * roomJudgePosX);
                }
                posY = Random.Range(minPosition.y, maxPosition.y);

                games[floor, startPos + i] = objectPool.CreateObject(gameObjects[kind].name, gameObjects[kind],
                    gameObject, new Vector3(posX, posY, 1000), Quaternion.identity);
                games[floor, startPos + i].transform.localPosition = new Vector3(posX, posY, 0);
            }
            return;
        }
        else if (floor == 9)
        {
            kind = 0;
        }
        else
        {
            float randomKind = Random.Range(0f, 1f);

            if (randomKind <= 0.65f)
            {
                kind = 1;
            }
            else
            {
                kind = 2;
            }
        }

        posX = Random.Range(minPosition.x, maxPosition.x);
        posY = Random.Range(minPosition.y, maxPosition.y);

        games[floor, startPos] = objectPool.CreateObject(gameObjects[kind].name, gameObjects[kind],
            gameObject, new Vector3(posX, posY, 1000), Quaternion.identity);
        games[floor, startPos].transform.localPosition = new Vector3(posX, posY, 0);
    }
    private void SetLine(GameObject statrObject, GameObject endObject)
    {
        Transform startPos = statrObject.transform;             //�������
        Transform endPos = endObject.transform;               //�����յ�

        GameObject lineObject;

        Vector3 mid = (startPos.position + endPos.position) / 2;

        if (!dir.ContainsKey(statrObject))
        {
            dir.Add(statrObject, new List<GameObject>());
        }
        dir[statrObject].Add(endObject);

        lineObject = objectPool.CreateObject(line.name, line, gameObject, mid, Quaternion.identity);

        //�㼶�ö�
        lineObject.transform.SetAsFirstSibling();

        //����
        lineObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Vector3.Distance(startPos.localPosition, endPos.localPosition), lineWidth);

        //��ת
        Vector3 direction = endPos.localPosition - lineObject.transform.localPosition;
        direction.z = 0;
        float angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.forward);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        lineObject.transform.rotation = rotation;
    }
}
