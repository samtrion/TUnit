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

BenchmarkDotNet v0.15.2, macOS Sonoma 14.7.6 (23H626) [Darwin 23.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method       | Version | Mean    | Error    | StdDev   | Median   |
|------------- |-------- |--------:|---------:|---------:|---------:|
| Build_TUnit  | 0.57.1  | 1.360 s | 0.0530 s | 0.1530 s | 1.3349 s |
| Build_NUnit  | 4.4.0   | 1.143 s | 0.0456 s | 0.1287 s | 1.1295 s |
| Build_xUnit  | 2.9.3   | 1.008 s | 0.0370 s | 0.1089 s | 0.9893 s |
| Build_MSTest | 3.10.2  | 1.053 s | 0.0406 s | 0.1197 s | 1.0281 s |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method       | Version | Mean    | Error    | StdDev   | Median  |
|------------- |-------- |--------:|---------:|---------:|--------:|
| Build_TUnit  | 0.57.1  | 1.938 s | 0.0384 s | 0.0653 s | 1.927 s |
| Build_NUnit  | 4.4.0   | 1.616 s | 0.0307 s | 0.0287 s | 1.614 s |
| Build_xUnit  | 2.9.3   | 1.597 s | 0.0272 s | 0.0291 s | 1.599 s |
| Build_MSTest | 3.10.2  | 1.618 s | 0.0257 s | 0.0241 s | 1.612 s |



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
| Build_TUnit  | 0.57.1  | 1.877 s | 0.0317 s | 0.0502 s | 1.866 s |
| Build_NUnit  | 4.4.0   | 1.613 s | 0.0308 s | 0.0316 s | 1.614 s |
| Build_xUnit  | 2.9.3   | 1.590 s | 0.0176 s | 0.0156 s | 1.592 s |
| Build_MSTest | 3.10.2  | 1.598 s | 0.0160 s | 0.0149 s | 1.594 s |


### Scenario: Tests focused on assertion performance and validation

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sonoma 14.7.6 (23H626) [Darwin 23.6.0]
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
| MSTest    | 3.10.2  |   NA |    NA |     NA |     NA |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
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
| MSTest    | 3.10.2  |   NA |    NA |     NA |     NA |

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
| MSTest    | 3.10.2  |   NA |    NA |     NA |     NA |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)


### Scenario: Tests running asynchronous operations and async/await patterns

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sonoma 14.7.6 (23H626) [Darwin 23.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean       | Error    | StdDev   | Median      |
|---------- |-------- |-----------:|---------:|---------:|------------:|
| TUnit_AOT | 0.57.1  |   100.3 ms |  1.44 ms |  1.34 ms |    99.60 ms |
| TUnit     | 0.57.1  |   570.4 ms | 11.38 ms | 19.01 ms |   568.58 ms |
| NUnit     | 4.4.0   | 1,014.2 ms | 20.26 ms | 46.54 ms | 1,016.34 ms |
| xUnit     | 2.9.3   |   974.2 ms | 25.49 ms | 75.15 ms |   961.49 ms |
| MSTest    | 3.10.2  |   882.0 ms | 25.56 ms | 75.35 ms |   880.16 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    27.10 ms |  0.490 ms |  0.459 ms |    27.02 ms |
| TUnit     | 0.57.1  |   929.93 ms | 17.940 ms | 20.659 ms |   928.88 ms |
| NUnit     | 4.4.0   | 1,302.05 ms |  8.479 ms |  7.931 ms | 1,301.11 ms |
| xUnit     | 2.9.3   | 1,393.32 ms |  9.929 ms |  9.288 ms | 1,395.64 ms |
| MSTest    | 3.10.2  | 1,242.56 ms | 14.023 ms | 12.431 ms | 1,241.11 ms |



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
| TUnit_AOT | 0.57.1  |    67.13 ms |  1.320 ms |  2.897 ms |    66.85 ms |
| TUnit     | 0.57.1  | 1,088.93 ms | 20.933 ms | 26.474 ms | 1,089.02 ms |
| NUnit     | 4.4.0   | 1,526.49 ms | 26.476 ms | 22.109 ms | 1,525.53 ms |
| xUnit     | 2.9.3   | 1,635.70 ms | 22.357 ms | 19.819 ms | 1,633.61 ms |
| MSTest    | 3.10.2  | 1,457.48 ms | 22.682 ms | 21.217 ms | 1,454.35 ms |


### Scenario: Simple tests with basic operations and assertions

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sonoma 14.7.6 (23H626) [Darwin 23.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev   | Median    |
|---------- |-------- |----------:|----------:|---------:|----------:|
| TUnit_AOT | 0.57.1  |  80.07 ms |  0.669 ms | 0.559 ms |  79.99 ms |
| TUnit     | 0.57.1  | 533.05 ms |  5.128 ms | 4.546 ms | 534.08 ms |
| NUnit     | 4.4.0   | 715.16 ms | 10.257 ms | 9.594 ms | 714.22 ms |
| xUnit     | 2.9.3   | 743.07 ms |  5.450 ms | 4.255 ms | 742.37 ms |
| MSTest    | 3.10.2  | 680.35 ms |  5.546 ms | 5.188 ms | 678.73 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    27.10 ms |  0.459 ms |  0.407 ms |    27.11 ms |
| TUnit     | 0.57.1  |   969.00 ms | 19.128 ms | 24.872 ms |   961.00 ms |
| NUnit     | 4.4.0   | 1,353.92 ms | 13.417 ms | 12.550 ms | 1,353.64 ms |
| xUnit     | 2.9.3   | 1,439.83 ms | 27.201 ms | 29.105 ms | 1,428.03 ms |
| MSTest    | 3.10.2  | 1,290.84 ms | 12.552 ms | 11.127 ms | 1,290.34 ms |



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
| TUnit_AOT | 0.57.1  |    56.24 ms |  1.089 ms |  1.377 ms |    56.85 ms |
| TUnit     | 0.57.1  |   992.98 ms | 19.624 ms | 24.100 ms |   983.22 ms |
| NUnit     | 4.4.0   | 1,330.24 ms |  9.868 ms |  8.748 ms | 1,328.60 ms |
| xUnit     | 2.9.3   | 1,396.49 ms | 12.154 ms | 10.774 ms | 1,397.28 ms |
| MSTest    | 3.10.2  | 1,280.81 ms | 13.927 ms | 13.027 ms | 1,278.38 ms |


### Scenario: Parameterized tests with multiple test cases using data attributes

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sonoma 14.7.6 (23H626) [Darwin 23.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean     | Error    | StdDev   | Median   |
|---------- |-------- |---------:|---------:|---------:|---------:|
| TUnit_AOT | 0.57.1  |       NA |       NA |       NA |       NA |
| TUnit     | 0.57.1  |       NA |       NA |       NA |       NA |
| NUnit     | 4.4.0   | 891.5 ms | 23.48 ms | 69.22 ms | 890.7 ms |
| xUnit     | 2.9.3   | 919.7 ms | 29.61 ms | 87.32 ms | 922.7 ms |
| MSTest    | 3.10.2  | 682.6 ms |  7.28 ms |  6.46 ms | 683.9 ms |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
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
| NUnit     | 4.4.0   | 1.318 s | 0.0097 s | 0.0091 s | 1.320 s |
| xUnit     | 2.9.3   | 1.395 s | 0.0148 s | 0.0139 s | 1.392 s |
| MSTest    | 3.10.2  | 1.280 s | 0.0140 s | 0.0131 s | 1.280 s |

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
| NUnit     | 4.4.0   | 1.340 s | 0.0151 s | 0.0141 s | 1.338 s |
| xUnit     | 2.9.3   | 1.393 s | 0.0153 s | 0.0143 s | 1.395 s |
| MSTest    | 3.10.2  | 1.303 s | 0.0122 s | 0.0114 s | 1.300 s |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)


### Scenario: Tests utilizing class fixtures and shared test context

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sonoma 14.7.6 (23H626) [Darwin 23.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean     | Error   | StdDev  | Median   |
|---------- |-------- |---------:|--------:|--------:|---------:|
| TUnit_AOT | 0.57.1  | 102.1 ms | 2.79 ms | 7.97 ms | 102.6 ms |
| TUnit     | 0.57.1  | 537.4 ms | 8.89 ms | 7.42 ms | 538.6 ms |
| NUnit     | 4.4.0   | 711.4 ms | 8.89 ms | 8.32 ms | 713.0 ms |
| xUnit     | 2.9.3   | 741.4 ms | 8.85 ms | 7.85 ms | 739.9 ms |
| MSTest    | 3.10.2  | 678.1 ms | 8.57 ms | 7.59 ms | 678.2 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    26.76 ms |  0.246 ms |  0.218 ms |    26.79 ms |
| TUnit     | 0.57.1  |   950.29 ms | 18.701 ms | 22.967 ms |   947.72 ms |
| NUnit     | 4.4.0   | 1,312.49 ms | 17.580 ms | 16.445 ms | 1,306.75 ms |
| xUnit     | 2.9.3   | 1,396.09 ms | 11.403 ms | 10.666 ms | 1,396.88 ms |
| MSTest    | 3.10.2  |          NA |        NA |        NA |          NA |

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
| TUnit_AOT | 0.57.1  |    56.52 ms |  1.106 ms |  1.317 ms |    56.61 ms |
| TUnit     | 0.57.1  | 1,033.38 ms | 19.401 ms | 20.759 ms | 1,037.99 ms |
| NUnit     | 4.4.0   | 1,379.78 ms | 10.651 ms |  9.963 ms | 1,380.07 ms |
| xUnit     | 2.9.3   | 1,445.90 ms | 16.010 ms | 14.192 ms | 1,446.06 ms |
| MSTest    | 3.10.2  |          NA |        NA |        NA |          NA |

Benchmarks with issues:
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)


### Scenario: Tests executing in parallel to test framework parallelization

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sonoma 14.7.6 (23H626) [Darwin 23.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit_AOT | 0.57.1  |  83.28 ms |  0.748 ms |  0.832 ms |  82.88 ms |
| TUnit     | 0.57.1  | 545.28 ms |  8.586 ms | 12.852 ms | 542.83 ms |
| NUnit     | 4.4.0   | 710.56 ms | 10.833 ms | 10.133 ms | 710.07 ms |
| xUnit     | 2.9.3   | 742.42 ms |  9.179 ms |  8.586 ms | 741.18 ms |
| MSTest    | 3.10.2  | 678.65 ms |  3.706 ms |  3.286 ms | 677.95 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    26.17 ms |  0.371 ms |  0.347 ms |    25.98 ms |
| TUnit     | 0.57.1  |   928.42 ms | 18.079 ms | 19.344 ms |   923.20 ms |
| NUnit     | 4.4.0   | 1,298.68 ms |  7.566 ms |  7.078 ms | 1,296.87 ms |
| xUnit     | 2.9.3   | 1,372.33 ms | 11.092 ms | 10.376 ms | 1,371.75 ms |
| MSTest    | 3.10.2  | 1,244.37 ms |  4.724 ms |  4.419 ms | 1,242.97 ms |



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
| TUnit_AOT | 0.57.1  |    48.70 ms |  0.971 ms |  1.870 ms |    48.20 ms |
| TUnit     | 0.57.1  |   986.87 ms | 19.339 ms | 23.022 ms |   982.62 ms |
| NUnit     | 4.4.0   | 1,334.94 ms |  8.503 ms |  7.537 ms | 1,332.96 ms |
| xUnit     | 2.9.3   | 1,388.24 ms | 13.543 ms | 12.668 ms | 1,385.75 ms |
| MSTest    | 3.10.2  | 1,279.55 ms | 11.184 ms | 10.461 ms | 1,278.05 ms |


### Scenario: A test that takes 50ms to execute, repeated 100 times

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sonoma 14.7.6 (23H626) [Darwin 23.6.0]
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
| MSTest    | 3.10.2  |   NA |    NA |     NA |     NA |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
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
| MSTest    | 3.10.2  |   NA |    NA |     NA |     NA |

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
| MSTest    | 3.10.2  |   NA |    NA |     NA |     NA |

Benchmarks with issues:
  RuntimeBenchmarks.TUnit_AOT: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.TUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.NUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.xUnit: Job-YNJDZW(Runtime=.NET 9.0)
  RuntimeBenchmarks.MSTest: Job-YNJDZW(Runtime=.NET 9.0)


### Scenario: Tests with setup and teardown lifecycle methods

#### macos-latest

```

BenchmarkDotNet v0.15.2, macOS Sonoma 14.7.6 (23H626) [Darwin 23.6.0]
Apple M1 (Virtual), 1 CPU, 3 logical and 3 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), Arm64 RyuJIT AdvSIMD

Runtime=.NET 9.0  

```
| Method    | Version | Mean      | Error     | StdDev    | Median    |
|---------- |-------- |----------:|----------:|----------:|----------:|
| TUnit_AOT | 0.57.1  |  84.52 ms |  1.684 ms |  3.662 ms |  83.37 ms |
| TUnit     | 0.57.1  | 582.26 ms | 11.343 ms | 27.824 ms | 575.38 ms |
| NUnit     | 4.4.0   | 780.12 ms | 15.429 ms | 38.423 ms | 768.06 ms |
| xUnit     | 2.9.3   | 977.58 ms | 23.602 ms | 69.592 ms | 984.96 ms |
| MSTest    | 3.10.2  | 872.00 ms | 21.032 ms | 61.351 ms | 879.19 ms |



#### ubuntu-latest

```

BenchmarkDotNet v0.15.2, Linux Ubuntu 24.04.2 LTS (Noble Numbat)
AMD EPYC 7763, 1 CPU, 4 logical and 2 physical cores
.NET SDK 9.0.304
  [Host]     : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2
  Job-YNJDZW : .NET 9.0.8 (9.0.825.36511), X64 RyuJIT AVX2

Runtime=.NET 9.0  

```
| Method    | Version | Mean        | Error     | StdDev    | Median      |
|---------- |-------- |------------:|----------:|----------:|------------:|
| TUnit_AOT | 0.57.1  |    26.96 ms |  0.189 ms |  0.158 ms |    27.00 ms |
| TUnit     | 0.57.1  |   958.51 ms | 18.770 ms | 22.344 ms |   963.68 ms |
| NUnit     | 4.4.0   | 1,330.83 ms | 15.449 ms | 14.451 ms | 1,331.21 ms |
| xUnit     | 2.9.3   | 1,399.25 ms | 10.808 ms |  9.581 ms | 1,398.50 ms |
| MSTest    | 3.10.2  | 1,275.67 ms | 10.446 ms |  9.771 ms | 1,274.51 ms |



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
| TUnit_AOT | 0.57.1  |    50.72 ms |  0.821 ms |  0.946 ms |    50.94 ms |
| TUnit     | 0.57.1  |   995.10 ms | 19.770 ms | 23.535 ms |   990.21 ms |
| NUnit     | 4.4.0   | 1,331.69 ms | 12.244 ms | 11.453 ms | 1,332.59 ms |
| xUnit     | 2.9.3   | 1,382.05 ms | 11.020 ms |  8.604 ms | 1,383.05 ms |
| MSTest    | 3.10.2  | 1,283.81 ms | 13.967 ms | 13.065 ms | 1,280.69 ms |



