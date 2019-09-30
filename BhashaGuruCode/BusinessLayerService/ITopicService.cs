using System.Collections.Generic;
using System.Threading.Tasks;
using BhashaguruModel;

namespace BusinessLayerService
{
    public interface ITopicService
    {
        Task<TopicDetail> GetTopicDetails();
        Task<List<Card>> GetCardSet(string topicName);
        Task<Topic> AddTopic(Topic topic);
    }
}