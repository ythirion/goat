namespace Goat.Examples
{
    public class Goat
    {
        void poorName()
        {
        }
    }

    public interface AGoat
    {
    }

    public abstract record Command(Guid Id)
    {
    }

    public interface ICommandHandler<in TCommand>
        where TCommand : Command
    {
        int Handle(TCommand command);
    }

    public record Order(Guid Id) : Command(Id)
    {
    }

    public class OrderService : ICommandHandler<Order>
    {
        public int Handle(Order command) => 42;
    }
}