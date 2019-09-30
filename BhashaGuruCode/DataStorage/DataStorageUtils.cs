using BhashaguruModel;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public class DataStorageUtils
    {
        /// <summary>
        /// Demonstrate the most efficient storage query - the point query - where both partition key and row key are specified.
        /// </summary>
        /// <param name="table">Sample table name</param>
        /// <param name="partitionKey">Partition key - i.e., last name</param>
        /// <param name="rowKey">Row key - i.e., first name</param>
        /// <returns>A Task object</returns>
        public static async Task<User> RetrieveEntityUsingPointQueryAsync(CloudTable table, string partitionKey, string rowKey)
        {
            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<User>(partitionKey, rowKey);
                TableResult result = await table.ExecuteAsync(retrieveOperation);
                User user = result.Result as User;
              
                return user;
            }
            catch (StorageException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// The Table Service supports two main types of insert operations.
        ///  1. Insert - insert a new entity. If an entity already exists with the same PK + RK an exception will be thrown.
        ///  2. Replace - replace an existing entity. Replace an existing entity with a new entity.
        ///  3. Insert or Replace - insert the entity if the entity does not exist, or if the entity exists, replace the existing one.
        ///  4. Insert or Merge - insert the entity if the entity does not exist or, if the entity exists, merges the provided entity properties with the already existing ones.
        /// </summary>
        /// <param name="table">The sample table name</param>
        /// <param name="entity">The entity to insert or merge</param>
        /// <returns>A Task object</returns>
        public static async Task<User> InsertOrMergeEntityAsync(CloudTable table, User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("InsertOrMergeEntityAsync:User");
            }

            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                User insertedCustomer = result.Result as User;

                return insertedCustomer;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        /// <summary>
        /// The Table Service supports two main types of insert operations.
        ///  1. Insert - insert a new entity. If an entity already exists with the same PK + RK an exception will be thrown.
        ///  2. Replace - replace an existing entity. Replace an existing entity with a new entity.
        ///  3. Insert or Replace - insert the entity if the entity does not exist, or if the entity exists, replace the existing one.
        ///  4. Insert or Merge - insert the entity if the entity does not exist or, if the entity exists, merges the provided entity properties with the already existing ones.
        /// </summary>
        /// <param name="table">The sample table name</param>
        /// <param name="entity">The entity to insert or merge</param>
        /// <returns>A Task object</returns>
        public static async Task<T> InsertOrMergeEntityAsync<T>(CloudTable table, T entity) where T:ITableEntity
        {
            if (entity == null)
            {
                throw new ArgumentNullException("InsertOrMergeEntityAsync:User");
            }

            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);
                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                var createOrUpdatedEntity = (T)result.Result;
                return createOrUpdatedEntity;
            }
            catch (StorageException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="table">Sample table name</param>
        /// <param name="deleteEntity">Entity to delete</param>
        /// <returns>A Task object</returns>
        //public static async Task DeleteEntityAsync(CloudTable table, CustomerEntity deleteEntity)
        //{
        //    try
        //    {
        //        if (deleteEntity == null)
        //        {
        //            throw new ArgumentNullException("deleteEntity");
        //        }

        //        TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
        //        await table.ExecuteAsync(deleteOperation);
        //    }
        //    catch (StorageException e)
        //    {
        //        Console.WriteLine(e.Message);
        //        Console.ReadLine();
        //        throw;
        //    }
        //}

        /// <summary>
        /// Retrive all row of a particular azure table.
        /// </summary>
        /// <param name="table">Topic</param>
        /// <returns>ListOfEntities</returns>
        public static async Task<List<T>> RetrieveEntitiesAsync<T>(CloudTable table) where T : ITableEntity, new ()
        {
            try
            {
                List<Topic> topics = new List<Topic>();
                TableQuery<T> tableQuery = new TableQuery<T>();
                TableContinuationToken continuationToken = null;
                do
                {
                    var page = await table.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);
                    continuationToken = page.ContinuationToken;
                    //topics.Add();
                } while (continuationToken != null);

                return topics;
            }
            catch (StorageException e)
            {
                throw e;
            }
        }

    }
}
