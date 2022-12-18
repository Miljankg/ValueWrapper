using ValueWrapper.Attributes;

namespace ValueWrapper.Tests.Integration.TestProject.TestNamespace1;

/// <summary>
/// This struct has the same name as the struct in the parent namespace,
/// to ensure that types with the same name are handled properly during
/// code generation.
/// </summary>
[ValueWrapper(typeof(int))]
public partial struct TestValue1
{
}