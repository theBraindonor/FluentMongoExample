using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MongoDB.Bson;

public class Record
{
    public ObjectId _id { get; set; }
    public string SomeRandomText { get; set; }
    public DateTime RecordDate { get; set; }
}

public class ObjectIdBinder : IModelBinder
{
    public object BindModel(ControllerContext controller_context, ModelBindingContext binding_context)
    {
        ValueProviderResult result = binding_context.ValueProvider.GetValue(binding_context.ModelName);
        return new ObjectId(result.AttemptedValue);
    }
}