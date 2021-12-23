using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class File
{
    //��� ����� ��� ������ ������ � �����.
    //� ���� ����� ��� ���� �����,��� ����� ������ �� ������!
    public void Write(string path, string file, string data)
    {
        if (!Directory.Exists(path))
            path = CreatePath(path);


        if (Directory.Exists(path))
            Write(path + file, data);
        else
            Debug.Log("�������� � path = " + path + "!");
    }

    public void Write(string path, string data)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path))
                sw.Write(data);
        }
        catch (System.Exception e)
        {
            Debug.Log("������ �� �������,���������� = " + e);
        }
    }

    private string CreatePath(string path)
    {
        if (path.Contains("/"))
        {
            List<string> folders = new List<string>(path.Split('/'));
            string nowPath = null;
            foreach (string tmp in folders)
            {
                nowPath += tmp + "/";
                if (!Directory.Exists(nowPath))
                    try
                    {
                        Directory.CreateDirectory(nowPath);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log("��� �������� ���� = " + nowPath +
                            "\n�������� �������� : " + e);
                    }
            }

            return nowPath;
        }

        return null;
    }

    public string Read(string path, string file)
    {
        if (Directory.Exists(path))
        {
            if (System.IO.File.Exists(path + file))
                return Read(path + file);
            else
                Debug.Log("���� " + file + " ��� ������ �� �������.");
        }
        else
            Debug.Log("���� " + path + " ��� ������ �����������.");

        return null;
    }

    public string Read(string path)
    {
        try
        {
            using (StreamReader sr = new StreamReader(path))
                return sr.ReadLine();
        }
        catch (System.Exception e)
        {
            Debug.Log("������ �� �������,���������� = " + e);
        }

        return null;
    }
}
