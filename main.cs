using System;
using System.Net;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting HTTP listener on port 8002...");
        string[] s3_facts = new string[]
        {
            "Scale storage resources to meet fluctuating needs with 99.999999999% (11 9s) of data durability.",
            "Store data across Amazon S3 storage classes to reduce costs without upfront investment or hardware refresh cycles.",
            "Protect your data with unmatched security, compliance, and audit capabilities.",
            "Easily manage data at any scale with robust access controls, flexible replication tools, and organization-wide visibility.",
            "Run big data analytics, artificial intelligence (AI), machine learning (ML), and high performance computing (HPC) applications.",
            "Meet Recovery Time Objectives (RTO), Recovery Point Objectives (RPO), and compliance requirements with S3's robust replication features."
        };

        using var listener = new HttpListener();
        listener.Prefixes.Add("http://*:8002/");
        listener.Start();

        Console.WriteLine("Listener started. Waiting for requests...");

        try
        {
            while (true)
            {
                var ctx = listener.GetContext();
                using var response = ctx.Response;

                var rnd = new Random();
                int i = rnd.Next(s3_facts.Length);

                Console.WriteLine($"Serving fact index {i}: {s3_facts[i]}");

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes($"{DateTime.Now} - {s3_facts[i]}");
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
        }
        catch (HttpListenerException ex)
        {
            Console.WriteLine($"Listener stopped: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
