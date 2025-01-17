using System.Collections.Generic;

[System.Serializable]
public class Category
{
    public int id;
    public string name;
}

[System.Serializable]
public class CategoryList
{
    public List<Category> categories;
}