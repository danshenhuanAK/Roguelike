using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWay : MonoBehaviour
{
    private ObjectPool objectPool;

    [SerializeField]
    private GameObject[] gameObjects;           //预制体数组
    public GameObject line;                     //线条
    private GameObject[,] games;                //生成对象数组
    //每个房间所连接的下一层房间
    private Dictionary<GameObject, List<GameObject>> dir = new Dictionary<GameObject, List<GameObject>>();

    [SerializeField]
    private int maxRoom;                        //每层最大间数
    [SerializeField]
    private int maxFloor;                       //最大层数
    private int[] roomCounts;                   //实际每层间数

    [SerializeField]
    private float lineWidth;                    //线条宽度

    private float rawImageHeight;               //背景高度

    public Vector2 maxRevisePosition;           //最大生成范围
    public Vector2 minRevisePosition;           //最小生成范围
    public float lineJuegePosX;                 //连线限制距离
    public float roomJudgePosX;                 //房间之间限制距离

    public GameObject lastClickRoom;            //上次点击房间

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
        float beforeFloorAveragePosY;                       //上一层房间y值的平均值

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
    }
    private float GetAveragePositionY(int floor)                    //获得上一层房间的平均y值
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
        Transform startPos = statrObject.transform;             //线条起点
        Transform endPos = endObject.transform;               //线条终点

        GameObject lineObject;

        Vector3 mid = (startPos.position + endPos.position) / 2;

        if (!dir.ContainsKey(statrObject))
        {
            dir.Add(statrObject, new List<GameObject>());
        }
        dir[statrObject].Add(endObject);

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
}
