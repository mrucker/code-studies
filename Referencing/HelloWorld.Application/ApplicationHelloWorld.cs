using HelloWorld.Library;

namespace HelloWorld.Application
{
    class ApplicationHelloWorld: IHelloWorld
    {
        public string SayHelloWorld()
        {
            return "Hello from me, World!";
        }
    }
}
