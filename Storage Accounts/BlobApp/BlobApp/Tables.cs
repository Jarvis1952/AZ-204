using Azure;
using Azure.Data.Tables;
using System.Net.Http.Headers;

namespace BlobApp
{
    public class Tables
    {
        string ConnectionString = "";
        string tableName = "";
        Tables()
        {
            AddEntity("1", "Mobile", 100);
            AddEntity("2", "Laptop", 500);
            QueryEntity("Mobile");
        }

        public void AddEntity(string orderID, string category, int quantity)
        {
            TableClient tableClient = new TableClient(ConnectionString, tableName);

            // Here Partition key is Category and Row key is OrderID

            TableEntity tableEntity = new TableEntity(category, orderID)
            {
                {"Quantity",quantity}
            };

            tableClient.AddEntity(tableEntity);
            Console.WriteLine("Entity Added");
        }

        public void QueryEntity(string Category)
        {
            TableClient tableClient = new TableClient(ConnectionString, tableName);

            Pageable<TableEntity> results = tableClient.Query<TableEntity>(entity=>entity.PartitionKey == Category);

            foreach(TableEntity tableEntity in results)
            {
                Console.WriteLine("Oder Id {0}", tableEntity.RowKey);
            }
        }

        public void DeleteEntity(string category, string orderID)
        {
            TableClient tableClient = new TableClient(ConnectionString, tableName);
            tableClient.DeleteEntity(category, orderID);
            Console.Write("Entity Deleted");
        }
    }
}
