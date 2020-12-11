namespace FastestApi.Commands
{
    using FastestApi.ViewModels;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.JsonPatch;

    public interface IPatchCarCommand : IAsyncCommand<int, JsonPatchDocument<SaveCar>>
    {
    }
}
