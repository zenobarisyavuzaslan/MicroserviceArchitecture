using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services;
using FreeCourse.Shared.Messages;
using FreeCourse.Shared.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeCourse.Services.Basket.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        private readonly RedisService _redisService;
        private readonly ISharedIdentityService _sharedIdentityService;
        public CourseNameChangedEventConsumer(RedisService redisService, ISharedIdentityService sharedIdentityService)
        {
            _redisService = redisService;
            _sharedIdentityService = sharedIdentityService;
        }
        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(_sharedIdentityService.GetUsetId);
            var basketResult = JsonSerializer.Deserialize<BasketDto>(existBasket);
            if (basketResult.BasketItems.Count > 0)
            {
                var items = basketResult.BasketItems.Where(x => x.CourseId == context.Message.CourseId);
                foreach (var item in items)
                {
                    item.CourseName = context.Message.UpdateName;
                }
                var status = await _redisService.GetDb().StringSetAsync(basketResult.UserId, JsonSerializer.Serialize(basketResult));

            }
        }
    }
}
