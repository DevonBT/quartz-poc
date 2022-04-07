using Quartz;
using Quartz.Impl;
using Quartz_Proof_Of_Concept;
using Microsoft.Data.Sqlite;

Console.WriteLine("Job Service: Quartz Abstraction Proof-of-Concept");
Console.WriteLine("================================================");
Console.WriteLine();

// Data setup

List<Schedule> schedules = new List<Schedule>();
using (var connection = new SqliteConnection("Data Source=C:\\Users\\dparsons\\work\\quartz-poc\\quartzdb"))
{
    connection.Open();

    var schedule_command = connection.CreateCommand();
    schedule_command.CommandText = "SELECT * FROM schedules";

    using (var reader = schedule_command.ExecuteReader())
    {
        while (reader.Read())
        {
            var Id = reader.GetGuid(0);
            var JobId = reader.GetInt32(1);
            var Cron = reader.GetString(2);

            schedules.Add(new Schedule
            {
                Id = Id,
                JobId = JobId,
                Cron = Cron
            });
        }
    }

    schedules.ForEach(s =>
    {
        Dictionary<string, string> Params = new Dictionary<string, string>();

        var param_command = connection.CreateCommand();
        param_command.CommandText = "SELECT * from schedule_params WHERE ScheduleId = $id";
        param_command.Parameters.AddWithValue("$id", s.Id.ToString());
        using (var reader = param_command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine("reading from table");
                Params.Add(reader.GetString(1), reader.GetString(2));
            }
        }
        s.Parameters = Params;
        Params.Keys.ToList().ForEach(key => Console.WriteLine(key));
        Params.Values.ToList().ForEach(value => Console.WriteLine(value));

        Console.WriteLine();
    });
}

// Quartz setup

StdSchedulerFactory factory = new StdSchedulerFactory();
IScheduler scheduler = await factory.GetScheduler();
await scheduler.Start();

// Load schedules into Quartz
    // This will be different in real impl?
    // Schedules only need to be loaded once each

schedules.ForEach(async s =>
{
    Console.WriteLine($"Scheduling {s.Id}: {s.Job.Name}, {s.Cron}");
    var quartzjob = s.Job.ToQuartz();
    var trigger = s.Trigger;

    await scheduler.ScheduleJob(quartzjob, trigger);
});

Console.WriteLine();
Console.ReadLine();