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
    private const string PATH_SAVE_KEY = "xlsx_path";
    private const string SAVE_PATH = "Assets/Resources/Tables/";

    // 在目标文件夹里但又不需要读的表
    private static readonly string[] DIRTY_LIST = new string[] { "achievement", "ConstEditor", "anniversary", "change_player", "dialogue", "facefight_immunity", "mail", "message", "StringUI" };


    [UnityEditor.MenuItem("Excel本地表/设置导入路径")]
    static public void SetXlsxPath()
    {
        EditorWindow.GetWindow(typeof(ImportExcel));
    }

    [UnityEditor.MenuItem("Excel本地表/当前读取目录")]
    static public void ShowCurrentFolder()
    {
        Debug.Log($"<color=#f22f16>当前读取目录为： {EditorPrefs.GetString(PATH_SAVE_KEY, "尚未设置")}</color>");
    }

    [UnityEditor.MenuItem("Excel本地表/开始导入")]
    static public void Import()
    {
        string path = EditorPrefs.GetString(PATH_SAVE_KEY, null);

        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("先设置xlsx文件的路径！！！");
            return;
        }

        // 搜索目录下的xlsx文件
        string[] rawFiles = Directory.GetFiles(path);
        List<string> xlsxFiles = (from file in rawFiles where file.EndsWith(".xlsx") || file.EndsWith(".XLSX") select file).ToList();

        DateTime startTime = DateTime.Now;
        string tableReflectPath = "/TableReflect.xlsx";
        if (File.Exists(tableReflectPath))
        {
            try
            {
                using (FileStream stream = File.Open(tableReflectPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
                                    continue;
                                if (i == 1)
                                {
                                    for (int j = 0; j < reader.FieldCount; j++)
                                        fieldIndex.Add(reader.GetString(j), j);
                                    continue;
                                }
                            }
                            catch (Exception e)
                            {
                                if (i == 1)
                                    EditorUtility.DisplayDialog("错误", $"表头错误", "确定");
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                EditorUtility.DisplayDialog("错误", $"文件{tableReflectPath}被其他进程占用，无法访问！", "确定");
            }
        }       
    }

    private string m_selectedPath = "";

    private void OnGUI()
    {
        GUILayout.Label("设置Excel文件所在目录", EditorStyles.boldLabel);

        m_selectedPath = GUILayout.TextField(m_selectedPath);

        if (GUILayout.Button("选择Excel所在的文件夹"))
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "选择Excel所在目录";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                m_selectedPath = fbd.SelectedPath;
            }
        }

        GUILayout.Space(30);

        if (GUILayout.Button("设置目录"))
        {
            // 检查目录（防止手动输入的路径不合法）
            if (System.IO.Directory.Exists(m_selectedPath))
            {
                EditorPrefs.SetString(PATH_SAVE_KEY, m_selectedPath);
                Debug.Log($"<color=#f22f16>当前读取目录已设置为： {m_selectedPath}</color>");
            }
            else
            {
                Debug.LogError("输入的目录不合法");
            }
        }
    }


    static private bool containChineseChar(string c)
    {
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] >= 0x4e00 && c[i] <= 0x9fbb)
                return true;
        }

        return false;
    }
}
