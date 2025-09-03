![](assets/banner.png)

# 🚀 The Modern Testing Framework for .NET

**TUnit** is a next-generation testing framework for C# that outpaces traditional frameworks with **source-generated tests**, **parallel execution by default**, and **Native AOT support**. Built on the modern Microsoft.Testing.Platform, TUnit delivers faster test runs, better developer experience, and unmatched flexibility.

<div align="center">

[![thomhurst%2FTUnit | Trendshift](https://trendshift.io/api/badge/repositories/11781)](https://trendshift.io/repositories/11781)


[![Codacy Badge](https://api.codacy.com/project/badge/Grade/a8231644d844435eb9fd15110ea771d8)](https://app.codacy.com/gh/thomhurst/TUnit?utm_source=github.com&utm_medium=referral&utm_content=thomhurst/TUnit&utm_campaign=Badge_Grade)![GitHub Repo stars](https://img.shields.io/github/stars/thomhurst/TUnit) ![GitHub Issues or Pull Requests](https://img.shields.io/github/issues-closed-raw/thomhurst/TUnit)
 [![GitHub Sponsors](https://img.shields.io/github/sponsors/thomhurst)](https://github.com/sponsors/thomhurst) [![nuget](https://img.shields.io/nuget/v/TUnit.svg)](https://www.nuget.org/packages/TUnit/) [![NuGet Downloads](https://img.shields.io/nuget/dt/TUnit)](https://www.nuget.org/packages/TUnit/) ![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/thomhurst/TUnit/dotnet.yml) ![GitHub last commit (branch)](https://img.shields.io/github/last-commit/thomhurst/TUnit/main) ![License](https://img.shields.io/github/license/thomhurst/TUnit)

</div>

## ⚡ Why Choose TUnit?

| Feature | Traditional Frameworks | **TUnit** |
|---------|----------------------|-----------|
| Test Discovery | ❌ Runtime reflection | ✅ **Compile-time generation** |
| Execution Speed | ❌ Sequential by default | ✅ **Parallel by default** |
| Modern .NET | ⚠️ Limited AOT support | ✅ **Full Native AOT & trimming** |
| Test Dependencies | ❌ Not supported | ✅ **`[DependsOn]` chains** |
| Resource Management | ❌ Manual lifecycle | ✅ **Intelligent cleanup** |

⚡ **Parallel by Default** - Tests run concurrently with intelligent dependency management

🎯 **Compile-Time Discovery** - Know your test structure before runtime

🔧 **Modern .NET Ready** - Native AOT, trimming, and latest .NET features

🎭 **Extensible** - Customize data sources, attributes, and test behavior

---

<div align="center">

## 📚 **[Complete Documentation & Learning Center](https://tunit.dev)**

**🚀 New to TUnit?** Start with our **[Getting Started Guide](https://tunit.dev/docs/getting-started/installation)**

**🔄 Migrating?** See our **[Migration Guides](https://tunit.dev/docs/migration/xunit)**

**🎯 Advanced Features?** Explore **[Data-Driven Testing](https://tunit.dev/docs/test-authoring/arguments)**, **[Test Dependencies](https://tunit.dev/docs/test-authoring/depends-on)**, and **[Parallelism Control](https://tunit.dev/docs/parallelism/not-in-parallel)**

</div>

---

## 🏁 Quick Start

### Using the Project Template (Recommended)
```bash
dotnet new install TUnit.Templates
dotnet new TUnit -n "MyTestProject"
```

### Manual Installation
```bash
dotnet add package TUnit --prerelease
```

📖 **[📚 Complete Documentation & Guides](https://tunit.dev)** - Everything you need to master TUnit

## ✨ Key Features

<table>
<tr>
<td width="50%">

**🚀 Performance & Modern Platform**
- 🔥 Source-generated tests (no reflection)
- ⚡ Parallel execution by default
- 🚀 Native AOT & trimming support
- 📈 Optimized for performance

</td>
<td width="50%">

**🎯 Advanced Test Control**
- 🔗 Test dependencies with `[DependsOn]`
- 🎛️ Parallel limits & custom scheduling
- 🛡️ Built-in analyzers & compile-time checks
- 🎭 Custom attributes & extensible conditions

</td>
</tr>
<tr>
<td>

**📊 Rich Data & Assertions**
- 📋 Multiple data sources (`[Arguments]`, `[Matrix]`, `[ClassData]`)
- ✅ Fluent async assertions
- 🔄 Smart retry logic & conditional execution
- 📝 Rich test metadata & context

</td>
<td>

**🔧 Developer Experience**
- 💉 Full dependency injection support
- 🪝 Comprehensive lifecycle hooks
- 🎯 IDE integration (VS, Rider, VS Code)
- 📚 Extensive documentation & examples

</td>
</tr>
</table>

## 📝 Simple Test Example

```csharp
[Test]
public async Task User_Creation_Should_Set_Timestamp()
{
    // Arrange
    var userService = new UserService();

    // Act
    var user = await userService.CreateUserAsync("john.doe@example.com");

    // Assert - TUnit's fluent assertions
    await Assert.That(user.CreatedAt)
        .IsEqualTo(DateTime.Now)
        .Within(TimeSpan.FromMinutes(1));

    await Assert.That(user.Email)
        .IsEqualTo("john.doe@example.com");
}
```

## 🎯 Data-Driven Testing

```csharp
[Test]
[Arguments("user1@test.com", "ValidPassword123")]
[Arguments("user2@test.com", "AnotherPassword456")]
[Arguments("admin@test.com", "AdminPass789")]
public async Task User_Login_Should_Succeed(string email, string password)
{
    var result = await authService.LoginAsync(email, password);
    await Assert.That(result.IsSuccess).IsTrue();
}

// Matrix testing - tests all combinations
[Test]
[MatrixDataSource]
public async Task Database_Operations_Work(
    [Matrix("Create", "Update", "Delete")] string operation,
    [Matrix("User", "Product", "Order")] string entity)
{
    await Assert.That(await ExecuteOperation(operation, entity))
        .IsTrue();
}
```

## 🔗 Advanced Test Orchestration

```csharp
[Before(Class)]
public static async Task SetupDatabase(ClassHookContext context)
{
    await DatabaseHelper.InitializeAsync();
}

[Test, DisplayName("Register a new account")]
[MethodDataSource(nameof(GetTestUsers))]
public async Task Register_User(string username, string password)
{
    // Test implementation
}

[Test, DependsOn(nameof(Register_User))]
[Retry(3)] // Retry on failure
public async Task Login_With_Registered_User(string username, string password)
{
    // This test runs after Register_User completes
}

[Test]
[ParallelLimit<LoadTestParallelLimit>] // Custom parallel control
[Repeat(100)] // Run 100 times
public async Task Load_Test_Homepage()
{
    // Performance testing
}

// Custom attributes
[Test, WindowsOnly, RetryOnHttpError(5)]
public async Task Windows_Specific_Feature()
{
    // Platform-specific test with custom retry logic
}

public class LoadTestParallelLimit : IParallelLimit
{
    public int Limit => 10; // Limit to 10 concurrent executions
}
```

## 🔧 Smart Test Control

```csharp
// Custom conditional execution
public class WindowsOnlyAttribute : SkipAttribute
{
    public WindowsOnlyAttribute() : base("Windows only test") { }

    public override Task<bool> ShouldSkip(TestContext testContext)
        => Task.FromResult(!OperatingSystem.IsWindows());
}

// Custom retry logic
public class RetryOnHttpErrorAttribute : RetryAttribute
{
    public RetryOnHttpErrorAttribute(int times) : base(times) { }

    public override Task<bool> ShouldRetry(TestInformation testInformation,
        Exception exception, int currentRetryCount)
        => Task.FromResult(exception is HttpRequestException { StatusCode: HttpStatusCode.ServiceUnavailable });
}
```

## 🎯 Perfect For Every Testing Scenario

<table>
<tr>
<td width="33%">

### 🧪 **Unit Testing**
```csharp
[Test]
[Arguments(1, 2, 3)]
[Arguments(5, 10, 15)]
public async Task Calculate_Sum(int a, int b, int expected)
{
    await Assert.That(Calculator.Add(a, b))
        .IsEqualTo(expected);
}
```
**Fast, isolated, and reliable**

</td>
<td width="33%">

### 🔗 **Integration Testing**
```csharp
[Test, DependsOn(nameof(CreateUser))]
public async Task Login_After_Registration()
{
    // Runs after CreateUser completes
    var result = await authService.Login(user);
    await Assert.That(result.IsSuccess).IsTrue();
}
```
**Stateful workflows made simple**

</td>
<td width="33%">

### ⚡ **Load Testing**
```csharp
[Test]
[ParallelLimit<LoadTestLimit>]
[Repeat(1000)]
public async Task API_Handles_Concurrent_Requests()
{
    await Assert.That(await httpClient.GetAsync("/api/health"))
        .HasStatusCode(HttpStatusCode.OK);
}
```
**Built-in performance testing**

</td>
</tr>
</table>

## 🚀 What Makes TUnit Different?

### **Compile-Time Intelligence**
Tests are discovered at build time, not runtime - enabling faster discovery, better IDE integration, and precise resource lifecycle management.

### **Parallel-First Architecture**
Built for concurrency from day one with `[DependsOn]` for test chains, `[ParallelLimit]` for resource control, and intelligent scheduling.

### **Extensible by Design**
The `DataSourceGenerator<T>` pattern and custom attribute system let you extend TUnit's capabilities without modifying core framework code.

## 🏆 Community & Ecosystem

<div align="center">

**🌟 Join thousands of developers modernizing their testing**

[![Downloads](https://img.shields.io/nuget/dt/TUnit?label=Downloads&color=blue)](https://www.nuget.org/packages/TUnit/)
[![Contributors](https://img.shields.io/github/contributors/thomhurst/TUnit?label=Contributors)](https://github.com/thomhurst/TUnit/graphs/contributors)
[![Discussions](https://img.shields.io/github/discussions/thomhurst/TUnit?label=Discussions)](https://github.com/thomhurst/TUnit/discussions)

</div>

### 🤝 **Active Community**
- 📚 **[Official Documentation](https://tunit.dev)** - Comprehensive guides, tutorials, and API reference
- 💬 **[GitHub Discussions](https://github.com/thomhurst/TUnit/discussions)** - Get help and share ideas
- 🐛 **[Issue Tracking](https://github.com/thomhurst/TUnit/issues)** - Report bugs and request features
- 📢 **[Release Notes](https://github.com/thomhurst/TUnit/releases)** - Stay updated with latest improvements

## 🛠️ IDE Support

TUnit works seamlessly across all major .NET development environments:

### Visual Studio (2022 17.13+)
✅ **Fully supported** - No additional configuration needed for latest versions

⚙️ **Earlier versions**: Enable "Use testing platform server mode" in Tools > Manage Preview Features

### JetBrains Rider
✅ **Fully supported**

⚙️ **Setup**: Enable "Testing Platform support" in Settings > Build, Execution, Deployment > Unit Testing > VSTest

### Visual Studio Code
✅ **Fully supported**

⚙️ **Setup**: Install [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) and enable "Use Testing Platform Protocol"

### Command Line
✅ **Full CLI support** - Works with `dotnet test`, `dotnet run`, and direct executable execution

## 📦 Package Options

| Package | Use Case |
|---------|----------|
| **`TUnit`** | ⭐ **Start here** - Complete testing framework (includes Core + Engine + Assertions) |
| **`TUnit.Core`** | 📚 Test libraries and shared components (no execution engine) |
| **`TUnit.Engine`** | 🚀 Test execution engine and adapter (for test projects) |
| **`TUnit.Assertions`** | ✅ Standalone assertions (works with any test framework) |
| **`TUnit.Playwright`** | 🎭 Playwright integration with automatic lifecycle management |

## 🎯 Migration from Other Frameworks

**Coming from NUnit or xUnit?** TUnit maintains familiar syntax while adding modern capabilities:

```csharp
// Enhanced with TUnit's advanced features
[Test]
[Arguments("value1")]
[Arguments("value2")]
[Retry(3)]
[ParallelLimit<CustomLimit>]
public async Task Modern_TUnit_Test(string value) { }
```

📖 **Need help migrating?** Check our detailed **[Migration Guides](https://tunit.dev/docs/migration/xunit)** with step-by-step instructions for xUnit, NUnit, and MSTest.


## 💡 Current Status

The API is mostly stable, but may have some changes based on feedback or issues before v1.0 release.

---

<div align="center">

## 🚀 Ready to Experience the Future of .NET Testing?

### ⚡ **Start in 30 Seconds**

```bash
# Create a new test project with examples
dotnet new install TUnit.Templates && dotnet new TUnit -n "MyAwesomeTests"

# Or add to existing project
dotnet add package TUnit --prerelease
```

### 🎯 **Why Wait? Join the Movement**

<table>
<tr>
<td align="center" width="25%">

### 📈 **Performance**
**Optimized execution**
**Parallel by default**
**Zero reflection overhead**

</td>
<td align="center" width="25%">

### 🔮 **Future-Ready**
**Native AOT support**
**Latest .NET features**
**Source generation**

</td>
<td align="center" width="25%">

### 🛠️ **Developer Experience**
**Compile-time checks**
**Rich IDE integration**
**Intelligent debugging**

</td>
<td align="center" width="25%">

### 🎭 **Flexibility**
**Test dependencies**
**Custom attributes**
**Extensible architecture**

</td>
</tr>
</table>

---

**📖 Learn More**: [tunit.dev](https://tunit.dev) | **💬 Get Help**: [GitHub Discussions](https://github.com/thomhurst/TUnit/discussions) | **⭐ Show Support**: [Star on GitHub](https://github.com/thomhurst/TUnit)

*TUnit is actively developed and production-ready. Join our growing community of developers who've made the switch!*

</div>

## Performance Benchmark

### Scenario: Building the test project

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.5 (24F74) [Darwin 24.5.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method       | Version | Mean    | Error    | StdDev   | Median  |
|------------- |-------- |--------:|---------:|---------:|--------:|
| Build_TUnit  | 0.57.1  | 2.023 s | 0.1891 s | 0.5575 s | 1.903 s |
| Build_NUnit  | 4.4.0   | 1.806 s | 0.1326 s | 0.3848 s | 1.820 s |
| Build_xUnit  | 2.9.3   | 1.382 s | 0.1071 s | 0.3072 s | 1.333 s |
| Build_MSTest | 3.10.3  | 1.346 s | 0.0782 s | 0.2295 s | 1.289 s |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method       | Version | Mean    | Error    | StdDev   | Median  |
|------------- |-------- |--------:|---------:|---------:|--------:|
| Build_TUnit  | 0.57.1  | 1.851 s | 0.0324 s | 0.0550 s | 1.835 s |
| Build_NUnit  | 4.4.0   | 1.539 s | 0.0175 s | 0.0155 s | 1.544 s |
| Build_xUnit  | 2.9.3   | 1.540 s | 0.0157 s | 0.0131 s | 1.541 s |
| Build_MSTest | 3.10.3  | 1.533 s | 0.0179 s | 0.0168 s | 1.532 s |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method       | Version | Mean    | Error    | StdDev   | Median  |
|------------- |-------- |--------:|---------:|---------:|--------:|
| Build_TUnit  | 0.57.1  | 1.883 s | 0.0366 s | 0.0803 s | 1.860 s |
| Build_NUnit  | 4.4.0   | 1.563 s | 0.0227 s | 0.0202 s | 1.563 s |
| Build_xUnit  | 2.9.3   | 1.563 s | 0.0208 s | 0.0195 s | 1.560 s |
| Build_MSTest | 3.10.3  | 1.573 s | 0.0141 s | 0.0125 s | 1.574 s |


### Scenario: Tests focused on assertion performance and validation

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.5 (24F74) [Darwin 24.5.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean | Error | StdDev | Median |
|---------- |-------- |-----:|------:|-------:|-------:|
| TUnit_AOT | 0.57.1  |   NA |    NA |     NA |     NA |
| TUnit     | 0.57.1  |   NA |    NA |     NA |     NA |
| NUnit     | 4.4.0   |   NA |    NA |     NA |     NA |
| xUnit     | 2.9.3   |   NA |    NA |     NA |     NA |
| MSTest    | 3.10.3  |   NA |    NA |     NA |     NA |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean | Error | StdDev | Median |
|---------- |-------- |-----:|------:|-------:|-------:|
| TUnit_AOT | 0.57.1  |   NA |    NA |     NA |     NA |
| TUnit     | 0.57.1  |   NA |    NA |     NA |     NA |
| NUnit     | 4.4.0   |   NA |    NA |     NA |     NA |
| xUnit     | 2.9.3   |   NA |    NA |     NA |     NA |
| MSTest    | 3.10.3  |   NA |    NA |     NA |     NA |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean | Error | StdDev | Median |
|---------- |-------- |-----:|------:|-------:|-------:|
| TUnit_AOT | 0.57.1  |   NA |    NA |     NA |     NA |
| TUnit     | 0.57.1  |   NA |    NA |     NA |     NA |
| NUnit     | 4.4.0   |   NA |    NA |     NA |     NA |
| xUnit     | 2.9.3   |   NA |    NA |     NA |     NA |
| MSTest    | 3.10.3  |   NA |    NA |     NA |     NA |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)


### Scenario: Tests running asynchronous operations and async/await patterns

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.5 (24F74) [Darwin 24.5.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean     | Error    | StdDev    | Median   |
|---------- |-------- |---------:|---------:|----------:|---------:|
| TUnit_AOT | 0.57.1  | 144.6 ms | 12.01 ms |  35.42 ms | 139.3 ms |
| TUnit     | 0.57.1  | 574.4 ms | 13.32 ms |  38.43 ms | 561.5 ms |
| NUnit     | 4.4.0   | 949.5 ms | 48.79 ms | 135.18 ms | 913.0 ms |
| xUnit     | 2.9.3   | 889.3 ms | 25.08 ms |  73.16 ms | 890.6 ms |
| MSTest    | 3.10.3  | 751.8 ms | 19.57 ms |  56.77 ms | 719.0 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    27.53 ms |  0.397 ms |  0.352 ms |    27.51 ms |
| TUnit     | 0.57.1  |   954.54 ms | 18.860 ms | 23.852 ms |   951.19 ms |
| NUnit     | 4.4.0   | 1,342.01 ms | 14.905 ms | 13.942 ms | 1,343.23 ms |
| xUnit     | 2.9.3   | 1,444.05 ms | 12.787 ms | 11.961 ms | 1,448.40 ms |
| MSTest    | 3.10.3  | 1,284.37 ms | 17.846 ms | 16.693 ms | 1,278.33 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    64.53 ms |  1.200 ms |  1.123 ms |    64.39 ms |
| TUnit     | 0.57.1  | 1,015.01 ms | 19.945 ms | 28.604 ms | 1,012.21 ms |
| NUnit     | 4.4.0   | 1,406.54 ms | 26.794 ms | 23.752 ms | 1,402.15 ms |
| xUnit     | 2.9.3   | 1,494.90 ms | 22.739 ms | 21.270 ms | 1,497.16 ms |
| MSTest    | 3.10.3  | 1,290.23 ms |  8.896 ms |  6.945 ms | 1,291.80 ms |


### Scenario: Simple tests with basic operations and assertions

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.5 (24F74) [Darwin 24.5.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean       | Error     | StdDev    | Median     |
|---------- |-------- |-----------:|----------:|----------:|-----------:|
| TUnit_AOT | 0.57.1  |   176.6 ms |  12.00 ms |  34.24 ms |   174.4 ms |
| TUnit     | 0.57.1  |   715.8 ms |  48.57 ms | 132.97 ms |   679.0 ms |
| NUnit     | 4.4.0   | 1,171.4 ms | 111.36 ms | 315.90 ms | 1,051.8 ms |
| xUnit     | 2.9.3   |   990.6 ms |  37.08 ms | 106.99 ms |   963.6 ms |
| MSTest    | 3.10.3  | 1,023.8 ms |  60.73 ms | 174.26 ms |   996.1 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    26.97 ms |  0.521 ms |  0.512 ms |    26.78 ms |
| TUnit     | 0.57.1  |   944.29 ms | 18.555 ms | 21.368 ms |   938.87 ms |
| NUnit     | 4.4.0   | 1,317.73 ms |  8.091 ms |  7.568 ms | 1,317.22 ms |
| xUnit     | 2.9.3   | 1,387.80 ms | 10.673 ms |  9.462 ms | 1,387.08 ms |
| MSTest    | 3.10.3  | 1,267.30 ms | 12.331 ms | 11.535 ms | 1,263.39 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    55.04 ms |  1.096 ms |  1.218 ms |    55.23 ms |
| TUnit     | 0.57.1  | 1,002.27 ms | 19.391 ms | 25.214 ms | 1,005.75 ms |
| NUnit     | 4.4.0   | 1,340.04 ms |  8.805 ms |  7.806 ms | 1,342.01 ms |
| xUnit     | 2.9.3   | 1,435.62 ms | 27.818 ms | 42.481 ms | 1,422.05 ms |
| MSTest    | 3.10.3  | 1,296.29 ms | 13.084 ms | 12.239 ms | 1,297.95 ms |


### Scenario: Parameterized tests with multiple test cases using data attributes

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.5 (24F74) [Darwin 24.5.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean    | Error    | StdDev   | Median  |
|---------- |-------- |--------:|---------:|---------:|--------:|
| TUnit_AOT | 0.57.1  |      NA |       NA |       NA |      NA |
| TUnit     | 0.57.1  |      NA |       NA |       NA |      NA |
| NUnit     | 4.4.0   | 1.514 s | 0.1724 s | 0.5030 s | 1.330 s |
| xUnit     | 2.9.3   | 1.386 s | 0.0779 s | 0.2236 s | 1.326 s |
| MSTest    | 3.10.3  | 1.226 s | 0.0688 s | 0.2008 s | 1.209 s |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean    | Error    | StdDev   | Median  |
|---------- |-------- |--------:|---------:|---------:|--------:|
| TUnit_AOT | 0.57.1  |      NA |       NA |       NA |      NA |
| TUnit     | 0.57.1  |      NA |       NA |       NA |      NA |
| NUnit     | 4.4.0   | 1.336 s | 0.0223 s | 0.0198 s | 1.337 s |
| xUnit     | 2.9.3   | 1.441 s | 0.0129 s | 0.0120 s | 1.440 s |
| MSTest    | 3.10.3  | 1.311 s | 0.0108 s | 0.0101 s | 1.311 s |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean    | Error    | StdDev   | Median  |
|---------- |-------- |--------:|---------:|---------:|--------:|
| TUnit_AOT | 0.57.1  |      NA |       NA |       NA |      NA |
| TUnit     | 0.57.1  |      NA |       NA |       NA |      NA |
| NUnit     | 4.4.0   | 1.332 s | 0.0199 s | 0.0177 s | 1.334 s |
| xUnit     | 2.9.3   | 1.391 s | 0.0140 s | 0.0131 s | 1.395 s |
| MSTest    | 3.10.3  | 1.284 s | 0.0095 s | 0.0085 s | 1.282 s |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)


### Scenario: Tests utilizing class fixtures and shared test context

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.5 (24F74) [Darwin 24.5.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean       | Error     | StdDev    | Median     |
|---------- |-------- |-----------:|----------:|----------:|-----------:|
| TUnit_AOT | 0.57.1  |   200.7 ms |  18.10 ms |  53.07 ms |   183.0 ms |
| TUnit     | 0.57.1  | 1,062.0 ms | 106.38 ms | 301.79 ms |   989.8 ms |
| NUnit     | 4.4.0   | 1,355.6 ms |  98.85 ms | 288.36 ms | 1,289.4 ms |
| xUnit     | 2.9.3   | 1,159.9 ms | 107.57 ms | 299.87 ms | 1,059.4 ms |
| MSTest    | 3.10.3  | 1,159.6 ms |  69.53 ms | 200.62 ms | 1,125.8 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    25.75 ms |  0.200 ms |  0.187 ms |    25.77 ms |
| TUnit     | 0.57.1  |   941.83 ms | 18.618 ms | 16.504 ms |   938.29 ms |
| NUnit     | 4.4.0   | 1,292.56 ms | 15.713 ms | 13.930 ms | 1,291.21 ms |
| xUnit     | 2.9.3   | 1,374.80 ms |  8.310 ms |  7.773 ms | 1,372.40 ms |
| MSTest    | 3.10.3  |          NA |        NA |        NA |          NA |

Benchmarks with issues:
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    52.90 ms |  1.047 ms |  2.231 ms |    53.05 ms |
| TUnit     | 0.57.1  | 1,016.66 ms | 19.858 ms | 29.723 ms | 1,010.89 ms |
| NUnit     | 4.4.0   | 1,356.25 ms | 23.300 ms | 21.794 ms | 1,353.49 ms |
| xUnit     | 2.9.3   | 1,416.77 ms | 23.697 ms | 36.189 ms | 1,404.75 ms |
| MSTest    | 3.10.3  |          NA |        NA |        NA |          NA |

Benchmarks with issues:
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)


### Scenario: Tests executing in parallel to test framework parallelization

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.5 (24F74) [Darwin 24.5.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean       | Error     | StdDev    | Median     |
|---------- |-------- |-----------:|----------:|----------:|-----------:|
| TUnit_AOT | 0.57.1  |   159.6 ms |  12.20 ms |  35.96 ms |   153.0 ms |
| TUnit     | 0.57.1  | 1,095.1 ms | 110.04 ms | 321.00 ms | 1,064.3 ms |
| NUnit     | 4.4.0   |   977.5 ms |  44.59 ms | 125.75 ms |   968.1 ms |
| xUnit     | 2.9.3   | 1,066.4 ms |  65.64 ms | 190.44 ms | 1,083.3 ms |
| MSTest    | 3.10.3  | 1,048.9 ms |  67.11 ms | 197.88 ms | 1,017.0 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    26.18 ms |  0.167 ms |  0.140 ms |    26.19 ms |
| TUnit     | 0.57.1  |   941.77 ms | 17.880 ms | 19.874 ms |   941.83 ms |
| NUnit     | 4.4.0   | 1,318.87 ms | 10.279 ms |  9.615 ms | 1,316.97 ms |
| xUnit     | 2.9.3   | 1,392.11 ms | 12.192 ms | 11.405 ms | 1,389.37 ms |
| MSTest    | 3.10.3  | 1,264.20 ms |  8.578 ms |  8.024 ms | 1,263.07 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    49.57 ms |  0.941 ms |  2.495 ms |    48.61 ms |
| TUnit     | 0.57.1  |   988.53 ms | 19.077 ms | 24.127 ms |   980.72 ms |
| NUnit     | 4.4.0   | 1,341.71 ms | 14.739 ms | 13.787 ms | 1,343.28 ms |
| xUnit     | 2.9.3   | 1,394.14 ms | 12.679 ms | 11.860 ms | 1,395.03 ms |
| MSTest    | 3.10.3  | 1,289.25 ms | 12.167 ms | 11.381 ms | 1,291.89 ms |


### Scenario: A test that takes 50ms to execute, repeated 100 times

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.5 (24F74) [Darwin 24.5.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean | Error | StdDev | Median |
|---------- |-------- |-----:|------:|-------:|-------:|
| TUnit_AOT | 0.57.1  |   NA |    NA |     NA |     NA |
| TUnit     | 0.57.1  |   NA |    NA |     NA |     NA |
| NUnit     | 4.4.0   |   NA |    NA |     NA |     NA |
| xUnit     | 2.9.3   |   NA |    NA |     NA |     NA |
| MSTest    | 3.10.3  |   NA |    NA |     NA |     NA |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean | Error | StdDev | Median |
|---------- |-------- |-----:|------:|-------:|-------:|
| TUnit_AOT | 0.57.1  |   NA |    NA |     NA |     NA |
| TUnit     | 0.57.1  |   NA |    NA |     NA |     NA |
| NUnit     | 4.4.0   |   NA |    NA |     NA |     NA |
| xUnit     | 2.9.3   |   NA |    NA |     NA |     NA |
| MSTest    | 3.10.3  |   NA |    NA |     NA |     NA |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean | Error | StdDev | Median |
|---------- |-------- |-----:|------:|-------:|-------:|
| TUnit_AOT | 0.57.1  |   NA |    NA |     NA |     NA |
| TUnit     | 0.57.1  |   NA |    NA |     NA |     NA |
| NUnit     | 4.4.0   |   NA |    NA |     NA |     NA |
| xUnit     | 2.9.3   |   NA |    NA |     NA |     NA |
| MSTest    | 3.10.3  |   NA |    NA |     NA |     NA |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)


### Scenario: Tests with setup and teardown lifecycle methods

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.5 (24F74) [Darwin 24.5.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean       | Error     | StdDev    | Median     |
|---------- |-------- |-----------:|----------:|----------:|-----------:|
| TUnit_AOT | 0.57.1  |   177.3 ms |  18.32 ms |  54.02 ms |   163.6 ms |
| TUnit     | 0.57.1  |   806.8 ms |  62.61 ms | 170.32 ms |   763.0 ms |
| NUnit     | 4.4.0   | 1,528.7 ms | 155.18 ms | 457.56 ms | 1,430.8 ms |
| xUnit     | 2.9.3   | 1,085.3 ms |  60.48 ms | 177.37 ms | 1,057.9 ms |
| MSTest    | 3.10.3  | 1,035.1 ms |  51.46 ms | 145.15 ms | 1,019.1 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    27.19 ms |  0.479 ms |  0.448 ms |    27.14 ms |
| TUnit     | 0.57.1  |   956.38 ms | 18.934 ms | 21.045 ms |   952.43 ms |
| NUnit     | 4.4.0   | 1,331.42 ms | 19.603 ms | 18.337 ms | 1,333.54 ms |
| xUnit     | 2.9.3   | 1,395.60 ms | 12.300 ms | 10.271 ms | 1,395.31 ms |
| MSTest    | 3.10.3  | 1,275.50 ms | 18.887 ms | 16.743 ms | 1,278.36 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    51.87 ms |  1.031 ms |  1.512 ms |    51.28 ms |
| TUnit     | 0.57.1  | 1,001.79 ms | 19.929 ms | 26.604 ms |   991.69 ms |
| NUnit     | 4.4.0   | 1,338.30 ms | 12.517 ms | 11.096 ms | 1,338.74 ms |
| xUnit     | 2.9.3   | 1,393.73 ms | 20.387 ms | 18.073 ms | 1,389.35 ms |
| MSTest    | 3.10.3  | 1,289.19 ms | 11.702 ms | 10.946 ms | 1,288.32 ms |



