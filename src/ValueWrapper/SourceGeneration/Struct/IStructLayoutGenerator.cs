using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct;

internal interface IStructLayoutGenerator
{
    public Namespace GenerateLayout(StructLayoutConfig config);
}