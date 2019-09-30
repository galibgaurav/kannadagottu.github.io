using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Runtime.Serialization;

namespace BhashaguruModel
{
    public class RegisterUser 
    {
        public string emailId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class LoginUser
    {
        public string emailId { get; set; }       
        public string password { get; set; }
        public string userLangugage { get; set; }
    }
    public class User : TableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerEntity"/> class.
        /// Your entity type must expose a parameter-less constructor
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerEntity"/> class.
        /// Defines the PK and RK.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <param name="firstName">The first name.</param>
        public User(string rowKey,string partitionKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey ;
        }

        public string loginId { get; set; }
        public string emailId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        [IgnoreDataMember]
        public string password { get; set; }
        public bool isActive { get; set; }
        [IgnoreDataMember]
        public DateTime createdDateTime { get; set; }
        [IgnoreDataMember]
        public DateTime modifiedDateTime { get; set; }
        public string photoUrl { get; set; }
        public string userLanguage { get; set; }
        public bool isPasswordResetRequired { get; set; }
        public string authToken { get; set; }
    }

}
