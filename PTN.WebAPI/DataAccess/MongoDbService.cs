using MongoDB.Driver;
using PTN.WebAPI.Models;

namespace PTN.WebAPI.DataAccess
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Tasks> _tasksCollection;

        private string connectionString = "mongodb+srv://fatihersy:fatih.ersy97@ptn-cluster.y3fywte.mongodb.net/?retryWrites=true&w=majority";
        private string collectionName = "tasks";
        private string databaseName = "PTN-Database";

        public MongoDbService()
        {
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            _tasksCollection = database.GetCollection<Tasks>(collectionName);
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
