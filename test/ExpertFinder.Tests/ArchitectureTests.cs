using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using FluentAssertions;
using HotChocolate;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ExpertFinder.Tests;

public class ArchitectureTests
{
    private static Architecture ApplicationArchitecture = new ArchLoader()
        .LoadAssemblies(typeof(ApplicationAssemblyMarker).Assembly)
        .Build();

    private static IObjectProvider<Class> DomainLayer = Classes().That().ResideInNamespace("ExpertFinder.Domain");
    private static IObjectProvider<Class> ApplicationLayer = Classes().That().ResideInNamespace("ExpertFinder.Application");
    private static IObjectProvider<Class> InfrastructureLayer = Classes().That().ResideInNamespace("ExpertFinder.Infrastructure");
    
    [Fact]
    public void LayersAreCorrectlyConnected()
    {
        Classes().That().Are(ApplicationLayer).Should().NotDependOnAny(InfrastructureLayer);
        Classes().That().Are(DomainLayer).Should().NotDependOnAny(ApplicationLayer);
        Classes().That().Are(DomainLayer).Should().NotDependOnAny(InfrastructureLayer);
    }
}