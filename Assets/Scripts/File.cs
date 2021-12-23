using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class File
{
    //Это класс для записи данных в файлы.
    //Я этот класс всю ночь писал,тут точно ничего не трогай!
    public void Write(string path, string file, string data)
    {
        if (!Directory.Exists(path))
            path = CreatePath(path);


        if (Directory.Exists(path))
            Write(path + file, data);
        else
            Debug.Log("Проблемы с path = " + path + "!");
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
            Debug.Log("Запись не удалась,исключение = " + e);
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
                        Debug.Log("При создании пути = " + nowPath +
                            "\nВозникли проблемы : " + e);
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
                Debug.Log("Файл " + file + " для чтения не найдено.");
        }
        else
            Debug.Log("Путь " + path + " для чтения отсутствует.");

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
            Debug.Log("Чтение не удалось,исключение = " + e);
        }

        return null;
    }
}
