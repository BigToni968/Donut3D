using System.IO;
using UnityEngine;

public class ResourcesExtension
{
    public static Sprite Load(string resourceName)
    {
        //Массив папок в папок Resources
        string[] directories = Directory.GetDirectories(Application.dataPath + "/Resources", "*", SearchOption.AllDirectories);

        //Проходимся по всем папкам и ищем спрайт.
        foreach (var item in directories)
        {
            string itemPath = item.Substring((Application.dataPath + "/Resources").Length + 1);
            Sprite result = Resources.Load(itemPath + "\\" + resourceName,typeof(Sprite)) as Sprite;
            
            if (result != null)
                return result;

        }

        return null;
    }
}
