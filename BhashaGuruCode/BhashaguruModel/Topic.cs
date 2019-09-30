using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BhashaguruModel
{
    public class Topic : TableEntity
    {
        public Topic()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerEntity"/> class.
        /// Defines the PK and RK.
        /// </summary>
        /// <param name="rowKey">Row key.</param>
        /// <param name="partitionKey">Partition Key.</param>
        public Topic(string rowKey, string partitionKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public string name { get; set; }
        public string imageUrl { get; set; }
        public string id { get; set; }
        public string color { get; set; }
        public string category { get; set; }
        public string language { get; set; }
        public string description { get; set; }
        public DateTime createdDateTime { get; set; }
        public DateTime modifiedDateTime { get; set; }
        //public List<Card> CardSet { get; set; }// add to user mapping table
        //public bool courseCompletedStatus { get; set; }// add to user mapping table
    }

    public class TopicDetail
    {
        public List<Topic> topic { get; set; }
    }

    public class Card :TableEntity
    {
        public Card()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerEntity"/> class.
        /// Defines the PK and RK.
        /// </summary>
        /// <param name="rowKey">Row key.</param>
        /// <param name="partitionKey">Partition Key.</param>
        public Card(string rowKey, string partitionKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public string cardName { get; set; }
        public string TopicName { get; set; }
        public string word { get; set; }
        public string translation { get; set; }
        public string audioUrl { get; set; }
        public string id { get; set; }
        [IgnoreDataMember]
        public DateTime createdDateTime { get; set; }
        [IgnoreDataMember]
        public DateTime modifiedDateTime { get; set; }
    }

    public class UserTopic
    {

    }

}
