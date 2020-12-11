namespace FastestApi.Commands
{
    using FastestApi.ViewModels;
    using Boxed.AspNetCore;

    public interface IPostCarCommand : IAsyncCommand<SaveCar>
    {
    }
}
