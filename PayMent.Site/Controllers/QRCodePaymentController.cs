using Microsoft.AspNetCore.Mvc;
using PayCore;
using PayCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayMent.Site.Controllers
{
    public class QRCodePaymentController : Controller
    {
        private readonly IGateways gateways;

        public QRCodePaymentController(IGateways gateways)
        {
            this.gateways = gateways;
        }
        /// <summary>
        /// 创建二维码订单
        /// </summary>
        /// <param name="gatewayType">支付网关类型</param>
        public void CreateOrder(GatewayType gatewayType)
        {
            //通过网关类型,交易类型获取网关
            var gateway = gateways.Get(gatewayType, GatewayTradeType.QRCode);
            //设置需要支付的订单的数据，创建支付订单URL地址或HTML表单
            var paymentSetting = new PaymentSetting(gateway);
            paymentSetting.Order = new Order()
            {
                OrderAmount = 0.01, //订单总金额
                OrderNo = DateTime.Now.ToString("yyyyMMddhhmmss"), //订单编号
                Subject = "QRCodePayment", //订单主题
                PaymentDate = DateTime.Now //订单支付时间
            };
            paymentSetting.Payment();
        }
    }
}
