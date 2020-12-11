namespace FastestApi.Mappers
{
    using System;
    using FastestApi.Services;
    using FastestApi.ViewModels;
    using Boxed.Mapping;

    public class CarToSaveCarMapper : IMapper<Models.Car, SaveCar>, IMapper<SaveCar, Models.Car>
    {
        private readonly IClockService clockService;

        public CarToSaveCarMapper(IClockService clockService) =>
            this.clockService = clockService;

        public void Map(Models.Car source, SaveCar destination)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Cylinders = source.Cylinders;
            destination.Make = source.Make;
            destination.Model = source.Model;
        }

        public void Map(SaveCar source, Models.Car destination)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var now = this.clockService.UtcNow;

            if (destination.Created == DateTimeOffset.MinValue)
            {
                destination.Created = now;
            }

            destination.Cylinders = source.Cylinders;
            destination.Make = source.Make;
            destination.Model = source.Model;
            destination.Modified = now;
        }
    }
}
