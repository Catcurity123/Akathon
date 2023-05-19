
using System;
using System.Linq;
using Cassandra;

public class AstraDBConnection
{
    public Cluster Cluster { get; set; }

    public AstraDBConnection()
    {
        var session = Cluster.Builder()
       .WithCloudSecureConnectionBundle(@"D:\Astra DB\Akathon\secure-connect-akathon.zip")
       .WithCredentials("voJZHtCODdJimBycJkqLuYZS", "5IuH45xuZoTDGv3vNLekQxp0rdKohp2KDQJZPCHQ0TEFpZOjljYD8FnLHNmAMP7QMZYSHRBe7Bgf02viwSmpWTJAI0Y8ENgDW3f.UuyKpPv+8rEeSDqbq,f7m1TTQDRs")
       .Build()
       .Connect();
    }

    public void Close()
    {
        Cluster?.Dispose();
    }
}