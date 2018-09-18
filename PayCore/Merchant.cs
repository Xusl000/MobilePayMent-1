using PayCore.Enums;
using System;

namespace PayCore
{
    /// <summary>
    /// �̻�����
    /// </summary>
    [Serializable]
    public class Merchant
    {
        #region ˽���ֶ�

        string partner;
        string key;
        string email;
        string appId;
        Uri notifyUrl;
        Uri returnUrl;

        #endregion

        #region ���캯��

        public Merchant()
        {
        }


        public Merchant(string userName, string key, Uri notifyUrl, GatewayType gatewayType)
        {
            this.partner = userName;
            this.key = key;
            this.notifyUrl = notifyUrl;
            GatewayType = gatewayType;
        }

        #endregion

        #region ����

        /// <summary>
        /// �̻��ʺ�
        /// </summary>
        public string Partner
        {
            get
            {
                if (string.IsNullOrEmpty(partner))
                {
                    throw new ArgumentNullException("Partner", "�̻��ʺ�û������");
                }
                return partner;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Partner", "�̻��ʺŲ���Ϊ��");
                }
                partner = value;
            }
        }


        /// <summary>
        /// �̻���Կ
        /// </summary>
        public string Key
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("Key", "�̻���Կû������");
                }
                return key;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Key", "�̻���Կ����Ϊ��");
                }
                key = value;
            }
        }

        /// <summary>
        /// �̻�����
        /// </summary>
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        /// <summary>
        ///  ΢��֧������Ҫ
        /// </summary>
        public string AppId
        {
            get
            {
                return appId;
            }
            set
            {
                appId = value;
            }
        }

        /// <summary>
        /// ���ػط�֪ͨURL
        /// </summary>
        public Uri NotifyUrl
        {
            get
            {
                if (notifyUrl == null)
                {
                    throw new ArgumentNullException("NotifyUrl", "����֪ͨUrlû������");
                }
                return notifyUrl;
            }
            set
            {
                notifyUrl = value ?? throw new ArgumentNullException("NotifyUrl", "����֪ͨUrl����Ϊ��");
            }
        }

        /// <summary>
        /// ����������ת֪ͨURL
        /// </summary>
        public Uri ReturnUrl
        {
            get
            {
                return returnUrl;
            }
            set
            {
                returnUrl = value;
            }
        }

        /// <summary>
        /// ˽Կ��ַ
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// ��Կ��ַ
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// �ļ���ȡ��Կ
        /// </summary>
        public bool KeyFromFile { get; set; } = false;

        /// <summary>
        /// ��������
        /// </summary>
        public GatewayType GatewayType { get; set; }

        #endregion
    }
}