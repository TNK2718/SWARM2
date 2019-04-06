using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public abstract class DataLoaderBase
{
    // csvの読み込み
    public void ReadCSV(string path, ref string[,] _data) {
        StreamReader streamReader = new StreamReader(Application.dataPath + path);
        string strStream = streamReader.ReadToEnd();
        Debug.Log(strStream);
        System.StringSplitOptions option = System.StringSplitOptions.None;

        string[] rows = strStream.Split(new char[] { '\n' }, option);
        char[] spliter = new char[1] { ',' };
        int height = rows.Length;
        int width = rows[0].Split(spliter, option).Length;
        Debug.Log(height);
        Debug.Log(width);

        _data = new string[height, width];
        for(int i = 0; i < height; i++) {
            string[] stringRow = rows[i].Split(spliter, option);
            foreach (string tmp in stringRow) Debug.Log(tmp);
            for(int j = 0; j < width; j++) {
                _data[i, j] = stringRow[j];
            }
        }
    }
}
