using PayCore.Enums;
using PayCore.Interfaces;
using PayCore.Providers;
using PayCore.Utils;
#if NETSTANDARD2_0
using QRCoder;
using System.DrawingCore;
using System.DrawingCore.Imaging;
#elif NET461
using ThoughtWorks.QRCode.Codec;
using System.Drawing;
using System.Drawing.Imaging;
#endif
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace PayCore
{
    /// <summary>
    /// ������Ҫ֧���Ķ��������ݣ�����֧������URL��ַ��HTML��
    /// </summary>
    /// <remarks>
    ///����֧��GB2312���롣 ͨ���� Web.config �е� configuration/system.web �ڵ����� <globalization requestEncoding="gb2312" responseEncoding="gb2312" />
    /// </remarks>
    public class PaymentSetting
    {
        #region �ֶ�

        GatewayBase gateway;

        #endregion

        #region ���캯��

        public PaymentSetting(GatewayBase gateway)
        {
            this.gateway = gateway;
        }

        public PaymentSetting(GatewayType gatewayType)
        {
            gateway = CreateGateway(gatewayType);
        }


        public PaymentSetting(GatewayType gatewayType, Merchant merchant, Order order)
            : this(gatewayType)
        {
            gateway.Merchant = merchant;
            gateway.Order = order;
        }

        #endregion

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        public GatewayBase Gateway
        {
            get
            {
                return gateway;
            }
        }


        /// <summary>
        /// �̼�����
        /// </summary>
        public Merchant Merchant
        {
            get
            {
                return gateway.Merchant;
            }

            set
            {
                gateway.Merchant = value;
            }
        }


        /// <summary>
        /// ��������
        /// </summary>
        public Order Order
        {
            get
            {
                return gateway.Order;
            }

            set
            {
                gateway.Order = value;
            }
        }

        #endregion

        #region ����


        private GatewayBase CreateGateway(GatewayType gatewayType)
        {
            switch (gatewayType)
            {
                case GatewayType.Alipay:
                    {
                        return new AlipayGateway();
                    }
                case GatewayType.WeChatPay:
                    {
                        return new WeChatPayGataway();
                    }
                case GatewayType.UnionPay:
                    {
                        return new UnionPayGateway();
                    }
                default:
                    {
                        return new NullGateway();
                    }
            }
        }

        public Dictionary<string, string> Payment(GatewayTradeType gatewayTradeType, Dictionary<string, string> map = null)
        {
            gateway.GatewayTradeType = gatewayTradeType;
            return Payment(gatewayTradeType);
        }

        public Dictionary<string, string> Payment(Dictionary<string, string> map = null)
        {
            switch (gateway.GatewayTradeType)
            {
                case GatewayTradeType.APP:
                    {
                        return BuildPayParams();
                    }               
                case GatewayTradeType.Wap:
                    {
                        WapPayment(map);
                    }
                    break;
                case GatewayTradeType.Web:
                    {
                        WebPayment();
                    }
                    break;
                case GatewayTradeType.QRCode:
                    {
                        QRCodePayment();
                    }
                    break;
                case GatewayTradeType.Public:
                    break;
                case GatewayTradeType.BarCode:
                    break;
                case GatewayTradeType.Applet:
                    break;
                case GatewayTradeType.None:
                    {
                        throw new NotSupportedException($"{gateway.GatewayType} û��ʵ�� {gateway.GatewayTradeType} �ӿ�");
                    }
                default:
                    break;
            }
            return null;
        }


        /// <summary>
        /// ����������֧��Url��Form������ά�롣
        /// </summary>
        /// <remarks>
        /// ����������Ƕ�����Url��Form������ת����Ӧ����֧��
        /// </remarks>
        private void WebPayment()
        {
            IPaymentUrl paymentUrl = gateway as IPaymentUrl;
            if (paymentUrl != null)
            {
                HttpUtil.Redirect(paymentUrl.BuildPaymentUrl());
                return;
            }

            IPaymentForm paymentForm = gateway as IPaymentForm;
            if (paymentForm != null)
            {
                HttpUtil.Write(paymentForm.BuildPaymentForm());
                return;
            }
            throw new NotSupportedException(gateway.GatewayType + " û��ʵ��֧���ӿ�");
        }

        /// <summary>
        /// ����WAP֧��
        /// </summary>
        /// <param name="map"></param>
        private void WapPayment(Dictionary<string, string> map = null)
        {
            IWapPaymentUrl paymentUrl = gateway as IWapPaymentUrl;
            if (paymentUrl != null)
            {
                if (gateway.GatewayType == GatewayType.WeChatPay)
                {
                    HttpUtil.Write($"<script language='javascript'>window.location='{paymentUrl.BuildWapPaymentUrl(map)}'</script>");
                }
                else
                {
                    HttpUtil.Redirect(paymentUrl.BuildWapPaymentUrl(map));
                }
                return;
            }

            IWapPaymentForm paymentForm = gateway as IWapPaymentForm;
            if (paymentForm != null)
            {
                HttpUtil.Write(paymentForm.BuildWapPaymentForm());
                return;
            }

            throw new NotSupportedException(gateway.GatewayType + " û��ʵ��֧���ӿ�");
        }

        /// <summary>
        ///��ά��֧��
        /// </summary>
        private void QRCodePayment()
        {          
            IPaymentQRCode paymentQRCode = gateway as IPaymentQRCode;
            if (paymentQRCode != null)
            {
                BuildQRCodeImage(paymentQRCode.GetPaymentQRCodeContent());
                return;
            }
            throw new NotSupportedException(gateway.GatewayType + " û��ʵ��֧���ӿ�");
        }

        /// <summary>
        /// ����APP��SDK֧����Ҫ�Ĳ���
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> BuildPayParams()
        {
            IAppParams appParams = gateway as IAppParams;
            if (appParams != null)
            {
                return appParams.BuildPayParams();
            }
            throw new NotSupportedException(gateway.GatewayType + " û��ʵ�� IAppParams ��ѯ�ӿ�");
        }

        /// <summary>
        /// ��ѯ�����������Ĳ�ѯ֪ͨ����ͨ����֧��֪ͨһ������ʽ���ء��ô�������֪ͨһ���ķ������ܲ�ѯ���������ݡ�
        /// </summary>
        public void QueryNotify()
        {
            IQueryUrl queryUrl = gateway as IQueryUrl;
            if (queryUrl != null)
            {
                HttpUtil.Redirect(queryUrl.BuildQueryUrl());
                return;
            }

            IQueryForm queryForm = gateway as IQueryForm;
            if (queryForm != null)
            {
                HttpUtil.Write(queryForm.BuildQueryForm());
                return;
            }

            throw new NotSupportedException(gateway.GatewayType + " û��ʵ�� IQueryUrl �� IQueryForm ��ѯ�ӿ�");
        }

        /// <summary>
        /// ��ѯ������������ö����Ĳ�ѯ���
        /// </summary>
        /// <returns></returns>
        public bool QueryNow()
        {
            IQueryNow queryNow = gateway as IQueryNow;
            if (queryNow != null)
            {
                return queryNow.QueryNow();
            }

            throw new NotSupportedException(gateway.GatewayType + " û��ʵ�� IQueryNow ��ѯ�ӿ�");
        }

     

        /// <summary>
        /// �����˿�
        /// </summary>
        /// <param name="refund"></param>
        /// <returns></returns>
        public Refund BuildRefund(Refund refund)
        {
            IRefundReq iRefund = gateway as IRefundReq;
            if (iRefund != null)
            {
                return iRefund.BuildRefund(refund);
            }
            throw new NotSupportedException(gateway.GatewayType + " û��ʵ�� IRefund ��ѯ�ӿ�");
        }

        /// <summary>
        /// ��ѯ�˿���
        /// </summary>
        /// <param name="refund"></param>
        /// <returns></returns>
        public Refund BuildRefundQuery(Refund refund)
        {
            IRefundReq iRefund = gateway as IRefundReq;
            if (iRefund != null)
            {
                return iRefund.BuildRefundQuery(refund);
            }
            throw new NotSupportedException(gateway.GatewayType + " û��ʵ�� IRefund ��ѯ�ӿ�");
        }

        /// <summary>
        /// �������ص�����
        /// </summary>
        /// <param name="gatewayParameterName">���صĲ�������</param>
        /// <param name="gatewayParameterValue">���صĲ���ֵ</param>
        public void SetGatewayParameterValue(string gatewayParameterName, string gatewayParameterValue)
        {
            Gateway.SetGatewayParameterValue(gatewayParameterName, gatewayParameterValue);
        }

        /// <summary>
        /// ���ɲ������ά��ͼƬ
        /// </summary>
        /// <param name="qrCodeContent">��ά������</param>
        private void BuildQRCodeImage(string qrCodeContent)
        {
#if NETSTANDARD2_0
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeContent, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            MemoryStream ms = new MemoryStream();
            qrCodeImage.Save(ms, ImageFormat.Png);
            byte[] buffer = ms.GetBuffer();
#elif NET461
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeScale = 4;  // ��ά���С
            Bitmap image = qrCodeEncoder.Encode(qrCodeContent, Encoding.GetEncoding(gateway.Charset));
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);   
            byte[] buffer = ms.GetBuffer();
#endif
            HttpUtil.Current.Response.ContentType = "image/x-png";
#if NETSTANDARD2_0
            HttpUtil.Current.Response.Body.WriteAsync(buffer, 0, (int)buffer.Length).GetAwaiter().GetResult();
#elif NET461
            HttpUtil.Current.Response.BinaryWrite(buffer);
#endif
        }
        #endregion
    }
}
