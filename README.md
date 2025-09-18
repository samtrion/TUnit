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

BenchmarkDotNet v0.15.2, macOS Sequoia 15.6 (24G84) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method       | Version | Mean    | Error    | StdDev   | Median  |
|------------- |-------- |--------:|---------:|---------:|--------:|
| Build_TUnit  | 0.57.1  | 1.655 s | 0.1016 s | 0.2931 s | 1.579 s |
| Build_NUnit  | 4.4.0   | 1.881 s | 0.1121 s | 0.3254 s | 1.848 s |
| Build_xUnit  | 2.9.3   | 2.157 s | 0.1323 s | 0.3880 s | 2.192 s |
| Build_MSTest | 3.10.3  | 2.043 s | 0.1424 s | 0.4200 s | 2.003 s |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method       | Version | Mean    | Error    | StdDev   | Median  |
|------------- |-------- |--------:|---------:|---------:|--------:|
| Build_TUnit  | 0.57.1  | 1.967 s | 0.0391 s | 0.1043 s | 1.925 s |
| Build_NUnit  | 4.4.0   | 1.614 s | 0.0236 s | 0.0221 s | 1.606 s |
| Build_xUnit  | 2.9.3   | 1.629 s | 0.0178 s | 0.0158 s | 1.632 s |
| Build_MSTest | 3.10.3  | 1.640 s | 0.0168 s | 0.0149 s | 1.639 s |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method       | Version | Mean    | Error    | StdDev   | Median  |
|------------- |-------- |--------:|---------:|---------:|--------:|
| Build_TUnit  | 0.57.1  | 1.857 s | 0.0371 s | 0.0659 s | 1.842 s |
| Build_NUnit  | 4.4.0   | 1.561 s | 0.0278 s | 0.0246 s | 1.565 s |
| Build_xUnit  | 2.9.3   | 1.564 s | 0.0124 s | 0.0116 s | 1.561 s |
| Build_MSTest | 3.10.3  | 1.575 s | 0.0299 s | 0.0279 s | 1.576 s |


### Scenario: Tests focused on assertion performance and validation

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.6 (24G84) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD

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
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

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
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

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

BenchmarkDotNet v0.15.2, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean       | Error     | StdDev    | Median     |
|---------- |-------- |-----------:|----------:|----------:|-----------:|
| TUnit_AOT | 0.57.1  |   314.0 ms |  29.84 ms |  87.99 ms |   329.8 ms |
| TUnit     | 0.57.1  | 1,188.1 ms |  83.97 ms | 247.60 ms | 1,177.7 ms |
| NUnit     | 4.4.0   | 1,814.3 ms | 123.80 ms | 365.02 ms | 1,766.1 ms |
| xUnit     | 2.9.3   | 1,462.5 ms |  84.84 ms | 235.09 ms | 1,422.8 ms |
| MSTest    | 3.10.3  | 1,133.2 ms |  51.97 ms | 150.77 ms | 1,100.6 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    26.61 ms |  0.182 ms |  0.161 ms |    26.59 ms |
| TUnit     | 0.57.1  |   938.36 ms | 18.603 ms | 20.677 ms |   927.43 ms |
| NUnit     | 4.4.0   | 1,308.53 ms | 10.390 ms |  9.719 ms | 1,308.11 ms |
| xUnit     | 2.9.3   | 1,402.42 ms |  9.245 ms |  8.648 ms | 1,403.54 ms |
| MSTest    | 3.10.3  | 1,259.40 ms |  9.881 ms |  9.243 ms | 1,259.51 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    63.21 ms |  1.213 ms |  1.134 ms |    62.48 ms |
| TUnit     | 0.57.1  |   996.57 ms | 19.281 ms | 21.431 ms |   992.48 ms |
| NUnit     | 4.4.0   | 1,386.19 ms | 14.152 ms | 13.238 ms | 1,387.15 ms |
| xUnit     | 2.9.3   | 1,464.90 ms | 11.035 ms |  9.782 ms | 1,468.07 ms |
| MSTest    | 3.10.3  | 1,308.98 ms | 11.412 ms | 10.675 ms | 1,309.98 ms |


### Scenario: Simple tests with basic operations and assertions

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.6 (24G84) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean     | Error    | StdDev   | Median   |
|---------- |-------- |---------:|---------:|---------:|---------:|
| TUnit_AOT | 0.57.1  | 128.5 ms |  4.02 ms | 11.40 ms | 124.4 ms |
| TUnit     | 0.57.1  | 615.8 ms | 19.95 ms | 58.52 ms | 603.8 ms |
| NUnit     | 4.4.0   | 786.8 ms | 23.26 ms | 68.60 ms | 745.5 ms |
| xUnit     | 2.9.3   | 823.6 ms | 22.23 ms | 65.56 ms | 798.6 ms |
| MSTest    | 3.10.3  | 770.3 ms | 19.78 ms | 57.69 ms | 767.7 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    25.81 ms |  0.406 ms |  0.399 ms |    25.84 ms |
| TUnit     | 0.57.1  |   936.54 ms | 18.619 ms | 21.441 ms |   937.05 ms |
| NUnit     | 4.4.0   | 1,300.84 ms | 12.730 ms | 11.285 ms | 1,299.38 ms |
| xUnit     | 2.9.3   | 1,373.69 ms |  7.678 ms |  7.182 ms | 1,372.76 ms |
| MSTest    | 3.10.3  | 1,240.82 ms |  9.477 ms |  8.865 ms | 1,241.06 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    61.76 ms |  0.935 ms |  0.874 ms |    62.31 ms |
| TUnit     | 0.57.1  | 1,033.57 ms | 20.101 ms | 30.086 ms | 1,036.49 ms |
| NUnit     | 4.4.0   | 1,392.65 ms | 26.003 ms | 25.538 ms | 1,391.78 ms |
| xUnit     | 2.9.3   | 1,457.09 ms | 12.690 ms | 11.249 ms | 1,458.60 ms |
| MSTest    | 3.10.3  | 1,351.13 ms | 22.130 ms | 20.700 ms | 1,347.52 ms |


### Scenario: Parameterized tests with multiple test cases using data attributes

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean    | Error    | StdDev   | Median   |
|---------- |-------- |--------:|---------:|---------:|---------:|
| TUnit_AOT | 0.57.1  |      NA |       NA |       NA |       NA |
| TUnit     | 0.57.1  |      NA |       NA |       NA |       NA |
| NUnit     | 4.4.0   | 1.039 s | 0.0981 s | 0.2720 s | 0.9592 s |
| xUnit     | 2.9.3   | 1.455 s | 0.1022 s | 0.2949 s | 1.3802 s |
| MSTest    | 3.10.3  | 1.198 s | 0.0640 s | 0.1887 s | 1.2161 s |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean    | Error    | StdDev   | Median  |
|---------- |-------- |--------:|---------:|---------:|--------:|
| TUnit_AOT | 0.57.1  |      NA |       NA |       NA |      NA |
| TUnit     | 0.57.1  |      NA |       NA |       NA |      NA |
| NUnit     | 4.4.0   | 1.427 s | 0.0229 s | 0.0235 s | 1.431 s |
| xUnit     | 2.9.3   | 1.459 s | 0.0258 s | 0.0276 s | 1.452 s |
| MSTest    | 3.10.3  | 1.317 s | 0.0234 s | 0.0219 s | 1.312 s |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean    | Error    | StdDev   | Median  |
|---------- |-------- |--------:|---------:|---------:|--------:|
| TUnit_AOT | 0.57.1  |      NA |       NA |       NA |      NA |
| TUnit     | 0.57.1  |      NA |       NA |       NA |      NA |
| NUnit     | 4.4.0   | 1.384 s | 0.0228 s | 0.0202 s | 1.390 s |
| xUnit     | 2.9.3   | 1.457 s | 0.0262 s | 0.0245 s | 1.455 s |
| MSTest    | 3.10.3  | 1.379 s | 0.0261 s | 0.0244 s | 1.378 s |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)


### Scenario: Tests utilizing class fixtures and shared test context

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean       | Error    | StdDev    | Median     |
|---------- |-------- |-----------:|---------:|----------:|-----------:|
| TUnit_AOT | 0.57.1  |   160.5 ms | 10.56 ms |  30.47 ms |   155.6 ms |
| TUnit     | 0.57.1  |   833.6 ms | 59.04 ms | 174.08 ms |   821.5 ms |
| NUnit     | 4.4.0   |   923.1 ms | 32.70 ms |  96.42 ms |   921.7 ms |
| xUnit     | 2.9.3   |   956.1 ms | 39.72 ms | 115.22 ms |   934.2 ms |
| MSTest    | 3.10.3  | 1,043.9 ms | 51.07 ms | 150.58 ms | 1,045.5 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    27.09 ms |  0.526 ms |  0.540 ms |    27.21 ms |
| TUnit     | 0.57.1  |   972.60 ms | 19.295 ms | 22.220 ms |   967.90 ms |
| NUnit     | 4.4.0   | 1,329.09 ms |  8.004 ms |  7.095 ms | 1,330.08 ms |
| xUnit     | 2.9.3   | 1,408.24 ms | 15.974 ms | 14.942 ms | 1,403.60 ms |
| MSTest    | 3.10.3  | 1,274.32 ms |  8.639 ms |  8.081 ms | 1,272.23 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    50.12 ms |  0.999 ms |  0.935 ms |    49.91 ms |
| TUnit     | 0.57.1  | 1,002.98 ms | 19.736 ms | 24.237 ms |   999.42 ms |
| NUnit     | 4.4.0   | 1,357.85 ms | 24.781 ms | 33.082 ms | 1,346.03 ms |
| xUnit     | 2.9.3   | 1,416.59 ms | 13.119 ms | 12.272 ms | 1,413.47 ms |
| MSTest    | 3.10.3  |          NA |        NA |        NA |          NA |

Benchmarks with issues:
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)


### Scenario: Tests executing in parallel to test framework parallelization

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.6 (24G84) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean       | Error    | StdDev    | Median     |
|---------- |-------- |-----------:|---------:|----------:|-----------:|
| TUnit_AOT | 0.57.1  |   247.2 ms | 21.49 ms |  63.36 ms |   248.1 ms |
| TUnit     | 0.57.1  | 1,058.4 ms | 86.56 ms | 252.51 ms |   993.3 ms |
| NUnit     | 4.4.0   | 1,210.9 ms | 78.98 ms | 231.63 ms | 1,194.6 ms |
| xUnit     | 2.9.3   | 1,051.7 ms | 57.13 ms | 167.55 ms | 1,018.6 ms |
| MSTest    | 3.10.3  | 1,183.5 ms | 37.88 ms | 110.51 ms | 1,163.3 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    25.50 ms |  0.375 ms |  0.313 ms |    25.38 ms |
| TUnit     | 0.57.1  |   929.24 ms | 16.223 ms | 15.175 ms |   932.24 ms |
| NUnit     | 4.4.0   | 1,304.34 ms | 13.692 ms | 12.807 ms | 1,301.07 ms |
| xUnit     | 2.9.3   | 1,371.91 ms |  7.274 ms |  6.449 ms | 1,372.34 ms |
| MSTest    | 3.10.3  | 1,247.91 ms | 13.051 ms | 12.208 ms | 1,246.12 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    54.93 ms |  0.774 ms |  0.686 ms |    54.62 ms |
| TUnit     | 0.57.1  | 1,009.12 ms | 19.549 ms | 23.272 ms | 1,009.85 ms |
| NUnit     | 4.4.0   | 1,373.59 ms |  9.395 ms |  7.846 ms | 1,372.31 ms |
| xUnit     | 2.9.3   | 1,438.60 ms | 13.122 ms | 12.274 ms | 1,438.36 ms |
| MSTest    | 3.10.3  | 1,325.19 ms | 17.816 ms | 16.666 ms | 1,323.28 ms |


### Scenario: A test that takes 50ms to execute, repeated 100 times

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sequoia 15.6 (24G84) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD

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
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

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
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

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

BenchmarkDotNet v0.15.2, macOS Sequoia 15.6.1 (24G90) [Darwin 24.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean     | Error    | StdDev    | Median   |
|---------- |-------- |---------:|---------:|----------:|---------:|
| TUnit_AOT | 0.57.1  | 207.8 ms | 18.26 ms |  53.83 ms | 199.6 ms |
| TUnit     | 0.57.1  | 894.1 ms | 47.53 ms | 139.40 ms | 876.7 ms |
| NUnit     | 4.4.0   | 860.5 ms | 25.96 ms |  74.48 ms | 870.5 ms |
| xUnit     | 2.9.3   | 847.5 ms | 22.27 ms |  65.67 ms | 844.8 ms |
| MSTest    | 3.10.3  | 785.0 ms | 17.51 ms |  51.35 ms | 787.6 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.3 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    26.74 ms |  0.447 ms |  0.396 ms |    26.71 ms |
| TUnit     | 0.57.1  |   942.45 ms | 18.389 ms | 18.060 ms |   939.52 ms |
| NUnit     | 4.4.0   | 1,298.18 ms | 13.956 ms | 13.054 ms | 1,295.61 ms |
| xUnit     | 2.9.3   | 1,369.73 ms | 11.147 ms | 10.427 ms | 1,369.25 ms |
| MSTest    | 3.10.3  | 1,251.17 ms |  8.671 ms |  8.111 ms | 1,253.70 ms |



#### windows-latest

```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.20348.4052) (Hyper-V)
AMD EPYC 7763 2.44GHz, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.9 (9.0.925.41916), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    59.15 ms |  1.181 ms |  2.465 ms |    58.82 ms |
| TUnit     | 0.57.1  | 1,042.22 ms | 20.036 ms | 24.606 ms | 1,033.75 ms |
| NUnit     | 4.4.0   | 1,380.61 ms | 15.488 ms | 14.487 ms | 1,381.32 ms |
| xUnit     | 2.9.3   | 1,417.84 ms | 23.045 ms | 21.557 ms | 1,410.58 ms |
| MSTest    | 3.10.3  | 1,295.74 ms | 12.298 ms | 11.504 ms | 1,299.89 ms |



