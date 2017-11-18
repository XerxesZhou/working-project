/***************************************************************************
 * 
 *       ���ܣ�     ϵͳ��Ϣ�����ӿ�ʵ����
 *       ���ߣ�     ��ΰ
 *       ���ڣ�     2007-03-22
 * 
 *       �޸����ڣ� 
 *       �޸��ˣ�
 *       �޸����ݣ�
 * 
 * *************************************************************************/
namespace SmartSoft.Service.Implement.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    using SmartSoft.Domain.Common;
    using SmartSoft.Persistence.Common;
    using SmartSoft.Service.Interface.Common;


    using Castle.Facilities.AutomaticTransactionManagement;
    using Castle.Facilities.IBatisNetIntegration;
    using Castle.Services.Transaction;

    [Transactional]
    [UsesAutomaticSessionCreation]
    public class MessageService : IMessageService
    {
        private MessageSqlMapDao _message;
        public MessageService(MessageSqlMapDao message)
        {
            _message = message;
        }

        #region ϵͳ��Ϣ

        public IList<sysMessage> SelectShowMessageList(int opeID)
        {
            return _message.SelectShowMessageList(opeID);
        }

        public IList<sysMessage> SelectReceiveMessageList(int opeID)
        {
            return _message.SelectReceiveMessageList(opeID);
        }

        public IList<sysMessage> SelectSendMessageList(int opeID)
        {
            return _message.SelectSendMessageList(opeID);
        }
        
        [Transaction(TransactionMode.Requires)]
        public int InsertMessage(sysMessage message, ArrayList al)
        {
            //��������Ϣ����
            int messageId = _message.InsertMessage(message);
            //������Ϣ�鿴��
            for (int i = 0; i < al.Count; i++)
            {
                int opeID = (int)al[i];

                _message.InsertMessageLooker(messageId, opeID);
            }

            return messageId;
        }

        public sysMessage GetMessageDetail(int messageId)
        {
            return _message.GetMessageDetail(messageId);
        }

        public void InsertMessageReaded(int messageid, int operatorid)
        {
            _message.InsertMessageReaded(messageid, operatorid);
        }

        public void InsertMessageDeleted(int messageid, int operatorid)
        {
            _message.InsertMessageDeleted(messageid, operatorid);
        }

        public void DeleteMessage_Readed(int messageid, int operatorid)
        {
            _message.DeleteMessage_Readed(messageid, operatorid);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteMessage(int messageId)
        {
            _message.DeleteMessage(messageId);
        }

        public void UpdateMessage(sysMessage message)
        {
            _message.UpdateMessage(message);
        }

        public void UpdatesysMessage_LookerByMessageID(int operatorID, int messageID)
        {
            _message.UpdatesysMessage_LookerByMessageID(operatorID, messageID);
        }

        public bool SelectMessageIsRead(int messageid, int operatorid)
        {
            return _message.SelectMessageIsRead(messageid, operatorid);
        }

        #endregion

        #region ��ҳ

        public IList<sysMessage> SelectReceiveMessageListByCount(int count, int opeId)
        {
            return _message.SelectReceiveMessageListByCount(count, opeId);
        }

        #endregion
    }
}
