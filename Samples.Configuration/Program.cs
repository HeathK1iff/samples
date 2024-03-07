using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// Configure  of configuration
// Need nuget packages: Microsoft.Extensions.Configuration, Microsoft.Extensions.Configuration.Ini, Microsoft.Extensions.Configuration.Binder 
const string ConfigurationFileName = "app.ini";
IConfiguration configuration = new ConfigurationBuilder()
                                .AddIniFile(ConfigurationFileName)
                                .Build();

// Example Option pattern
var options = new TestSectionOptions();
configuration.GetSection(nameof(TestSectionOptions)).Bind(options);
Console.WriteLine($"SectionDay {options.SectionDay}");
Console.WriteLine($"SectionKey {options.SectionKey}");
Console.WriteLine($"SectionStatus {options.SectionStatus}");

// Configure Configuration in DI
IServiceCollection serviceDescriptors = new ServiceCollection();

// Bind the configuration to TestSectionOptions
serviceDescriptors
        .Configure<TestSectionOptions>(_ => configuration.GetSection(key: nameof(TestSectionOptions)));

serviceDescriptors.AddScoped(_ => configuration);
serviceDescriptors.AddScoped<ISimpleServiceWithConfiguration, SimpleServiceWithConfiguration>();
serviceDescriptors.AddScoped<ISimpleSectionService, SimpleSectionService>();

// Example of injection Configuration in custom service
IServiceProvider serviceProvider = serviceDescriptors.BuildServiceProvider();
using (IServiceScope serviceScope = serviceProvider.CreateScope())
{
    var service = serviceScope.ServiceProvider.GetRequiredService<ISimpleServiceWithConfiguration>();
    service.PrintSimpleKey();
    Console.WriteLine("-------------------");
    var service2 = serviceScope.ServiceProvider.GetRequiredService<ISimpleSectionService>();
    service2.Print();
}


#region Options
// Must be non-abstract with a public parameterless constructor
// Contain public read-write properties to bind (fields are not bound)
public class TestSectionOptions
{
    public int SectionKey { get; set; }
    public bool SectionStatus { get; set; }
    public TimeSpan SectionDay { get; set; }
}

#endregion Options

#region SimpleServiceWithConfiguration
public interface ISimpleServiceWithConfiguration{
    void PrintSimpleKey();
}

public class SimpleServiceWithConfiguration : ISimpleServiceWithConfiguration
{
    private IConfiguration _configuration;
    public SimpleServiceWithConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void PrintSimpleKey()
    {
        string value = _configuration["TestKey"] ?? string.Empty;

        Console.WriteLine(value);
    }
}

#endregion SimpleServiceWithConfiguration

#region SimpleSectionService
public interface ISimpleSectionService{
    void Print();
}

public class SimpleSectionService : ISimpleSectionService
{
    private TestSectionOptions _options;
    public SimpleSectionService(IOptions<TestSectionOptions> options)
    {
        _options = options.Value;
    }

    public void Print()
    {
        Console.WriteLine($"#SectionDay {_options.SectionDay}");
        Console.WriteLine($"#SectionKey {_options.SectionKey}");
        Console.WriteLine($"#SectionStatus {_options.SectionStatus}");
    }
}
#endregion SimpleSectionService