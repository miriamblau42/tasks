using Jobs2.Modules;
using System.Collections.Generic;

namespace Jobs2.interfaces
{
    public interface IJobService
    {
        List<Job> GetAll();

        Job Get(int id,int userId);

        void Add(Job job);

        void Delete(int id, int userId);

        void Update(Job job);
        void DeleteMyJobs(int userId);

        int Count { get; }
        User CurrentUser { get; set; }
    }
}