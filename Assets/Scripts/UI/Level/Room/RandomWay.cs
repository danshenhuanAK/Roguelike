using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RandomWay : MonoBehaviour
{
    private ObjectPool objectPool;
    private DataManager dataManager;

    [SerializeField]
    private GameObject[] gameObjects;           //Ԥ����������1�����䣺0�����ʱ�䣺2
    public GameObject line;                     //����

    private GameObject[,] games;                //ÿһ�����ɵķ���

    [SerializeField]
    private int maxRoom;                        //ÿ��������
    [SerializeField]
    private int maxFloor;                       //������
    private int[] roomCounts;                   //ʵ��ÿ�����
    private int currenRoomNum;                  //��ǰ������

    private float rawImageHeight;               //�����߶�
    [SerializeField]
    private float lineWidth;                    //�������

    public Vector2 maxRevisePosition;           //������ɷ�Χ
    public Vector2 minRevisePosition;           //��С���ɷ�Χ
    public float lineJuegePosX;                 //�������ƾ���
    public float roomJudgePosX;                 //����֮�����ƾ���

    public GameObject lastClickRoom;            //�ϴε������

    private void Awake()
    {
        objectPool = ObjectPool.Instance;
        dataManager = DataManager.Instance;

        lastClickRoom = null;
        rawImageHeight = gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        dataManager.maxFloor = maxFloor;
        roomCounts = new int[maxFloor];
        games = new GameObject[maxFloor, maxRoom + 1];
    }

    private void Start()
    {
        if (dataManager.levelDatas.Count == 0)
        {
            SetRoom();
        }
        else
        {
            LoadRoom(dataManager.levelDatas);
        }
    }

    public void SetRoom()
    {
        float beforeFloorAveragePosY;                       //��һ�㷿��yֵ��ƽ��ֵ

        //���������2~4����һ��ķ��䣬���Ҷ�Ϊ���﷿��
        roomCounts[0] = Random.Range(2, 5);
        CreatObject(new Vector2(maxRevisePosition.x, -rawImageHeight + maxRevisePosition.y),
                    new Vector2(minRevisePosition.x, -rawImageHeight + minRevisePosition.y), 0, roomCounts[0], 0);
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

        //���һ��Ϊ���﷿�䣬��Ϊһ��
        Vector2 minPos = new (0f, beforeFloorAveragePosY + minRevisePosition.y);
        Vector2 maxPos = new(0f, beforeFloorAveragePosY + maxRevisePosition.y);

        CreatObject(maxPos, minPos, maxFloor - 1, 1, 0);

        for(int i = 0; i < roomCounts[maxFloor - 2]; i++)
        {
            SetLine(games[maxFloor - 2, i], games[maxFloor - 1, 0]);
        }
    }

    public void ChangeLevelIsButton(LevelData buttonData)
    {
        if(buttonData.roomFloor == 0)
        {
            for(int i = 0; i < roomCounts[0]; i++)
            {
                games[0, i].GetComponent<MapButton>().levelData.isButton = false;
            }
        }
        else
        {
            buttonData.isButton = false;
        }

        foreach (int lineNum in buttonData.lineRoomFloorNum)
        {
            games[buttonData.roomFloor + 1, lineNum].GetComponent<MapButton>().levelData.isButton = true;
        }
    }

    private float GetAveragePositionY(int floor)                    //�����һ�㷿���ƽ��yֵ
    {
        float maxPosY = games[floor, 0].transform.localPosition.y;
        float minPosY = games[floor, 0].transform.localPosition.y;

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

    private void CreatObject(Vector2 maxPosition, Vector2 minPosition, int floor, int creatCount, int roomFloorNum)
    {
        int kind;
        float posX = 0;
        float posY;

        if (floor == 0 || floor == maxFloor - 1)
        {
            kind = 1;

            for (int i = 0; i < creatCount; i++)
            {
                if (i == 0)
                {
                    posX = Random.Range(minPosition.x, maxPosition.x - (creatCount - 1) * roomJudgePosX);
                }
                else
                {
                    posX = Random.Range(posX + roomJudgePosX, maxPosition.x - (creatCount - i - 1) * roomJudgePosX);
                }
                posY = Random.Range(minPosition.y, maxPosition.y);

                games[floor, roomFloorNum + i] = objectPool.CreateObject(gameObjects[kind].name, gameObjects[kind],
                    gameObject, Vector3.zero, Quaternion.identity);
                games[floor, roomFloorNum + i].transform.localPosition = new Vector3(posX, posY, 0);
                games[floor, roomFloorNum + i].GetComponent<MapButton>().levelData.roomPosX = posX;
                games[floor, roomFloorNum + i].GetComponent<MapButton>().levelData.roomPosY = posY;
                games[floor, roomFloorNum + i].GetComponent<MapButton>().levelData.roomNum = currenRoomNum;
                games[floor, roomFloorNum + i].GetComponent<MapButton>().levelData.roomFloor = floor;
                games[floor, roomFloorNum + i].GetComponent<MapButton>().levelData.roomFloorNum = i;
                if(floor == 0)
                {
                    games[floor, roomFloorNum + i].GetComponent<MapButton>().levelData.isButton = true;
                }
                else
                {
                    games[floor, roomFloorNum + i].GetComponent<MapButton>().levelData.isButton = false;
                }
                games[floor, roomFloorNum + i].GetComponent<MapButton>().levelData.kind = kind;
                currenRoomNum++;
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

        games[floor, roomFloorNum] = objectPool.CreateObject(gameObjects[kind].name, gameObjects[kind],
            gameObject, Vector3.zero, Quaternion.identity);
        games[floor, roomFloorNum].transform.localPosition = new Vector3(posX, posY, 0);
        games[floor, roomFloorNum].GetComponent<MapButton>().levelData.roomPosX = posX;
        games[floor, roomFloorNum].GetComponent<MapButton>().levelData.roomPosY = posY;
        games[floor, roomFloorNum].GetComponent<MapButton>().levelData.roomNum = currenRoomNum;
        games[floor, roomFloorNum].GetComponent<MapButton>().levelData.roomFloor = floor;
        games[floor, roomFloorNum].GetComponent<MapButton>().levelData.roomFloorNum = roomFloorNum;
        games[floor, roomFloorNum].GetComponent<MapButton>().levelData.kind = kind;
        currenRoomNum++;
    }
    private void SetLine(GameObject startObject, GameObject endObject)
    {
        Transform startPos = startObject.transform;             //�������
        Transform endPos = endObject.transform;               //�����յ�

        GameObject lineObject;

        Vector3 mid = (startPos.position + endPos.position) / 2;

        startObject.GetComponent<MapButton>().levelData.lineRoomNum.Add(endObject.GetComponent<MapButton>().levelData.roomNum);
        startObject.GetComponent<MapButton>().levelData.lineRoomFloorNum.Add(endObject.GetComponent<MapButton>().levelData.roomFloorNum);

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

    private void LoadRoom(List<LevelData> levelDatas)
    {
        foreach(LevelData roomdata in levelDatas)
        {
            GameObject room = objectPool.CreateObject(gameObjects[roomdata.kind].name, gameObjects[roomdata.kind], gameObject, Vector3.zero, Quaternion.identity);
            room.transform.localPosition = new Vector3((float)roomdata.roomPosX, (float)roomdata.roomPosY, 0);
            room.GetComponent<MapButton>().levelData = roomdata;

            games[roomdata.roomFloor, roomdata.roomFloorNum] = room;
            roomCounts[roomdata.roomFloor]++;

            foreach(int lineRoom in roomdata.lineRoomNum)
            {
                Vector3 endRoomPos = new Vector3((float)levelDatas[lineRoom].roomPosX, (float)levelDatas[lineRoom].roomPosY, 0);
                Vector3 mid = (room.transform.localPosition + endRoomPos) / 2;

                GameObject lineObj = objectPool.CreateObject(line.name, line, gameObject, mid, Quaternion.identity);
                lineObj.transform.localPosition = mid;
                lineObj.transform.SetAsFirstSibling();
                lineObj.GetComponent<RectTransform>().sizeDelta = new Vector2(Vector3.Distance(room.transform.localPosition, endRoomPos), lineWidth);

                Vector3 direction = endRoomPos - lineObj.transform.localPosition;
                direction.z = 0;
                float angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.forward);
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                lineObj.transform.rotation = rotation;
            }
        }
    }
}
