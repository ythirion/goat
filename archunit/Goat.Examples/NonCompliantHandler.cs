using Goat.Examples.Commands;

namespace Goat.Examples
{
    public class NonCompliantHandler : ICommandHandler<Order>
    {
        public int Handle(Order command) => 42;
    }
}