using System.IO;
using System.Runtime.CompilerServices;
using VerifyTests;
using VerifyXunit;

namespace ValueWrapper.Tests.Integration.GeneratorTests.Helpers;

public static class VerifyInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifierSettings.DerivePathInfo(
            (_, projectDirectory, type, method) => new PathInfo(
                directory: Path.Combine(projectDirectory, "GeneratorTests", "Snapshots"),
                typeName: type.Name,
                methodName: method.Name));
    }
}