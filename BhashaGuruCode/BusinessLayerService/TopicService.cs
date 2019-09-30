using BhashaguruModel;
using DataStorage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerService
{
    public class TopicService : ITopicService
    {
        IConfiguration _iconfiguration;
        
        public TopicService(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public Task<List<Card>> GetCardSet(string topic)
        {
            throw new NotImplementedException();
        }

        public async Task<TopicDetail> GetTopicDetails()
        {
            TopicDataStorage topicDataStorage = new TopicDataStorage(_iconfiguration);
            try
            {
                var result = await topicDataStorage.GetAllTopics();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Topic> AddTopic(Topic topic)
        {
            TopicDataStorage topicDataStorage = new TopicDataStorage(_iconfiguration);
            try
            {
                var result = await topicDataStorage.AddTopic(topic);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
