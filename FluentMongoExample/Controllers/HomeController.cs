using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using FluentMongo.Linq;

public class HomeController : Controller
{
    public ActionResult Index()
    {
        MongoDatabase database = MongoDatabase.Create("mongodb://localhost/FluentMongoExample");
        MongoCollection<Record> collection = database.GetCollection<Record>("Record");

        IQueryable<Record> records = from r in collection.AsQueryable() select r;

        return View(records);
    }

    public ActionResult CreateRecord(string SomeRandomText)
    {
        MongoDatabase database = MongoDatabase.Create("mongodb://localhost/FluentMongoExample");
        MongoCollection<Record> collection = database.GetCollection<Record>("Record");

        Record record = new Record();

        record.SomeRandomText = SomeRandomText;
        record.RecordDate = DateTime.Now;

        collection.Save(record);

        return RedirectToAction("Index");
    }

    public ActionResult DeleteRecord(ObjectId record_id)
    {
        MongoDatabase database = MongoDatabase.Create("mongodb://localhost/FluentMongoExample");
        MongoCollection<Record> collection = database.GetCollection<Record>("Record");

        Record record = (from r in collection.AsQueryable() where r._id == record_id select r).FirstOrDefault();

        if (record != null)
        {
            collection.Remove(Query.EQ("_id", record._id));
        }

        return RedirectToAction("Index");
    }
}

