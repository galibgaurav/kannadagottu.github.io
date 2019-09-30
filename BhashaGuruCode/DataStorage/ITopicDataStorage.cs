using System.Threading.Tasks;
using BhashaguruModel;

namespace DataStorage
{
    public interface ITopicDataStorage
    {
        Task<TopicDetail> GetAllTopics();
        Task<Topic> AddTopic(Topic topic);
    }
}