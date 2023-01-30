![code-coverage-badge](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/Miljankg/279c1aef1f40f826150b879af3072c46/raw/value-wrapper-total-test-code-coverage.json)
![build-status-badge](https://github.com/Miljankg/ValueWrapper/actions/workflows/dotnet.yml/badge.svg?branch=main)
# Table of Contents
* [Credits](#credits)  
* [Introduction](#introduction)  
* [How to use Value Wrapper](#how-to-use-value-wrapper)  

# Credits
This project was done as a part of learning about code generation in .NET. As such, a lot of things were done according to instructions in [Andrew Lock's blog post](https://andrewlock.net/series/creating-a-source-generator/). Andrew also has his own library solving a similar problem like this one: [StronglyTypedId](https://github.com/andrewlock/StronglyTypedId).

Please take a look at his blog and the project itself. I would also use this to thank him for the work he did, as without his information this would be much harder.

# Introduction
ValueWrapper is a library that targets a part of the specific problem, [primitive obsession](https://blog.ndepend.com/code-smell-primitive-obsession-and-refactoring-recipes/#:~:text=What%20is%20Primitive%20obsession,not%20in%20a%20good%20way.).

What happens often is that things like IDs and certain specific values like a City, Name, etc. are represented using primitives like `strings` and `integers`. This can happen within a domain implementation, the public interface of a library, or anywhere else within a codebase.

As primitive values can be easily replaced with one another (when of the same primitive type), this may lead to problems. `Address` of type `string` can be easily replaced with `Name` of type `string`. Here is another example:

```csharp
public void DoSomething(int someId, int someValue)
{
    // Some work being done here.
}
```
Invoking this function is error-prone, as the compiler won't complain about things like wrong parameters being passed to it, and similar. For example:
```csharp
int someValue = 98;
int someId = 55;

// All these invocations are correct for the compiler.
DoSomething(someValue, someId);
DoSomething(someValue, someValue);
DoSomething(someId, someId);
DoSomething(someId, someValue);
```
This may not seem like a big issue in an example like this. The problem may be discovered during the code review or test execution. However, in a normal, commercial project, following this practice will result in a lot of wrong invocations, that are not discovered at all, or discovered very late, in production, and the feedback loop can be very long.

Normally, to address this problem, we usually opt for creating our types when needed. This can be done for the `someId` from the example, but also for any other value that can be classified under a certain type. This depends on the situation. Then the situation from above looks like this:

```csharp
int someValue = 98;
int someId = new SomeId(55);

// Compiler error.
DoSomething(someValue, someId);
DoSomething(someValue, someValue);
DoSomething(someId, someId);

// Correct invocation.
DoSomething(someId, someValue);
```
> _Please note_ that `someValue` can also be wrapped in its own type. This depends a lot on the design of the API, and there is a balance to be struct between having primitive values and specific types.

When creating such types, there is a bit of work to be done, like implementing equality. This should be tested as well. As the number of types grows, the effort to maintain them, as well as the chance of making a mistake, also grows.

This library, as well as other libraries of this type, tries to automate this process. This is especially because structs are often used instead of classes, for performance reasons and inheritance is not an option in that case.

# How to use ValueWrapper

As usual, we have to define our type, in this case, it must be a struct, as at the moment, Value Wrapper does not work with classes. Struct must be marked partial. Then we can annotate the struct with ValueWrapper and add the type we would like to wrap as an argument of the ValueWrapperAttribute.

```csharp
[ValueWrapper(typeof(string))]
public readonly partial struct TestValue
{
}
```

The code is then automatically generated for this struct. Along with it, each of the generated struct has:

- A proper constructor, accepting a value of a correct type, annotated in the attribute.
- A static factory method From, accepting a value of a correct type, annotated in the attribute.
- A property that enables retrieval of the raw value.
- All equality methods, including the equality operators.
- ToString method.

```csharp
var testValue1 = new TestValue("TestString1");
var testValue2 = new TestValue("TestString2");
var testValue3 = TestValue.From("TestString1");
var testValue4 = default(TestValue);

// Outputs: "TestString1"
Console.WriteLine(testValue1.Value);

// Outputs: "TestString1"
Console.WriteLine(testValue1);

// Outputs: "<NULL>"
Console.WriteLine(testValue4);

// Outputs: True
Console.WriteLine(testValue1.Equals(testValue3));

// Outputs: True
Console.WriteLine(testValue1 == testValue3);

// Outputs: False
Console.WriteLine(testValue1.Equals(testValue2));

// Outputs: False
Console.WriteLine(testValue1 == testValue2);
```

**Please note**, any feedback on any aspect of this project is much appreciated. The project is in the early stages, and further optimizations and updates are yet to come. Criticism and suggestions are welcome.
