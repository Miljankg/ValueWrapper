using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration;

internal interface ISourceNodeVisitor<out TResult, in TParams>
{
    TResult Visit(Namespace @namespace, TParams @params);

    TResult Visit(Structure @struct, TParams @params);

    TResult Visit(Constructor constructor, TParams @params);

    TResult Visit(StaticFactoryMethod staticFactoryMethod, TParams @params);

    TResult Visit(ValueProperty valueProperty, TParams @params);
}