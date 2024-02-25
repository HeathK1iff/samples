using Microsoft.Extensions.Logging;

using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => 
{
    builder.AddProvider(new FileLoggerProvider("test.log"));
});
 ILogger logger = loggerFactory.CreateLogger("Program");
 logger.LogInformation("Information");


public class FileLoggerProvider : ILoggerProvider
{
    private FileStream _stream;
    private TextWriter _writer;    

    public FileLoggerProvider(string path)
    {
        _stream = new FileStream(path, FileMode.OpenOrCreate);
        _writer = new StreamWriter(_stream);
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(_writer, categoryName); 
    }

    public void Dispose()
    {
        _stream.Dispose();
    }
}


public class FileLogger : ILogger
{
    private TextWriter _writer;
    private string _categoryName;
    public FileLogger(TextWriter writer, string categoryName)
    {
        _writer = writer;
        _categoryName = categoryName;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
       return logLevel >= LogLevel.Information;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
         if (!IsEnabled(logLevel))
        {
            return;
        }

        var message = formatter(state, exception);

        _writer.WriteLine($"[{logLevel}] [{_categoryName}] {message}");
        _writer.Flush();
    }
}