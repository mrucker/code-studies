namespace HelloWorld.Library
{
    public class HelloWorld: IHelloWorld
    {
        public string SayHelloWorld()
        {
            return "Hello, World Expanded Interface!";
        }

        public string SayPersonalizedHelloWorld(string name)
        {
            return "Hello, " + name + "'s World!";
        }
    }
}