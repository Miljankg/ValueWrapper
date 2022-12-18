using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration;

internal interface ISourceNodeVisitor<out TResult, in TParams>
{
    TResult Visit(RootNode root, TParams @params);
    
    TResult Visit(Directive directive, TParams @params);
    
    TResult Visit(Namespace @namespace, TParams @params);

    TResult Visit(Structure @struct, TParams @params);

    TResult Visit(Constructor constructor, TParams @params);

    TResult Visit(StaticFactoryMethod staticFactoryMethod, TParams @params);

    TResult Visit(ValueProperty valueProperty, TParams @params);
    
    TResult Visit(ToString toString, TParams @params);
    
    TResult Visit(GetHashCode getHashCode, TParams @params);
    
    TResult Visit(Equals equals, TParams @params);
    
    TResult Visit(EqualsObject equalsObject, TParams @params);
    
    TResult Visit(EqualsOperator equalsOperator, TParams @params);
    
    TResult Visit(NotEqualOperator notEqualOperator, TParams @params);
}