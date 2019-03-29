using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public abstract class DataLoaderBase
{
    public void ReadCSV(string path, ref string[,] _data) {
        StreamReader streamReader = new StreamReader(path);
        string strStream = streamReader.ReadToEnd();
        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;

        string[] rows = strStream.Split(new char[] { '\r', '\n' }, option);
        char[] spliter = new char[1] { ',' };
        int height = rows.Length;
        int width = rows[0].Split(spliter, option).Length;

        _data = new string[height, width];
        for(int i = 0; i < height; i++) {
            string[] stringRow = rows[i].Split(spliter, option);
            for(int j = 0; j < width; j++) {
                _data[i, j] = stringRow[j];
            }
        }
    }
}
