using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Tests
{
  [TestClass]
  public class CategoryTest: IDisposable
  {
    public void Dispose()
    {
      Category.ClearAll();
      Item.ClearAll();
    }

    public CategoryTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todolist_tests;";
    }
    
    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Category()
    {
      Category firstCategory = new Category("Household chores");
      Category secondCategory = new Category("Household chores");

      Assert.AreEqual(firstCategory, secondCategory);
    }

    [TestMethod]
    public void GetAll_ReturnsAllCategoryObjects_CategoryList()
    {
      string name01 = "Work";
      string name02 = "School";
      Category newCategory1 = new Category(name01);
      newCategory1.Save();
      Category newCategory2 = new Category(name02);
      newCategory2.Save();
      List<Category> newList = new List<Category> { newCategory1, newCategory2 };

      List<Category> result = Category.GetAll();

      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void CategoryConstructor_CreatesInstanceOfCategory_Category()
    {
      Category newCategory = new Category("test category");
      Assert.AreEqual(typeof(Category), newCategory.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      string name = "Test Category";

      string result = newCategory.Name;

      Assert.AreEqual(name, result);
    }
    [TestMethod]
    public void Save_SavesCategoryToDatabase_CategoryList()
    {
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      List<Category> result = Category.GetAll();
      List<Category> testList = new List<Category>{testCategory};

      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Save_DatabaseAssignsIdToCategory_Id()
    {
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      Category savedCategory = Category.GetAll()[0];

      int result = savedCategory.Id;
      int testId = testCategory.Id;

      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_ReturnsCategoryInDatabase_Category()
    {
      Category testCategory = new Category("Household chores");
      testCategory.Save();

      Category foundCategory = Category.Find(testCategory.Id);

      Assert.AreEqual(testCategory, foundCategory);
    }

    [TestMethod]
    public void DeleteCategory_DeletesCategoryAssociationsFromDatabase_CategoryList()
    {
      Item testItem = new Item("Mow the lawn");
      testItem.Save();
      string testName = "Home stuff";
      Category testCategory = new Category(testName);
      testCategory.Save();

      testCategory.AddItem(testItem);
      testCategory.DeleteCategory();
      List<Category> resultItemCategories = testItem.GetCategories();
      List<Category> testItemCategories = new List<Category> {};

      CollectionAssert.AreEqual(testItemCategories, resultItemCategories);
    }
    [TestMethod]
    public void Test_AddItem_AddsItemToCategory()
    {
      Category testCategory = new Category("Household chores");
      testCategory.Save();
      Item testItem = new Item("Mow the lawn");
      testItem.Save();
      Item testItem2 = new Item("Water the garden");
      testItem2.Save();

      testCategory.AddItem(testItem);
      testCategory.AddItem(testItem2);
      List<Item> result = testCategory.GetItems();
      List<Item> testList = new List<Item>{testItem, testItem2};

      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetItems_RetrievesAllItemsWithCategory_ItemList()
    {
    Category testCategory = new Category("Household chores");
    testCategory.Save();
    Item testItem1 = new Item("Mow the lawn");
    testItem1.Save();
    Item testItem2 = new Item("Buy plane ticket");
    testItem2.Save();

    testCategory.AddItem(testItem1);
    List<Item> savedItems = testCategory.GetItems();
    List<Item> testList = new List<Item> {testItem1};

    CollectionAssert.AreEqual(testList, savedItems);
    }
  }
}
