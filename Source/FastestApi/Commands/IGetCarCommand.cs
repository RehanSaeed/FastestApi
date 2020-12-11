namespace FastestApi.Commands
{
    using Boxed.AspNetCore;

    public interface IGetCarCommand : IAsyncCommand<int>
    {
    }
}
