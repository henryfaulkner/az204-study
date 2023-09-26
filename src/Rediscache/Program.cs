using StackExchange.Redis;

string connectionString = "az204-redis-slice.redis.cache.windows.net:6380,password=IigWOy1mMMIxNub8GkPuFj96tT8cZkUxrAzCaAh2FxQ=,ssl=True,abortConnect=False";

using (var cache = ConnectionMultiplexer.Connect(connectionString))
{
  IDatabase db = cache.GetDatabase();

  var result = await db.ExecuteAsync("ping");
  Console.WriteLine($"PING = {result.Type} : {result.ToString()}");

  bool setValue = await db.StringSetAsync("test:key", "100");
  Console.WriteLine($"SET: {setValue}");

  string getValue = await db.StringGetAsync("test:key");
  Console.WriteLine($"GET: {getValue}");
}