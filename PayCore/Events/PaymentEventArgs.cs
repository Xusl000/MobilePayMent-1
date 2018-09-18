using PayCore.Enums;
using PayCore.Utils;
using System;
using System.Collections.Generic;

namespace PayCore.Events
{
    /// <summary>
    /// ֧���¼����ݵĻ���
    /// </summary>
    public abstract class PaymentEventArgs : EventArgs
    {
        #region ˽���ֶ�

        protected GatewayBase gateway;
        string notifyServerHostAddress;

        #endregion

        #region ���캯��

        /// <summary>
        /// ��ʼ��֧���¼����ݵĻ���
        /// </summary>
        /// <param name="gateway">֧������</param>
        public PaymentEventArgs(GatewayBase gateway)
        {
            this.gateway = gateway;
#if NETSTANDARD2_0
            notifyServerHostAddress = HttpUtil.Current.Request.Host.Host;
#elif NET46
                notifyServerHostAddress = HttpUtil.Current.Request.UserHostAddress;
#endif
        }


        #endregion

        #region ����

        /// <summary>
        /// ����֧��֪ͨ������IP��ַ
        /// </summary>
        public string NotifyServerHostAddress
        {
            get
            {
                return notifyServerHostAddress;
            }
        }


        /// <summary>
        /// ֧�����ص�Get��Post���ݵļ���
        /// </summary>
        public ICollection<GatewayParameter> GatewayParameterData
        {
            get
            {
                return gateway.GatewayParameterData;
            }
        }

        #endregion

        #region ����

        public GatewayBase GatewayBase
        {
            get
            {
                return gateway;
            }
        }

        /// <summary>
        /// ������صĲ���ֵ��û�в���ֵʱ���ؿ��ַ�����Get��ʽ��ֵ��Ϊδ���롣
        /// </summary>
        /// <param name="gatewayParameterName">���صĲ�������</param>
        public string GetGatewayParameterValue(string gatewayParameterName)
        {
            return gateway.GetGatewayParameterValue(gatewayParameterName);
        }


        /// <summary>
        /// ������صĲ���ֵ��û�в���ֵʱ���ؿ��ַ�����Get��ʽ��ֵ��Ϊδ���롣
        /// </summary>
        /// <param name="gatewayParameterName">���صĲ�������</param>
        /// <param name="gatewayParameterRequestMethod">���ص����ݵ����󷽷�������</param>
        public string GetGatewayParameterValue(string gatewayParameterName, GatewayParameterRequestMethod gatewayParameterRequestMethod)
        {
            return gateway.GetGatewayParameterValue(gatewayParameterName, gatewayParameterRequestMethod);
        }

        #endregion
    }
}