using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UnityEngine;
using UnityEditor;
using ExcelDataReader;
using Newtonsoft.Json;

public class ImportExcel : EditorWindow
{
    private const string PATH_SAVE_KEY = "artistshow_xlsx_path";
    private const string SAVE_PATH = "Assets/Resources/Tables/";

    [UnityEditor.MenuItem("Local Excel/Set Import Path")]
    static public void SetXlsxPath()
    {
        EditorWindow.GetWindow(typeof(ImportExcel));
    }

    [UnityEditor.MenuItem("Local Excel/Current Access Directory")]
    static public void ShowCurrentFolder()
    {
        Debug.Log($"<color=#1E90FF>Current Access Directory is： {EditorPrefs.GetString(PATH_SAVE_KEY, "Not config")}</color>");
    }

    [UnityEditor.MenuItem("Local Excel/Start Import Excels")]
    static public void Import()
    {
        string path = EditorPrefs.GetString(PATH_SAVE_KEY, null);

        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("Please set the xlsx file first！！！");
            return;
        }

        // 搜索目录下的xlsx文件
        string[] rawFiles = Directory.GetFiles(path);
        List<string> xlsxFiles = (from file in rawFiles where file.EndsWith(".xlsx") || file.EndsWith(".XLSX") select file).ToList();
        Dictionary<string, Type> dictTable2SO = new Dictionary<string, Type>();

        // 获取映射
        string tableMapPath = path + "/TableMap.xlsx";
        if (File.Exists(tableMapPath))
        {
            try
            {
                using (FileStream stream = File.Open(tableMapPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        int count = reader.RowCount;
                        Dictionary<string, int> fieldIndex = new Dictionary<string, int>();

                        for (int i = 0; i < count; i++)
                        {
                            try
                            {
                                reader.Read();

                                if (i == 0)
                                {
                                    for (int j = 0; j < reader.FieldCount; j++)
                                        fieldIndex.Add(reader.GetString(j), j);
                                    continue;
                                }
                                string tableName = reader.GetString(fieldIndex["tableName"]);
                                string scriptObject = reader.GetString(fieldIndex["scriptObject"]);
                                dictTable2SO.Add(tableName, ParseUtil.ParseType(scriptObject));
                            }
                            catch (Exception e)
                            {
                                if (i == 1)
                                    EditorUtility.DisplayDialog("Error", $"Error in table field key", "OK");
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                EditorUtility.DisplayDialog("Error", $"File {tableMapPath} is occupied by other progress，无法访问！", "OK");
            }
        }

        // 解析各表
        int importExcelCount = 0;
        foreach (string xlsxFile in xlsxFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(xlsxFile);
            if (fileName.Equals("TableMap"))
                continue;

            if (ContainChineseChar(fileName))
                continue;

            importExcelCount++;
            EditorUtility.DisplayProgressBar("Importing Excel", $"Import {xlsxFile}", (float)importExcelCount / (float)xlsxFiles.Count);

            try
            {
                using (FileStream stream = File.Open(xlsxFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        int count = reader.RowCount;
                        Dictionary<string, int> fieldIndex = new Dictionary<string, int>();
                        List<Dictionary<string, object>> itemList = new List<Dictionary<string, object>>();

                        for (int i = 0; i < count; i++)
                        {
                            reader.Read();

                            if (i == 0)
                            {
                                for (int j = 0; j < reader.FieldCount; j++)
                                    fieldIndex.Add(reader.GetString(j), j);
                                continue;
                            }

                            Dictionary<string, object> item = new Dictionary<string, object>();
                            bool discardItem = false;
                            foreach (var field in fieldIndex)
                            {
                                Type fieldType = reader.GetFieldType(field.Value);
                                if (fieldType == typeof(int))
                                {
                                    item.Add(field.Key, reader.GetInt32(field.Value));
                                }
                                else if (fieldType == typeof(float))
                                {
                                    float cellFloat = reader.GetFloat(field.Value);

                                    if ((cellFloat - Mathf.FloorToInt(cellFloat)) < float.Epsilon)
                                        item.Add(field.Key, Mathf.FloorToInt(cellFloat));
                                    else
                                        item.Add(field.Key, cellFloat);

                                }
                                else if (fieldType == typeof(double))
                                {
                                    double cellDouble = reader.GetDouble(field.Value);

                                    if (Math.Abs(cellDouble - Convert.ToInt64(cellDouble)) < float.Epsilon)
                                        item.Add(field.Key, Convert.ToInt64(cellDouble));
                                    else
                                        item.Add(field.Key, cellDouble);

                                }
                                else if (fieldType == typeof(string))
                                {
                                    item.Add(field.Key, reader.GetString(field.Value));
                                }
                                else if (fieldType == null)
                                {
                                    item.Add(field.Key, "");
                                }
                                else
                                {
                                    throw new Exception($"Unsupported Data Type：{fieldType.Name}");
                                }

                                if (!discardItem)
                                {
                                    itemList.Add(item);
                                }
                            }
                        }

                        if (dictTable2SO.ContainsKey(fileName))
                        {
                            string json = JsonConvert.SerializeObject(itemList);
                            Type soType = dictTable2SO[fileName];
                            var res = CreateInstance(soType);
                            (res as ISOTable).Json2SO(json);
                            EditorUtility.SetDirty(res);
                            AssetDatabase.CreateAsset(res, SAVE_PATH + res.name + ".asset");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                EditorUtility.DisplayDialog("Read Excel Failed! ", $"{xlsxFile} is occupied by other progress!!!", "OK");
            }
        }

        EditorUtility.ClearProgressBar();
    }

    private string m_selectedPath = "";

    private void OnGUI()
    {
        GUILayout.Label("Set Excel Files Directory", EditorStyles.boldLabel);

        m_selectedPath = GUILayout.TextField(m_selectedPath);

        if (GUILayout.Button("Browse Excel Directory"))
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select Your Excel Files Path";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                m_selectedPath = fbd.SelectedPath;
            }
        }

        GUILayout.Space(30);

        if (GUILayout.Button("Confirm Files Path"))
        {
            // 检查目录（防止手动输入的路径不合法）
            if (System.IO.Directory.Exists(m_selectedPath))
            {
                EditorPrefs.SetString(PATH_SAVE_KEY, m_selectedPath);
                Debug.Log($"<color=#f22f16>Current Access Path is： {m_selectedPath}</color>");
            }
            else
            {
                Debug.LogError("Invalid Directory");
            }
        }
    }


    static private bool ContainChineseChar(string c)
    {
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] >= 0x4e00 && c[i] <= 0x9fbb)
                return true;
        }

        return false;
    }
}
