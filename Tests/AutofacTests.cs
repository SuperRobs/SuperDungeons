using Model.Composition;

namespace Tests;

[TestFixture]
public class AutofacTests
{
    [Test]
    public void ContainerResolvesSuccessfully()
    {
        using var container = CompositionRoot.CreateBuilder().Build();
        Assert.That(container, Is.Not.Null);
    }
}