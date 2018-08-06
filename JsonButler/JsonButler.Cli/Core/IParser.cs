namespace Andeart.JsonButler.Cli.Core
{

    internal interface IParser<in T> where T : IParserOptions
    {
        void ExecuteOptions (T options);
    }

}