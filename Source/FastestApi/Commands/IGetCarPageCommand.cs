namespace FastestApi.Commands
{
    using FastestApi.ViewModels;
    using Boxed.AspNetCore;

    public interface IGetCarPageCommand : IAsyncCommand<PageOptions>
    {
    }
}
