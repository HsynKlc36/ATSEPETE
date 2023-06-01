using AtSepete.Business.Abstract;
using AtSepete.Business.Constants;
using AtSepete.Business.Logger;
using AtSepete.Dtos.Dto.Reports;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AtSepete.Results;
using AtSepete.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Concrete
{
    public class ReportService : IReportService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMarketRepository _marketRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        public ReportService(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository, IMarketRepository marketRepository, IMapper mapper, ILoggerService loggerService)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _marketRepository = marketRepository;
            _mapper = mapper;
            _loggerService = loggerService;
        }
        public async Task<IDataResult<ReportCountDto>> GetAllReportCountAsync()
        {
            try
            {
              
                ReportCountDto reportCountDto = new()
                {
                    CountMarkets = (await _marketRepository.GetAllAsync()).Count(),
                    CountOrders = (await _orderRepository.GetAllAsync()).Count(),
                    CountProducts = (await _productRepository.GetAllAsync()).Count(),
                    CountUsers = (await _userRepository.GetAllAsync()).Count()
                };
                _loggerService.LogInfo(LogMessages.Report_Success);
                return new SuccessDataResult<ReportCountDto>(reportCountDto, Messages.ReportSuccess);
            }
            catch (Exception)
            {
                _loggerService.LogError(LogMessages.Report_Failed);
                return new ErrorDataResult<ReportCountDto>(Messages.ReportFailed);

            }


        }
    }
}
