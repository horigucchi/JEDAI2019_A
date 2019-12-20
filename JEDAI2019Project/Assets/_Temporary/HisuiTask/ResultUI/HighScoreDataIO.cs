using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// ハイスコアのデータのインプットとアウトプットを行うクラス
public class HighScoreDataIO : Horiguchi.Engine.Singleton<HighScoreDataIO>
{
    // ハイスコアのセーブ
    // filepath:ファイルへのパス
    // Score:書き込むスコア
    public void HighScoreSave(string filepath, int Score)
    {
        StreamWriter writer = new StreamWriter(Application.dataPath + filepath, false);
        writer.Write(Score);
        writer.Flush();
        writer.Close();
    }

    // ハイスコアのロード
    // filepath:ファイルへのパス
    public int HighScoreLoad(string filepath)
    {
        // ファイルがあるかどうかのチェック
        if (!File.Exists(Application.dataPath+filepath))
        {
            return 0;
        }

        // ファイルを最後まで読み込む
        string strStream = "";        
        using (StreamReader sr = new StreamReader(Application.dataPath + filepath))
        {
            strStream = sr.ReadToEnd();
            sr.Close();
        }

        // String->intの変換
        int val = 0;
        for (int i = 0;i < strStream.Length;++i)
        {
            val *= 10;
            val += (strStream[i] -'0');
        }

        return val;
    }
}
