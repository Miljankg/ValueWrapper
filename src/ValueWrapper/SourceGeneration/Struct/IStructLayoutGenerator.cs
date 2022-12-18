using ValueWrapper.SourceLayout;

namespace ValueWrapper.SourceGeneration.Struct;

internal interface IStructLayoutGenerator
{
    public RootNode GenerateLayout(StructLayoutConfig config);
}