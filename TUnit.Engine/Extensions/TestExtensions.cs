﻿using Microsoft.Testing.Extensions.TrxReport.Abstractions;
using Microsoft.Testing.Platform.Extensions.Messages;
using TUnit.Core;
using TUnit.Core.Extensions;

namespace TUnit.Engine.Extensions;

internal static class TestExtensions
{
    internal static TestNode ToTestNode(this TestContext testContext)
    {
        var testDetails = testContext.TestDetails;
        
        var testNode = new TestNode
        {
            Uid = new TestNodeUid(testDetails.TestId),
            DisplayName = testContext.GetTestDisplayName(),
            Properties = new PropertyBag(
            [
                new TestFileLocationProperty(testDetails.TestFilePath, new LinePositionSpan
                {
                    Start = new LinePosition(testDetails.TestLineNumber, 0),
                    End = new LinePosition(testDetails.TestLineNumber, 0)
                }),
                new TestMethodIdentifierProperty(
                    Namespace: testDetails.TestClass.Type.Namespace!,
                    AssemblyFullName: testDetails.TestClass.Type.Assembly.FullName!,
                    TypeName: testContext.GetClassTypeName(),
                    MethodName: testDetails.TestName,
                    ParameterTypeFullNames: testDetails.TestMethodParameterTypes.Select(x => x.FullName!).ToArray(),
                    ReturnTypeFullName: testDetails.ReturnType.FullName!,
                    MethodArity: testDetails.TestMethod.GenericTypeCount
                    ),
                
                // Custom TUnit Properties
                ..testDetails.Categories.Select(category => new TestMetadataProperty(category, string.Empty)),
                ..testDetails.CustomProperties.Select(x => new TestMetadataProperty(x.Key, x.Value)),
                
                // Artifacts
                ..testContext.Artifacts.Select(x => new FileArtifactProperty(x.File, x.DisplayName, x.Description)),
                
                // TRX Report Properties
                new TrxFullyQualifiedTypeNameProperty(testDetails.TestClass.Type.FullName!),
                new TrxCategoriesProperty([..testDetails.Categories]),
            ])
        };
        
        return testNode;
    }

    internal static TestNode WithProperty(this TestNode testNode, IProperty property)
    {
        testNode.Properties.Add(property);
        return testNode;
    }
}