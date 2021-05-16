using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class Server
{
    public static void ProcessAuthorization(string requestsPath, string requestsResultsPath)
    {
        // Check users with incorrect password.
        Dictionary<string, DateTime> incorrectPasswordUsers = new Dictionary<string, DateTime>();
        Dictionary<string, DateTime> successUsers = new Dictionary<string, DateTime>();
        List<string> blockedUers = new List<string>();
        // Exit users.
        List<string> leaveUsers = new List<string>();
        using (var streamReader = new StreamReader(requestsPath))
        {
            using (var streamWriter = new StreamWriter(requestsResultsPath))
            {
                while (streamReader.Peek() != -1)
                {
                    var query = streamReader.ReadLine().Split(' ');
                    if (query[0] == "SI")
                    {
                        if (UserDb.Users.ContainsKey(query[1]))
                        {
                            var time = DateTime.ParseExact(query[3] + " " + query[4],
                                "dd'.'MM'.'yyyy' 'HH':'mm':'ss", CultureInfo.InvariantCulture);
                            if (successUsers.ContainsKey(query[1]))
                            {
                                if (time - successUsers[query[1]] < new TimeSpan(24,0,0))
                                {
                                    blockedUers.Add(query[1]);
                                    streamWriter.WriteLine($"{query[1]}> account blocked due suspicious login attempt");
                                }
                            }
                            if (UserDb.Users[query[1]] == query[2])
                            {
                                if (leaveUsers.Contains(query[1]))
                                {
                                    if (!successUsers.ContainsKey(query[1]))
                                    {
                                        var timeline = DateTime.Parse(query[3] + " " + query[4],
                                            CultureInfo.GetCultureInfo("ru"));
                                        successUsers.Add(query[1], timeline);
                                    }

                                    streamWriter.WriteLine($"{query[1]}> sign in successful");
                                }
                                else
                                {
                                    if (successUsers.ContainsKey(query[1]))
                                    {
                                        var timeline = DateTime.ParseExact(query[3] + " " + query[4],
                                            "dd'.'MM'.'yyyy' 'HH':'mm':'ss", CultureInfo.InvariantCulture);
                                        if (timeline - successUsers[query[1]] < new TimeSpan(24,0,0))
                                        {
                                            blockedUers.Add(query[1]);
                                            streamWriter.WriteLine(
                                                $"{query[1]}> account blocked due suspicious login attempt");
                                        }
                                        else
                                            streamWriter.WriteLine($"{query[1]}> sign in successful");
                                    }
                                    else
                                    {
                                        if (!successUsers.ContainsKey(query[1]))
                                        {
                                            var timeline = DateTime.ParseExact(query[3] + " " + query[4],
                                                "dd'.'MM'.'yyyy' 'HH':'mm':'ss",
                                                CultureInfo.InvariantCulture);
                                            successUsers.Add(query[1], timeline);
                                        }

                                        streamWriter.WriteLine($"{query[1]}> sign in successful");
                                    }
                                }
                            }
                            // Incorrect password.
                            else
                            {
                                if (incorrectPasswordUsers.ContainsKey(query[1]))
                                {
                                    var timeline = DateTime.ParseExact(query[3] + " " + query[4],
                                        "dd'.'MM'.'yyyy' 'HH':'mm':'ss", CultureInfo.InvariantCulture);
                                    if (timeline - incorrectPasswordUsers[query[1]] < new TimeSpan(1,0,0))
                                    {
                                        blockedUers.Add(query[1]);
                                        streamWriter.WriteLine(
                                            $"{query[1]}> account blocked due suspicious login attempt");
                                    }
                                    else
                                        streamWriter.WriteLine($"{query[1]}> incorrect password");
                                }
                                else
                                {
                                    var timeLine = DateTime.ParseExact(query[3] + " " + query[4],
                                        "dd'.'MM'.'yyyy' 'HH':'mm':'ss", CultureInfo.InvariantCulture);
                                    incorrectPasswordUsers.Add(query[1], timeLine);
                                    streamWriter.WriteLine($"{query[1]}> incorrect password");
                                }
                            }
                        }
                        else
                        {
                            streamWriter.WriteLine($"{query[1]}> no user with such login");
                        }
                    }
                    else if (query[0] == "SO")
                    {
                        if (UserDb.Users.ContainsKey(query[1]))
                        {
                            if (blockedUers.Contains(query[1]))
                            {
                                streamWriter.WriteLine($"{query[1]}> account blocked due suspicious login attempt");
                            }
                            else
                            {
                                leaveUsers.Add(query[1]);
                                streamWriter.WriteLine($"{query[1]}> sign out successful");
                            }
                        }
                        else
                            streamWriter.WriteLine($"{query[1]}> no user with such login");
                    }
                }
            }
        }
    }
}