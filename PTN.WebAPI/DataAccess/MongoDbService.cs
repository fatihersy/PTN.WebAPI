using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PTN.WebAPI.Models;

namespace PTN.WebAPI.DataAccess
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Tasks> _tasksCollection;

        public MongoDbService(IOptions<MongoDbSettings> mondoDbSettings)
        {
            MongoClient client = new MongoClient(mondoDbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mondoDbSettings.Value.DatabaseName);
            _tasksCollection = database.GetCollection<Tasks>(mondoDbSettings.Value.CollectionName);
        }


        public List<Tasks> getTasks()
        {
            return _tasksCollection.Find(t => t.Name != "").ToList();
        }

        public void AddTask(Tasks task)
        {
            _tasksCollection.InsertOne(task);
        }
        public void UpdateTask(Tasks task)
        {
            _tasksCollection.FindOneAndReplace(t => t.Id == task.Id, task);
        }
        public void DeleteTask(Tasks task)
        {
            _tasksCollection.DeleteOne(t => t.Id == task.Id);
        }
    }
}
