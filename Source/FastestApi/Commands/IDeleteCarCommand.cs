namespace FastestApi.Commands
{
    using Boxed.AspNetCore;

    public interface IDeleteCarCommand : IAsyncCommand<int>
    {
    }
}
