using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RandomWay : MonoBehaviour
{
    private ObjectPool objectPool;
    private DataManager dataManager;

    [SerializeField]
    private GameObject[] gameObjects;           //预制体数组怪物：1，宝箱：0，随机时间：2
    public GameObject line;                     //线条

    private GameObject[,] games;                //每一层生成的房间

    [SerializeField]
    private int maxRoom;                        //每层最大间数
    [SerializeField]
    private int maxFloor;                       //最大层数
    private int[] roomCounts;                   //实际每层间数
    private int currenRoomNum;                  //当前房间数

    private float rawImageHeight;               //背景高度
    [SerializeField]
    private float lineWidth;                    //线条宽度

    public Vector2 maxRevisePosition;           //最大生成范围
    public Vector2 minRevisePosition;           //最小生成范围
    public float lineJuegePosX;                 //连线限制距离
    public float roomJudgePosX;                 //房间之间限制距离

    public GameObject lastClickRoom;            //上次点击房间

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
        float beforeFloorAveragePosY;                       //上一层房间y值的平均值

        //先随机生成2~4个第一层的房间，并且都为怪物房间
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

            if (roomCounts[i - 1] == 1)                      //上一层为1个房间生成并连接本层全部房间
            {
                Vector3 linePosition = games[i - 1, 0].transform.localPosition;

                while (roomCounts[i] <= 3)                      //上层为一个房间则这一层最多生成三个房间
                {
                    maxPosition = new Vector2(Mathf.Min(linePosition.x + lineJuegePosX, maxRevisePosition.x),
                        linePosition.y + maxRevisePosition.y);

                    if (roomCounts[i] == 0)                     //第一个房间的最小生成范围根据此房间连接的上一层房间进行修正
                    {
                        minPosition = new Vector2(Mathf.Max(linePosition.x - lineJuegePosX, minRevisePosition.x),
                            linePosition.y + maxRevisePosition.y);
                    }
                    else                                        //之后的房间的最小生成范围根据这一层上一个生成的放进进行修正
                    {
                        minPosition = new Vector2(Mathf.Max(beforePosX + roomJudgePosX, minRevisePosition.x),
                            linePosition.y + maxRevisePosition.y);
                    }

                    CreatObject(maxPosition, minPosition, i, 1, roomCounts[i]);
                    SetLine(games[i - 1, 0], games[i, roomCounts[i]]);
                    roomCounts[i]++;
                    beforePosX = games[i, roomCounts[i] - 1].transform.localPosition.x;

                    //保证后续生成的房间不离连接房间太远
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

            int currentFloorMaxRoom = Mathf.Min(roomCounts[i - 1] * 3, maxRoom);        //根据上一层房间个数决定当前层房间最大个数

            int lineCount = 1;
            float isLineBefore;

            //先为上一层第一个房间生成一个连接房间，后续生成房间根据此房间进行位置修正
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

                //下一层最后一个房间进入到当前连接点的可连接范围
                if (beforePosX >= Mathf.Max(linePosX - lineJuegePosX, minRevisePosition.x) &&
                        beforePosX < Mathf.Min(linePosX + lineJuegePosX, maxRevisePosition.x))
                {
                    /*
                     3种情况下必须连接下一层最后一个房间
                        1:下一层最后一个房间X大于当前连接房间X
                        2:当前层已经生成房间数最大
                        3:当前连接房间已经没有剩余位置生成新的房间
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

                //当前连接房间连接数为0时要至少生成一个房间与其连接
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

                //当前连接点生成房间要满足后续未连接点至少可以生成一个房间
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
                    if (isCreatNext >= 0.5f)            //判断是否连接下一个房间
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
                    else                                 //若不连接的话进行下一个连接点的连接   
                    {
                        lineCount = 0;
                        break;
                    }

                    //判断这个连接点是否具备再次连接的条件，若有则再次随机isCreatNext,不满足条件则进行下一个点的连接
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

        //最后一层为怪物房间，且为一个
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

    private float GetAveragePositionY(int floor)                    //获得上一层房间的平均y值
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
        Transform startPos = startObject.transform;             //线条起点
        Transform endPos = endObject.transform;               //线条终点

        GameObject lineObject;

        Vector3 mid = (startPos.position + endPos.position) / 2;

        startObject.GetComponent<MapButton>().levelData.lineRoomNum.Add(endObject.GetComponent<MapButton>().levelData.roomNum);
        startObject.GetComponent<MapButton>().levelData.lineRoomFloorNum.Add(endObject.GetComponent<MapButton>().levelData.roomFloorNum);

        lineObject = objectPool.CreateObject(line.name, line, gameObject, mid, Quaternion.identity);

        //层级置顶
        lineObject.transform.SetAsFirstSibling();

        //长宽
        lineObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Vector3.Distance(startPos.localPosition, endPos.localPosition), lineWidth);

        //旋转
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
