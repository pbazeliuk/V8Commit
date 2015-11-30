using V8Commit.Entities.V8FileSystem;
using V8Commit.Services.FileV8Services;

namespace V8Commit.Plugins
{
    public interface IParsePlugin
    {
        void Parse(FileV8Reader fileV8Reader, V8FileSystem fileSystem, string output, int threads = 1);
    }
}
