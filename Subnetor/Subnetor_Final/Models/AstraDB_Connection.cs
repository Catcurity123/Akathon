using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cassandra;

namespace Subnetor.Models
{
    public class AstraDB_Connection
    {
        private static ISession _session;
        public static ISession GetSession()
        {
            if (_session == null)
            {
                _session = Cluster.Builder()
                     .WithCloudSecureConnectionBundle(@"D:\Astra DB\Akathon\secure-connect-akathon.zip")
                     .WithCredentials("voJZHtCODdJimBycJkqLuYZS", "5IuH45xuZoTDGv3vNLekQxp0rdKohp2KDQJZPCHQ0TEFpZOjljYD8FnLHNmAMP7QMZYSHRBe7Bgf02viwSmpWTJAI0Y8ENgDW3f.UuyKpPv+8rEeSDqbq,f7m1TTQDRs")
                     .Build()
                     .Connect("dangviluan");
            }
            return _session;
        }
        public static void CloseSession()
        {
            if (_session != null)
            {
                _session.Dispose();
                _session = null;
            }
        }
    }
}