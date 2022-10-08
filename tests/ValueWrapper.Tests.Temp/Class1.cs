using ValueWrapper.Attributes;

namespace ValueWrapper.Tests.Temp
{
    class Class
    {
        public Class()
        {
            RandomValue1.From("as");
        }
    }
        
    [ValueWrapper(typeof(string))]
    public partial struct RandomValue1
    {
    }
}