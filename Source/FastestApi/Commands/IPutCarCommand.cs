namespace FastestApi.Commands
{
    using FastestApi.ViewModels;
    using Boxed.AspNetCore;

    public interface IPutCarCommand : IAsyncCommand<int, SaveCar>
    {
    }
}
