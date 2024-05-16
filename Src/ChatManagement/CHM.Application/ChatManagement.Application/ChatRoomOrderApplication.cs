using System;
using System.Threading.Tasks;
using AccountManagement.Domain.EmployerPageAgg;
using ChatManagement.Application.Contracts.ChatRoom;
using ChatManagement.Application.Contracts.ChatRoomOrder;
using ChatManagement.Domain.ChatRoomOrderAgg;

namespace ChatManagement.Application
{
    public class ChatRoomOrderApplication : IChatRoomOrderApplication
    {
        private readonly IChatRoomApplication _chatRoomApplication;
        private readonly IChatRoomOrderRepository _chatRoomOrderRepository;

        public ChatRoomOrderApplication(IChatRoomOrderRepository chatRoomOrderRepository, IChatRoomApplication chatRoomApplication, IEmployerPageRepository employerPageRepository)
        {
            _chatRoomOrderRepository = chatRoomOrderRepository;
            _chatRoomApplication = chatRoomApplication;
        }

        public async ValueTask<long> PlaceOrderAsync(long pageId, long chatRoomId, double payAmount)
        {
            var order = new ChatRoomOrder(pageId, chatRoomId, payAmount);

            await _chatRoomOrderRepository.CreateAsync(order);
            await _chatRoomOrderRepository.SaveChangesAsync();

            return order.Id;
        }

        public async ValueTask PaymentSucceededAsync(long orderId, long refId)
        {
            var order = await _chatRoomOrderRepository.GetAsync(orderId);
            order.PaymentSucceeded(refId);
            await _chatRoomOrderRepository.SaveChangesAsync();

            await _chatRoomApplication.PayAsync(order.ChatRoomId);
        }

        public async ValueTask<double> GetAmountByAsync(long id) => await _chatRoomOrderRepository.GetAmountByAsync(id);
    }
}
