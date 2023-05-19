using System;
using System.Collections.Generic;
using Cassandra;

namespace Demo1.Models
{
    public class AstraDBModel
    {
        private AstraDBConnection _db;

        public AstraDBModel()
        {
            _db = new AstraDBConnection();
        }

        public List<MyData> GetData()
        {
            var session = _db.Cluster.Connect("dangviluan");
            var query = "SELECT * FROM users";
            var resultSet = session.Execute(query);
            var result = new List<MyData>();
            foreach (var row in resultSet)
            {
                var data = new MyData
                {
                    Id = row.GetValue<int>("id"),
                    Email = row.GetValue<HashSet<string>>("email"),
                    FirstName = row.GetValue<string>("fname"),
                    LastName = row.GetValue<string>("lname")
                };
                result.Add(data);
            }
            return result;
        }

        public void Close()
        {
            _db?.Close();
        }

        public class MyData
        {
            public int Id { get; set; }
            public HashSet<string> Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}