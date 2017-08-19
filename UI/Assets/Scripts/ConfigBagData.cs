using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using LitJson;

/// <summary>
/// 定义要读取的data类
/// </summary>
public class configData
{
    public readonly string id;
    public readonly string name;
    public readonly string source;
    public int count; 
}

public class ConfigBagData : MonoBehaviour
{
    public static ConfigBagData Instance;

    public Dictionary<string, configData> bagConfig;

	public void Awake ()
    {
        Instance = this;
	}
	
	public void Init ()
    {
        bagConfig = Load<configData>();
        foreach(var pair in bagConfig)
        {
            Debug.Log("Key:" + pair.Key + " Value:" + pair.Value.id + "|" + pair.Value.name + "|" + pair.Value.source + "|" + pair.Value.count);
        }
        RefreshExportJson();
        //Load("configData"); //另一种读取方式
    }

    /// <summary>
    /// 通过建立Dict读取Json数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private Dictionary<string,T> Load<T>() where T: class
    {
        string sheetName = typeof(T).Name;

        string readFilePath = Application.persistentDataPath + "/" + sheetName + ".txt";

        string str;

        //如果存档已存在则直接读取存档，否则读取resources中的txt文件
        if (File.Exists(readFilePath))
        {
            Debug.Log("Get file");
            StreamReader textData = File.OpenText(readFilePath);
            str = textData.ReadToEnd();
            textData.Close();
            textData.Dispose();
        }
        else
        {
            TextAsset textAsset = Resources.Load<TextAsset>("Data/" + sheetName);

            if (textAsset == null)
            {
                Debug.LogError(sheetName + " not found");
                return null;
            }
            str = textAsset.text;
        }

        Dictionary<string, T> data = JsonMapper.ToObject<Dictionary<string, T>>(str);

        return data;
    }

    /// <summary>
    /// 另一种读取Json数据的方式
    /// </summary>
    /// <param name="textName"></param>
    public void Load(string textName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Data/" + textName);

        if (textAsset == null)
        {
            Debug.LogError(textName + " not found");
        }

        JsonData data = JsonMapper.ToObject(textAsset.text);

        Debug.Log(data["001"]["name"] + "|" + data["001"]["source"]);
    }

    /// <summary>
    /// 导出Json数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    public void ExportToJson<T>(Dictionary<string,T> sheetData) where T: class
    {
        string name = typeof(T).Name;

        string outFilePath = Application.persistentDataPath + "/" + name + ".txt";
        Debug.Log(outFilePath);

        string jsonText = JsonMapper.ToJson(sheetData);

        FileStream fs = new FileStream(outFilePath, FileMode.Create);

        //获取字节数组
        byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonText);
        //写入数据
        fs.Write(data, 0, data.Length);
        //清空缓冲区，关闭流
        fs.Flush();
        fs.Close();
    }

    public void RefreshExportJson()
    {
        Instance.ExportToJson<configData>(bagConfig);
    }
}
