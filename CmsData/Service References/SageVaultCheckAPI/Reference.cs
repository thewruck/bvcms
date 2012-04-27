//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CmsData.SageVaultCheckAPI {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="https://www.sagepayments.net/web_services/wsVault/wsVaultVirtualCheck", ConfigurationName="SageVaultCheckAPI.wsVaultVirtualCheckSoap")]
    public interface wsVaultVirtualCheckSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.sagepayments.net/web_services/wsVault/wsVaultVirtualCheck/VIRTUAL_CHE" +
            "CK_PPD_SALE", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet VIRTUAL_CHECK_PPD_SALE(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.sagepayments.net/web_services/wsVault/wsVaultVirtualCheck/VIRTUAL_CHE" +
            "CK_PPD_CREDIT", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet VIRTUAL_CHECK_PPD_CREDIT(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.sagepayments.net/web_services/wsVault/wsVaultVirtualCheck/VIRTUAL_CHE" +
            "CK_CCD_SALE", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet VIRTUAL_CHECK_CCD_SALE(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_EIN, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.sagepayments.net/web_services/wsVault/wsVaultVirtualCheck/VIRTUAL_CHE" +
            "CK_CCD_CREDIT", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet VIRTUAL_CHECK_CCD_CREDIT(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_EIN, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.sagepayments.net/web_services/wsVault/wsVaultVirtualCheck/VIRTUAL_CHE" +
            "CK_WEB_SALE", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet VIRTUAL_CHECK_WEB_SALE(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY, 
                    string C_SSN, 
                    string C_DL_STATE_CODE, 
                    string C_DL_NUMBER, 
                    string C_DOB);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.sagepayments.net/web_services/wsVault/wsVaultVirtualCheck/VIRTUAL_CHE" +
            "CK_RCK_SALE", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet VIRTUAL_CHECK_RCK_SALE(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.sagepayments.net/web_services/wsVault/wsVaultVirtualCheck/VIRTUAL_CHE" +
            "CK_ARC_SALE", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet VIRTUAL_CHECK_ARC_SALE(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string C_CHECK_NUMBER, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.sagepayments.net/web_services/wsVault/wsVaultVirtualCheck/VIRTUAL_CHE" +
            "CK_TEL_SALE", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet VIRTUAL_CHECK_TEL_SALE(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.sagepayments.net/web_services/wsVault/wsVaultVirtualCheck/VIRTUAL_CHE" +
            "CK_PPD_SALE_WITH_AUTHENTICATION", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet VIRTUAL_CHECK_PPD_SALE_WITH_AUTHENTICATION(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY, 
                    string C_SSN, 
                    string C_DL_STATE_CODE, 
                    string C_DL_NUMBER, 
                    string C_DOB);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface wsVaultVirtualCheckSoapChannel : CmsData.SageVaultCheckAPI.wsVaultVirtualCheckSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class wsVaultVirtualCheckSoapClient : System.ServiceModel.ClientBase<CmsData.SageVaultCheckAPI.wsVaultVirtualCheckSoap>, CmsData.SageVaultCheckAPI.wsVaultVirtualCheckSoap {
        
        public wsVaultVirtualCheckSoapClient() {
        }
        
        public wsVaultVirtualCheckSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public wsVaultVirtualCheckSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public wsVaultVirtualCheckSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public wsVaultVirtualCheckSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet VIRTUAL_CHECK_PPD_SALE(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY) {
            return base.Channel.VIRTUAL_CHECK_PPD_SALE(M_ID, M_KEY, C_ORIGINATOR_ID, C_FIRST_NAME, C_MIDDLE_INITIAL, C_LAST_NAME, C_SUFFIX, C_ADDRESS, C_CITY, C_STATE, C_ZIP, C_COUNTRY, C_EMAIL, GUID, T_AMT, T_SHIPPING, T_TAX, T_ORDERNUM, C_TELEPHONE, C_FAX, C_SHIP_NAME, C_SHIP_ADDRESS, C_SHIP_CITY, C_SHIP_STATE, C_SHIP_ZIP, C_SHIP_COUNTRY);
        }
        
        public System.Data.DataSet VIRTUAL_CHECK_PPD_CREDIT(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY) {
            return base.Channel.VIRTUAL_CHECK_PPD_CREDIT(M_ID, M_KEY, C_ORIGINATOR_ID, C_FIRST_NAME, C_MIDDLE_INITIAL, C_LAST_NAME, C_SUFFIX, C_ADDRESS, C_CITY, C_STATE, C_ZIP, C_COUNTRY, C_EMAIL, GUID, T_AMT, T_SHIPPING, T_TAX, T_ORDERNUM, C_TELEPHONE, C_FAX, C_SHIP_NAME, C_SHIP_ADDRESS, C_SHIP_CITY, C_SHIP_STATE, C_SHIP_ZIP, C_SHIP_COUNTRY);
        }
        
        public System.Data.DataSet VIRTUAL_CHECK_CCD_SALE(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_EIN, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY) {
            return base.Channel.VIRTUAL_CHECK_CCD_SALE(M_ID, M_KEY, C_ORIGINATOR_ID, C_EIN, C_FIRST_NAME, C_MIDDLE_INITIAL, C_LAST_NAME, C_SUFFIX, C_ADDRESS, C_CITY, C_STATE, C_ZIP, C_COUNTRY, C_EMAIL, GUID, T_AMT, T_SHIPPING, T_TAX, T_ORDERNUM, C_TELEPHONE, C_FAX, C_SHIP_NAME, C_SHIP_ADDRESS, C_SHIP_CITY, C_SHIP_STATE, C_SHIP_ZIP, C_SHIP_COUNTRY);
        }
        
        public System.Data.DataSet VIRTUAL_CHECK_CCD_CREDIT(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_EIN, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY) {
            return base.Channel.VIRTUAL_CHECK_CCD_CREDIT(M_ID, M_KEY, C_ORIGINATOR_ID, C_EIN, C_FIRST_NAME, C_MIDDLE_INITIAL, C_LAST_NAME, C_SUFFIX, C_ADDRESS, C_CITY, C_STATE, C_ZIP, C_COUNTRY, C_EMAIL, GUID, T_AMT, T_SHIPPING, T_TAX, T_ORDERNUM, C_TELEPHONE, C_FAX, C_SHIP_NAME, C_SHIP_ADDRESS, C_SHIP_CITY, C_SHIP_STATE, C_SHIP_ZIP, C_SHIP_COUNTRY);
        }
        
        public System.Data.DataSet VIRTUAL_CHECK_WEB_SALE(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY, 
                    string C_SSN, 
                    string C_DL_STATE_CODE, 
                    string C_DL_NUMBER, 
                    string C_DOB) {
            return base.Channel.VIRTUAL_CHECK_WEB_SALE(M_ID, M_KEY, C_ORIGINATOR_ID, C_FIRST_NAME, C_MIDDLE_INITIAL, C_LAST_NAME, C_SUFFIX, C_ADDRESS, C_CITY, C_STATE, C_ZIP, C_COUNTRY, C_EMAIL, GUID, T_AMT, T_SHIPPING, T_TAX, T_ORDERNUM, C_TELEPHONE, C_FAX, C_SHIP_NAME, C_SHIP_ADDRESS, C_SHIP_CITY, C_SHIP_STATE, C_SHIP_ZIP, C_SHIP_COUNTRY, C_SSN, C_DL_STATE_CODE, C_DL_NUMBER, C_DOB);
        }
        
        public System.Data.DataSet VIRTUAL_CHECK_RCK_SALE(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY) {
            return base.Channel.VIRTUAL_CHECK_RCK_SALE(M_ID, M_KEY, C_ORIGINATOR_ID, C_FIRST_NAME, C_MIDDLE_INITIAL, C_LAST_NAME, C_SUFFIX, C_ADDRESS, C_CITY, C_STATE, C_ZIP, C_COUNTRY, C_EMAIL, GUID, T_AMT, T_SHIPPING, T_TAX, T_ORDERNUM, C_TELEPHONE, C_FAX, C_SHIP_NAME, C_SHIP_ADDRESS, C_SHIP_CITY, C_SHIP_STATE, C_SHIP_ZIP, C_SHIP_COUNTRY);
        }
        
        public System.Data.DataSet VIRTUAL_CHECK_ARC_SALE(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string C_CHECK_NUMBER, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY) {
            return base.Channel.VIRTUAL_CHECK_ARC_SALE(M_ID, M_KEY, C_ORIGINATOR_ID, C_FIRST_NAME, C_MIDDLE_INITIAL, C_LAST_NAME, C_SUFFIX, C_ADDRESS, C_CITY, C_STATE, C_ZIP, C_COUNTRY, C_EMAIL, GUID, C_CHECK_NUMBER, T_AMT, T_SHIPPING, T_TAX, T_ORDERNUM, C_TELEPHONE, C_FAX, C_SHIP_NAME, C_SHIP_ADDRESS, C_SHIP_CITY, C_SHIP_STATE, C_SHIP_ZIP, C_SHIP_COUNTRY);
        }
        
        public System.Data.DataSet VIRTUAL_CHECK_TEL_SALE(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY) {
            return base.Channel.VIRTUAL_CHECK_TEL_SALE(M_ID, M_KEY, C_ORIGINATOR_ID, C_FIRST_NAME, C_MIDDLE_INITIAL, C_LAST_NAME, C_SUFFIX, C_ADDRESS, C_CITY, C_STATE, C_ZIP, C_COUNTRY, C_EMAIL, GUID, T_AMT, T_SHIPPING, T_TAX, T_ORDERNUM, C_TELEPHONE, C_FAX, C_SHIP_NAME, C_SHIP_ADDRESS, C_SHIP_CITY, C_SHIP_STATE, C_SHIP_ZIP, C_SHIP_COUNTRY);
        }
        
        public System.Data.DataSet VIRTUAL_CHECK_PPD_SALE_WITH_AUTHENTICATION(
                    string M_ID, 
                    string M_KEY, 
                    string C_ORIGINATOR_ID, 
                    string C_FIRST_NAME, 
                    string C_MIDDLE_INITIAL, 
                    string C_LAST_NAME, 
                    string C_SUFFIX, 
                    string C_ADDRESS, 
                    string C_CITY, 
                    string C_STATE, 
                    string C_ZIP, 
                    string C_COUNTRY, 
                    string C_EMAIL, 
                    string GUID, 
                    string T_AMT, 
                    string T_SHIPPING, 
                    string T_TAX, 
                    string T_ORDERNUM, 
                    string C_TELEPHONE, 
                    string C_FAX, 
                    string C_SHIP_NAME, 
                    string C_SHIP_ADDRESS, 
                    string C_SHIP_CITY, 
                    string C_SHIP_STATE, 
                    string C_SHIP_ZIP, 
                    string C_SHIP_COUNTRY, 
                    string C_SSN, 
                    string C_DL_STATE_CODE, 
                    string C_DL_NUMBER, 
                    string C_DOB) {
            return base.Channel.VIRTUAL_CHECK_PPD_SALE_WITH_AUTHENTICATION(M_ID, M_KEY, C_ORIGINATOR_ID, C_FIRST_NAME, C_MIDDLE_INITIAL, C_LAST_NAME, C_SUFFIX, C_ADDRESS, C_CITY, C_STATE, C_ZIP, C_COUNTRY, C_EMAIL, GUID, T_AMT, T_SHIPPING, T_TAX, T_ORDERNUM, C_TELEPHONE, C_FAX, C_SHIP_NAME, C_SHIP_ADDRESS, C_SHIP_CITY, C_SHIP_STATE, C_SHIP_ZIP, C_SHIP_COUNTRY, C_SSN, C_DL_STATE_CODE, C_DL_NUMBER, C_DOB);
        }
    }
}