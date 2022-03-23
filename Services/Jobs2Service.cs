using Jobs2.Modules;
using Jobs2.interfaces;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace Jobs2.Services
{
    public class Jobs2Service : IJobService
    {
        List<Job> Job2List { get; }
        
     public User CurrentUser { get; set ; }

        private static string fileName = "job.json";

        public Jobs2Service()
        {
            using (var jsonFile = File.OpenText(fileName))
            {
                Job2List = JsonSerializer.Deserialize<List<Job>>(jsonFile.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }

        }

        private void saveToFile()
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize(Job2List));
        }
        //static int AnotherId = 1;

//getting all tasks of that current user
        public List<Job> GetAll() {
           //CurrentUser.Id
            var myTasks = ExploreAllUsersJobs(CurrentUser.Id);

          return   myTasks;
        } 
        //sort of multi functions that doing the samething
        //finding all the tasks that related to that user with this id
            public List<Job> ExploreAllUsersJobs(int id)
        {
            List<Job> found = new List<Job>();
            foreach(var job in Job2List)
            {
                if(job.UserId == id)
                found.Add(job);
            }
            return found;
        }
        public void DeleteMyJobs(int userId)
        {
          var MyJobs =    ExploreAllUsersJobs(userId).ToList();
            foreach(var job in MyJobs)
            {
                Delete(job.Id, userId);
            }
            saveToFile();
            
        }

        public Job Get(int id , int userId) => 
        Job2List.FirstOrDefault(j => j.Id == id && j.UserId==userId);

        public void Add(Job job)
        {
            job.Id = Count+1;
            Job2List.Add(job);
            saveToFile();
        }

        public void Delete(int id,int userId)
        {
            var job = Get(id,userId);
            if (job is null)
                return;

            Job2List.Remove(job);
            saveToFile();
        }

        public void Update(Job job)
        {
            var index = Job2List.FindIndex(j => j.Id == job.Id);
            if (index == -1)
                return;
            job.UserId=CurrentUser.Id;
            Job2List[index] = job;
            saveToFile();
        }



        public int Count => Job2List.Count();
    }
}