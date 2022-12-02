using System.Collections;
using System.Collections.Generic;
using static Book;
public class DataBook
{
    public int id { get; set; }
    public string title { get; set; }

    public string author { get; set; }

    public string genre { get; set; }

    public string cover { get; set; }

}

public class RecData
{
    public string[] genres { get; set; }
    public int userId { get; set; }
}

public class loginInfo
{
    public string username { get; set; }
    public string password { get; set; }
}

public class idResponce
{
    public int id { get; set; }
}
