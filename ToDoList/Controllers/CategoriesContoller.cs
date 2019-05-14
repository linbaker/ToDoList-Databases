using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
  public class CategoriesController : Controller
  {
    [HttpGet("/categories")]
    public ActionResult Index()
    {
      List<Category> allCategories = Category.GetAll();
      return View(allCategories);
    }

    [HttpGet("/categories/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/categories")]
    public ActionResult Create(string categoryName)
    {
      Category newCategory = new Category(categoryName);
      newCategory.Save();
      List<Category> allCategories = Category.GetAll();
      return View("Index", allCategories);
    }

    [HttpGet("/categories/{categoryId}")]
    public ActionResult Show(int categoryId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Category selectedCategory = Category.Find(categoryId);
      List<Item> categoryItems = selectedCategory.GetItems();
      List<Item> allItems = Item.GetAll();
      model.Add("category", selectedCategory);
      model.Add("categoryItems", categoryItems);
      model.Add("allItems", allItems);
      return View(model);
    }

    [HttpPost("/categories/{categoryId}/items/new")]
    public ActionResult AddItem(int categoryId, int itemId)
    {
      Category category = Category.Find(categoryId);
      Item item = Item.Find(itemId);
      category.AddItem(item);
      return RedirectToAction("Show",  new { id = categoryId });
    }

    [HttpPost("/categories/{categoryId}/delete")]
    public ActionResult Delete(int categoryId)
    {
      Category category = Category.Find(categoryId);
      category.DeleteCategory();
      return Redirect("/");
    }


    [HttpGet("/categories/{categoryId}/edit")]
    public ActionResult Edit(int categoryId)
    {
      Category category = Category.Find(categoryId);
      return View(category);
    }

    [HttpPost("/categories/{categoryId}")]
    public ActionResult Update(int categoryId, string newName)
    {
      Category category = Category.Find(categoryId);
      category.Edit(newName);
      return RedirectToAction("Show", categoryId);
    }
    // [HttpGet("/categories/{id}")]
    // public ActionResult Show(int id)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Category selectedCategory = Category.Find(id);
    //   List<Item> categoryItems = selectedCategory.GetItems();
    //   model.Add("category", selectedCategory);
    //   model.Add("categoryItems", categoryItems);
    //   return View(selectedCategory);
    // }
    //
    // [HttpPost("/categories/{categoryId}/items")]
    // public ActionResult Create(int categoryId, string itemDescription)
    // {
    //   Category foundCategory = Category.Find(categoryId);
    //   Item newItem = new Item(itemDescription, categoryId);
    //   newItem.Save();
    //   foundCategory.GetItems();
    //   // List<Item> categoryItems = foundCategory.GetItems();
    //   // model.Add("items", categoryItems);
    //   // model.Add("category", foundCategory);
    //   return View("Show", foundCategory);
    // }

    //
    // [HttpPost("/categories")]
    // public ActionResult Create(string categoryName)
    // {
    //   Category newCategory = new Category(categoryName);
    //   List<Category> allCategories = Category.GetAll();
    //   newCategory.Save();
    //   return RedirectToAction("Index");
    // }
    //
    //
    // [HttpPost("/categories/{categoryId}/items")]
    // public ActionResult Create(int categoryId, string itemDescription)
    // {
    //   Category foundCategory = Category.Find(categoryId);
    //   Item newItem = new Item(itemDescription, categoryId);
    //   newItem.Save();
    //   foundCategory.GetItems();
    //   // List<Item> categoryItems = foundCategory.GetItems();
    //   // model.Add("items", categoryItems);
    //   // model.Add("category", foundCategory);
    //   return View("Show", foundCategory);
    // }
    //
    // [HttpPost("/categories/{categoryId}/delete")]
    // public ActionResult Delete(int categoryId)
    // {
    //   Category category = Category.Find(categoryId);
    //   category.DeleteCategory();
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   model.Add("category", category);
    //   return RedirectToAction("Index");
    // }
    //
  }
}
