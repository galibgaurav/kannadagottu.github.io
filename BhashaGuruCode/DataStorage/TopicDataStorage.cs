using BhashaguruModel;
using HelperComponent;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public class TopicDataStorage : ITopicDataStorage
    {
        IConfiguration _iconfiguration;

        public TopicDataStorage(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        /// <summary>
        /// Demonstrate the most efficient storage query - the point query - where both partition key and row key are specified.
        /// </summary>
        /// <param name="table">Sample table name</param>
        /// <param name="partitionKey">Partition key </param>
        /// <param name="rowKey">Row key </param>
        /// <returns>A Task object</returns>
        public async Task<TopicDetail> GetAllTopics()
        {
            try
            {
                common cmn = new common(_iconfiguration);
                // Create or reference an existing table
                CloudTable table = await cmn.CreateTableAsync("topic");
                TopicDetail topicDetail = new TopicDetail();

                TableQuery<Topic> tableQuery = new TableQuery<Topic>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "BhashaGuru"));

                //TableQuery<Topic> employeeQuery = new TableQuery<Topic>().Where(
                //TableQuery.CombineFilters(
                //TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                //TableOperators.And,
                //TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThan, rowKey))); 

                //retrives topic from the db
                var topics = await DataStorageUtils.ExecuteQueryAsync<Topic>(table, tableQuery);
                topicDetail.topic = topics;
                return topicDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Demonstrate the most efficient storage query - the point query - where both partition key and row key are specified.
        /// </summary>
        /// <param name="table">Sample table name</param>
        /// <param name="partitionKey">Partition key </param>
        /// <param name="rowKey">Row key </param>
        /// <returns>A Task object</returns>
        public async Task<Topic> AddTopic(Topic topic)
        {
            try
            {
                string tableName = "topic";
                common cmn = new common(_iconfiguration);
                // Create or reference an existing table
                CloudTable table = await cmn.CreateTableAsync(tableName);

                var exitingTopic = await DataStorageUtils.RetrieveEntityUsingPointQueryAsync(table, "BhashaGuru", topic.name);
                if (exitingTopic != null)
                {
                    return null;
                }
                else
                {
                    
                    Topic topicToAdd = new Topic(topic.name, "BhashaGuru");
                    topicToAdd.category = topic.category;
                    topicToAdd.color = topic.color;
                    topicToAdd.createdDateTime = DateTime.UtcNow;
                    topicToAdd.description = topic.description;
                    topicToAdd.imageUrl = topic.imageUrl;
                    topicToAdd.language = topic.language;
                    topicToAdd.modifiedDateTime = DateTime.UtcNow;
                    topicToAdd.name = topic.name;
                    var newtopic = await DataStorageUtils.InsertOrMergeEntityAsync<Topic>(table, topicToAdd);
                    return newtopic;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
