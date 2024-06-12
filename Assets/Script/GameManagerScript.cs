using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManagerScript : MonoBehaviour
{

    enum Stage
    {
        Space,
        Block,
        Coin,
        Goal,
    }

    private int[,] map;

    public GameObject block;

    public GameObject goal;

    public GameObject coin;

    public TextAsset stageCSV;

    public TextMeshProUGUI scoreText;

    public static int score = 0;

    private List<List<string>> data = new List<List<string>>();

    public GameObject goalParticle;

    public GameObject BG;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1980, 1080, false);

        score = 0;

        Vector3 position = Vector3.zero;
        Vector3 BGPosition =  new Vector3(0.0f,0.0f,3.0f);
        LoadCSV();
        int lenY = map.GetLength(0);
        int lenX = map.GetLength(1);
        for (int x = 0; x < lenX; x++)
        {
            position.x = x;
            BGPosition.x = x;
            for (int y = 0; y < lenY; y++)
            {
                position.y = -y + 5;
                BGPosition.y = -y + 5;
                if (map[y, x] == (int)Stage.Block)
                {
                    Instantiate(block, position, Quaternion.identity);
                }else if (map[y, x] == (int)Stage.Goal)
                {
                    goal.transform.position = position;
                    goalParticle.transform.position = position;
                }else if (map[y, x] == (int)Stage.Coin)
                {
                    Quaternion prefabRotation = coin.transform.rotation;
                    Instantiate(coin, position, prefabRotation);
                }

                Instantiate(BG, BGPosition, Quaternion.identity);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(GoalScript.isGameClear == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene("Title");
            }
        }

        scoreText.text = "SCORE" + score;
    }


    void LoadCSV()
    {
        if (stageCSV != null)
        {
            // CSVファイルのテキストを取得
            string fileText = stageCSV.text;

            // 改行で分割して配列に格納
            string[] lines = fileText.Split('\n');

            // 行数と列数を取得
            int rowCount = lines.Length;
            int colCount = lines[0].Split(',').Length;

            // マップ配列の初期化
            map = new int[rowCount, colCount];

            // 各行について処理
            for (int i = 0; i < rowCount; i++)
            {
                // カンマで分割してデータを取得
                string[] fields = lines[i].Split(',');
                for (int j = 0; j < colCount; j++)
                {
                    int value;
                    // 文字列を整数に変換してマップ配列に格納
                    if (int.TryParse(fields[j], out value))
                    {
                        map[i, j] = value;
                    }
                    else
                    {
                        Debug.LogError("CSVファイル内に不正なデータが含まれています！");
                    }
                }
            }
        }
        else
        {
            Debug.LogError("CSVファイルが指定されていません！");
        }
    }
}
